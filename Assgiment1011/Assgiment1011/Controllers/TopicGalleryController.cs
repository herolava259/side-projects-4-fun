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
    public class TopicGalleryController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly ITopicGalleryRepository _objRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        public TopicGalleryController(ILoggerService logger, ITopicGalleryRepository objRepo, IMapper mapper)
        {
            _logger = logger;
            _objRepo = objRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}", Name = "GetTopicGallery")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetTopicGallery(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var obj = await _objRepo.GetAsync(u => u.Id == id);

                if (obj == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<TopicGalleryDTO>(obj);
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
        
        public async Task<ActionResult<APIResponse>> GetAllTopicGallery([FromQuery(Name = "title")] string? titleFilter,
                                                            [FromQuery(Name = "description")] string? descFilter,
                                                                      int pageSize = 0,
                                                                      int pageNumber = 1)
        {
            try
            {
                IEnumerable<TopicGallery> objList;



                if (!String.IsNullOrEmpty(titleFilter))
                {
                    objList = await _objRepo.GetAllAsync(c => c.Title.ToLower().Contains(titleFilter.ToLower())
                                                        , includeProperties: "DocumentGallery, Documents", pageSize: pageSize, pageNumber: pageNumber);
                }
                else
                {
                    objList = await _objRepo.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }


                if (!String.IsNullOrEmpty(titleFilter))
                {
                    objList = objList.Where(c => c.Description.ToLower().Contains(descFilter.ToLower()));
                }






                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.Result = _mapper.Map<List<TopicGalleryDTO>>(objList);
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
        public async Task<ActionResult<APIResponse>> CreateTopicGallery([FromBody] TopicGalleryDTO createDTO)
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

                if (createDTO.DocumentGalleryId == 0)
                {
                    _response.ErrorMessages = new()
                    {
                        "This DocumentGalleryId method is not invalid"
                    };

                    _response.Result = createDTO;

                    return BadRequest(_response);
                }

                var obj = _mapper.Map<TopicGallery>(createDTO);

                obj.Id = 0;
                await _objRepo.CreateAsync(obj);
                _response.Result = _mapper.Map<TopicGalleryDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetTopicGallery", new { id = obj.Id }, _response);
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
        [HttpDelete]

        public async Task<ActionResult<APIResponse>> DeleteTopicGallery(int id)
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
        public async Task<ActionResult<APIResponse>> UpdateTopicGallery(int id, [FromBody] TopicGalleryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.ErrorMessages = new()
                    {
                        "TopicGallery is not exist."
                    };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var model = await _objRepo.GetAsync(c => c.Id == id);

                if (model == null)
                {
                    _response.ErrorMessages = new()
                    {
                        "TopicGallery is not exist."
                    };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                model.Title = updateDTO.Title;
                model.Description = updateDTO.Description;

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
        public async Task<IActionResult> UpdatePartialTopicGallery(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<TopicGalleryUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var model = await _objRepo.GetAsync(c => c.Id == id);

            if (model == null)
            {
                _response.ErrorMessages = new()
                    {
                        "TopicGallery is not exist."
                    };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }

            var updateDTO = _mapper.Map<TopicGalleryUpdateDTO>(model);



            patchDTO.ApplyTo(updateDTO, ModelState);

            model.Title = updateDTO.Title;
            model.Description = updateDTO.Description;
            await _objRepo.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
