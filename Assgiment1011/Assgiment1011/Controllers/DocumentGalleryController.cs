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
    public class DocumentGalleryController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IDocumentGalleryRepository _docGalRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        public DocumentGalleryController(ILoggerService logger, IDocumentGalleryRepository docGalRepo, IMapper mapper)
        {
            _logger = logger;
            _docGalRepo = docGalRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}", Name="GetDocumentGallery")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetDocumentGallery(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var ann = await _docGalRepo.GetAsync(u => u.Id == id);

                if (ann == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<DocumentGalleryDTO>(ann);
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
        
        public async Task<ActionResult<APIResponse>> GetDocumentGalleries([FromQuery(Name = "createdDate")] DateTime? createdDate,
                                                                      [FromQuery(Name = "titleSearch")] string? titleFilter,
                                                                      int pageSize = 0,
                                                                      int pageNumber = 1)
        {
            try
            {
                IEnumerable<DocumentGallery> annList;



                if (createdDate.HasValue)
                {
                    annList = await _docGalRepo.GetAllAsync(c => c.CreatedDate >= createdDate.Value, pageSize: pageSize, pageNumber: pageNumber);
                }
                else
                {
                    annList = await _docGalRepo.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                if (!String.IsNullOrEmpty(titleFilter))
                {
                    annList = annList.Where(c => c.Title.ToLower().Contains(titleFilter.ToLower()));
                }





                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.Result = _mapper.Map<List<DocumentGalleryDTO>>(annList);
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
        public async Task<ActionResult<APIResponse>> CreateDocumentGallery([FromBody] DocumentGalleryDTO createDTO)
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

                var obj = _mapper.Map<DocumentGallery>(createDTO);


                await _docGalRepo.CreateAsync(obj);
                _response.Result = _mapper.Map<DocumentGalleryDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetDocumentGallery", new { id = obj.Id }, _response);
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

        public async Task<ActionResult<APIResponse>> DeleteDocumentGallery(int id)
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

                var obj = await _docGalRepo.GetAsync(c => c.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }

                await _docGalRepo.RemoveAsync(obj);
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
        public async Task<ActionResult<APIResponse>> Update([FromQuery(Name ="id")]int id, [FromBody] DocumentGalleryUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                var model = _mapper.Map<DocumentGallery>(updateDTO);
                model.Id = id;
                await _docGalRepo.UpdateAsync(model);
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
        public async Task<IActionResult> UpdatePartialDocumentGallery(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<DocumentGalleryUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var doc = await _docGalRepo.GetAsync(u => u.Id == id, tracked: false);

            var annUpdateDTO = _mapper.Map<DocumentGalleryUpdateDTO>(doc);


            if (doc == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(annUpdateDTO, ModelState);
            var model = _mapper.Map<DocumentGallery>(annUpdateDTO);

            model.Id = id;

            await _docGalRepo.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }

}
