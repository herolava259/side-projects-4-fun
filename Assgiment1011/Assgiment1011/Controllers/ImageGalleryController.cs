using Assgiment1011.Contracts;
using Assgiment1011.Models.DTOs.Updated;
using Assgiment1011.Models.DTOs;
using Assgiment1011.Models.RestfulModel;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Assgiment1011.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImageGalleryController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IImageGalleryRepository _objRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        public ImageGalleryController(ILoggerService logger, IImageGalleryRepository objRepo, IMapper mapper)
        {
            _logger = logger;
            _objRepo = objRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}", Name = "GetImageGallery")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetImageGallery(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var ann = await _objRepo.GetAsync(u => u.Id == id);

                if (ann == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<ImageGalleryDTO>(ann);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        
        public async Task<ActionResult<APIResponse>> GetAllImageGallery([FromQuery(Name = "name")] string? nameFilter,
                                                            [FromQuery(Name = "description")] string? descFilter,
                                                            

                                                                      int pageSize = 0,
                                                                      int pageNumber = 1)
        {
            try
            {
                IEnumerable<ImageGallery> objList;



                if (!String.IsNullOrEmpty(nameFilter))
                {
                    objList = await _objRepo.GetAllAsync(c => c.Name.ToLower().Contains(nameFilter.ToLower()), pageSize: pageSize, pageNumber: pageNumber);
                }
                else
                {
                    objList = await _objRepo.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                if (!String.IsNullOrEmpty(descFilter))
                {
                    objList = objList.Where(c => c.Description.ToLower().Contains(descFilter.ToLower()));
                }


                




                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.Result = _mapper.Map<List<ImageGalleryDTO>>(objList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>()
                {
                    ex.ToString()
                };
            }
            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateImageGallery([FromBody] ImageGalleryDTO createDTO)
        {
            try
            {


                if (createDTO == null)
                {

                    _response.ErrorMessages = new()
                    {
                        "This input must be not empty"
                    };


                    return BadRequest(_response);
                }

                if (createDTO.AuthorId == 0)
                {
                    _response.ErrorMessages = new()
                    {
                        "This AuthorId method is not invalid"
                    };

                    _response.Result = createDTO;

                    return BadRequest(_response);
                }

                var obj = _mapper.Map<ImageGallery>(createDTO);

                obj.Id = 0;
                await _objRepo.CreateAsync(obj);
                _response.Result = _mapper.Map<ImageGalleryDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetImageGallery", new { id = obj.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}")]

        public async Task<ActionResult<APIResponse>> DeleteImageGallery(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.ErrorMessages = new()
                    {
                        $"Param \"id\" = {id}is not valid"
                    };

                    return BadRequest(_response);
                }

                var obj = await _objRepo.GetAsync(c => c.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }

                await _objRepo.RemoveAsync(obj);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new()
                {
                    ex.ToString()
                };
            }

            return _response;
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateImageGallery(int id, [FromBody] ImageGalleryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.ErrorMessages = new()
                    {
                        "ImageGallery is not exist."
                    };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var model = await _objRepo.GetAsync(c => c.Id == id);

                if (model == null)
                {
                    _response.ErrorMessages = new()
                    {
                        "ImageGallery is not exist."
                    };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                model.Description = updateDTO.Description;
                model.Name = model.Name;
                await _objRepo.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialImageGallery(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<ImageGalleryUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var ans = await _objRepo.GetAsync(u => u.Id == id, tracked: false);

            var annUpdateDTO = _mapper.Map<ImageGalleryUpdateDTO>(ans);


            if (ans == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(annUpdateDTO, ModelState);
            var model = await _objRepo.GetAsync(c => c.Id == id);

            if (model == null)
            {
                _response.ErrorMessages = new()
                    {
                        "ImageGallery is not exist."
                    };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            model.Name = annUpdateDTO.Name;
            await _objRepo.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
