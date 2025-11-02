using Assgiment1011.Models;
using Assgiment1011.Models.DTOs;
using Assgiment1011.Models.RestfulModel;
using Assgiment1011.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Assgiment1011.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;

        private readonly GenericAPIResponse<DocumentDTO> _response;

        private readonly IMapper _mapper;
        public DocumentController(IDocumentRepository documentRepository, IMapper mapper) { 

            _documentRepository = documentRepository;

            _response = new();

            _mapper = mapper;
        }

        [HttpGet("{id:int}", Name = "GetDocument")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenericAPIResponse<DocumentDTO>>> GetDocument(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var entity = await _documentRepository.GetAsync(u => u.Id == id);

                if (entity == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<DocumentDTO>(entity);
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
        [Route("GetDocumentByAuthorName")]
        public async Task<ActionResult<GenericAPIResponse<DocumentDTO>>> GetDocumentsByName([FromQuery] string authorName)
        {
            var response = new GenericAPIResponse<DocumentDTO>();
            try
            {
                var entity = await _documentRepository.GetDocumentsByAuthorNameCompiledAsync(authorName);

                response.Result = _mapper.Map<Document, DocumentDTO>(entity);
                
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return Ok(response);
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.ErrorMessages = new()
                {
                    ex.Message,
                    ex.ToString()
                };
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Result = null;
                return BadRequest(_response);
            }
           
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("createdocument")]
        public async Task<ActionResult<GenericAPIResponse<DocumentDTO>>> CreateDocument([FromBody] DocumentDTO createDTO)
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

                var obj = _mapper.Map<DocumentDTO,Document>(createDTO);


                await _documentRepository.CreateAsync(obj);
                _response.Result = _mapper.Map<DocumentDTO>(obj);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute(nameof(this.GetDocument), new { Id = obj.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }


    }
}
