using Assgiment1011.Contracts;
using Assgiment1011.Models.DTOs.Updated;
using Assgiment1011.Models.DTOs;
using Assgiment1011.Models.RestfulModel;
using Assgiment1011.Models;
using Assgiment1011.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Assgiment1011.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly IAnswerRepository _ansRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;
        public AnswerController(ILoggerService logger, IAnswerRepository ansRepo, IMapper mapper)
        {
            _logger = logger;
            _ansRepo = ansRepo;
            _mapper = mapper;
            _response = new();
        }

        [HttpGet("{id:int}", Name ="GetAnswer")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetAnswer(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var ann = await _ansRepo.GetAsync(u => u.Id == id);

                if (ann == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<AnswerDTO>(ann);
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
        public async Task<ActionResult<APIResponse>> GetAllAnswer([FromQuery(Name = "beginDate")] DateTime? beginDate,
                                                            [FromQuery(Name = "endDate")] DateTime? endDate,
                                                            [FromQuery(Name = "contentSearch")] string? contentFilter,

                                                                      int pageSize = 0,
                                                                      int pageNumber = 1)
        {
            try
            {
                IEnumerable<Answer> objList;



                objList = await _ansRepo.GetAllAsync(beginDate,
                                                     endDate,
                                                     contentFilter,
                                                     pageSize:pageSize,
                                                     pageNumber: pageNumber);



                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));

                _response.Result = _mapper.Map<List<AnswerDTO>>(objList);
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
        public async Task<ActionResult<APIResponse>> CreateAnswer([FromBody] AnswerDTO createDTO)
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

                var obj = _mapper.Map<Answer>(createDTO);

                obj.Id = 0;
                await _ansRepo.CreateAsync(obj);
                _response.Result = _mapper.Map<AnswerDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetAnswer", new { id = obj.Id }, _response);
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
        public async Task<ActionResult<APIResponse>> DeleteAnswer(int id)
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

                var obj = await _ansRepo.GetAsync(c => c.Id == id);
                if (obj == null)
                {
                    return NotFound();
                }

                await _ansRepo.RemoveAsync(obj);
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
        public async Task<ActionResult<APIResponse>> UpdateAnswer(int id, [FromBody] AnswerUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    _response.ErrorMessages = new()
                    {
                        "Answer is not exist."
                    };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var model = await _ansRepo.GetAsync(c => c.Id == id);
                
                if(model == null)
                {
                    _response.ErrorMessages = new()
                    {
                        "Answer is not exist."
                    };
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }
                model.Content = updateDTO.Content;
                await _ansRepo.UpadteAsync(model);
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
        public async Task<IActionResult> UpdatePartialAnswer(int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<AnswerUpdateDTO> patchDTO)
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }
            var ans = await _ansRepo.GetAsync(u => u.Id == id, tracked: false);

            var annUpdateDTO = _mapper.Map<AnswerUpdateDTO>(ans);


            if (ans == null)
            {
                return BadRequest();
            }
            patchDTO.ApplyTo(annUpdateDTO, ModelState);
            var model = await _ansRepo.GetAsync(c => c.Id == id);

            if (model == null)
            {
                _response.ErrorMessages = new()
                    {
                        "Answer is not exist."
                    };
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            model.Content = annUpdateDTO.Content;
            await _ansRepo.UpadteAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }
    }
}
