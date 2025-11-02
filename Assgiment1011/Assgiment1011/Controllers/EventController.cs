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
    public class EventController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IEventRepository _eventRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        public EventController(ILoggerService logger, IEventRepository docGalRepo, IMapper mapper)
        {
            _logger = logger;
            _eventRepo = docGalRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}", Name = "GetEvent")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetEvent(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var ann = await _eventRepo.GetAsync(u => u.Id == id);

                if (ann == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<EventDTO>(ann);
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
        
        public async Task<ActionResult<APIResponse>> GetAllEvent([FromQuery(Name = "beginDate")] DateTime? beginDate,
                                                            [FromQuery(Name = "endDate")] DateTime? endDate,
                                                            [FromQuery(Name = "nameSearch")] string? nameFilter,

                                                                      int pageSize = 0,
                                                                      int pageNumber = 1)
        {
            try
            {
                IEnumerable<Event> objList;



                if (beginDate.HasValue)
                {
                    objList = await _eventRepo.GetAllAsync(c => c.BeginDate > beginDate.Value, pageSize: pageSize, pageNumber: pageNumber);
                }
                else
                {
                    objList = await _eventRepo.GetAllAsync(pageSize: pageSize, pageNumber: pageNumber);
                }

                if (endDate.HasValue)
                {
                    objList = objList.Where(c => c.EndDate > endDate.Value);
                }
                

                if (!String.IsNullOrEmpty(nameFilter))
                {
                    objList = objList.Where(c => c.Name.ToLower().Contains(nameFilter.ToLower()));
                }





                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.Result = _mapper.Map<List<EventDTO>>(objList);
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
        public async Task<ActionResult<APIResponse>> CreateEvent([FromBody] EventDTO createDTO)
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

                var obj = _mapper.Map<Event>(createDTO);


                await _eventRepo.CreateAsync(obj);
                _response.Result = _mapper.Map<EventDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetEvent", new { id = obj.Id }, _response);
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

        public async Task<ActionResult<APIResponse>> DeleteEvent(int id)
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

                var obj = await _eventRepo.GetAsync(c => c.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }

                await _eventRepo.RemoveAsync(obj);
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
        public async Task<ActionResult<APIResponse>> UpdateEvent(int id, [FromBody] EventUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }

                var model = await _eventRepo.GetAsync(c => c.Id == id);
                if (model == null)
                {
                    _response.ErrorMessages = new()
                    {
                        "This event is not exist!"
                    };

                    return BadRequest(_response);
                }

                model.Name = updateDTO.Name;
                model.Description = updateDTO.Description;
                model.BeginDate = updateDTO.BeginDate;
                model.EndDate = updateDTO.EndDate;
                

                await _eventRepo.UpdateAsync(model);
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
        public async Task<IActionResult> UpdatePartialEvent(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<EventUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var model = await _eventRepo.GetAsync(u => u.Id == id);

            if (model == null)
            {
                _response.ErrorMessages = new()
                    {
                        "This event is not exist!"
                    };

                return BadRequest(_response);
            }

            var updateDTO = _mapper.Map<EventUpdateDTO>(model);


            if (updateDTO == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(updateDTO, ModelState);
            model.Name = updateDTO.Name;
            model.Description = updateDTO.Description;
            model.BeginDate = updateDTO.BeginDate;
            model.EndDate = updateDTO.EndDate;

            await _eventRepo.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }

}
