using Assgiment1011.Contracts;
using Assgiment1011.Models;
using Assgiment1011.Models.DTOs;
using Assgiment1011.Models.RestfulModel;
using Assgiment1011.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;
using Assgiment1011.Extensions;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Assgiment1011.Models.FilterModels;

using Assgiment1011.Models.DTOs.Updated;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Assgiment1011.Utilities;
using Assgiment1011.Utilities.ActionFilters;

namespace Assgiment1011.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    [LogActionFilter]
    public class AnnouncementController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IAnnouncementRepository _annRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        public AnnouncementController(ILoggerService logger, IAnnouncementRepository announcementRepo, IMapper mapper)
        {
            _logger = logger;
            _annRepo = announcementRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}", Name ="GetAnnouncement")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<APIResponse>> GetAnnouncement(int id)
        {
            try
            {
                
                if(id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var ann = await _annRepo.GetAsync(u => u.Id == id);

                if(ann == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<AnnouncementDTO>(ann);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);

            }catch(Exception ex)
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
        
        public async Task<ActionResult<APIResponse>> GetAnnouncements([FromQuery(Name ="createdDate")]DateTime? createdDate,
                                                                      [FromQuery(Name ="titleSearch")]string? titleFilter,
                                                                      [FromQuery(Name ="slugSearch")]string? slugFilter,
                                                                      int pageSize = 0,
                                                                      int pageNumber = 1)
        {
            try
            {
                IEnumerable<Announcement> annList;


                annList = await _annRepo.GetAllAsync(createdDate, titleFilter, slugFilter,pageSize:pageSize, pageNumber: pageNumber);

                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize};
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.Result = _mapper.Map<List<AnnouncementDTO>>(annList);
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch(Exception ex)
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
        public async Task<ActionResult<APIResponse>> CreateAnnouncement([FromBody] AnnouncementDTO createDTO)
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

                if(createDTO.AuthorId == 0)
                {
                    _response.ErrorMessages = new()
                    {
                        "This AuthorId method is not invalid"
                    };

                    _response.Result = createDTO;

                    return BadRequest(_response);
                }
                
                var obj = _mapper.Map<Announcement>(createDTO);

                
                await _annRepo.CreateAsync(obj);
                _response.Result = _mapper.Map<AnnouncementDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAnnouncement", new { id = obj.Id }, _response);
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
        
        public async Task<ActionResult<APIResponse>> DeleteAnnoucement(int id)
        {
            try
            {
                if(id == 0)
                {
                    _response.ErrorMessages = new()
                    {
                        $"Param \"id\" = {id}is not valid"
                    };

                    return BadRequest(_response);
                }

                var obj = await _annRepo.GetAsync(c => c.Id == id);
                if(obj == null)
                {
                    return NotFound();
                }

                await _annRepo.RemoveAsync(obj);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch(Exception ex)
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
        public async Task<ActionResult<APIResponse>> UpdateAnnouncement([FromBody]AnnouncementUpdateDTO updateDTO)
        {
            try
            {
                int id = updateDTO.Id;
                var model = await _annRepo.GetAsync(c => c.Id == id);

                if(model == null)
                {
                    _response.ErrorMessages = new()
                    {
                        "Cannot Announcement is updated"
                    };

                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }
                
                model.State = updateDTO.State;
                model.Title = updateDTO.Title;
                model.Description = updateDTO.Description;
                model.Slug = updateDTO.Slug;
                model.ImageUrl = updateDTO.ImageUrl;


                await _annRepo.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<AnnouncementDTO>(model);
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
        public async Task<IActionResult> UpdatePartialAnnouncement(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<AnnouncementUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var ann = await _annRepo.GetAsync(u => u.Id == id, tracked: false);

             var annUpdateDTO = _mapper.Map<AnnouncementUpdateDTO>(ann);


            if (ann == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(annUpdateDTO, ModelState);
            var model = _mapper.Map<Announcement>(annUpdateDTO);

            await _annRepo.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
