using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/v{version:apiVersion}/VillaNumberAPI")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class VillaNumberAPIController : Controller
    {
        private readonly IVillaNumberRepository _villaNumber;
        private readonly IVillaRepository _dbVilla;
        private readonly IMapper _mapper;
        private readonly APIResponse _response;
       public VillaNumberAPIController(IVillaNumberRepository villaNumber, IMapper mapper, IVillaRepository dbVilla)
        {
            _villaNumber = villaNumber;
            _mapper = mapper;
            this._response = new();
            _dbVilla = dbVilla;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllNumbersAsync(CancellationToken token) 
        {
            try
            {
                IEnumerable<VillaNumber> villaNumbersList = await _villaNumber.GetAllAsync(token);
                _response.Result = _mapper.Map<List<VillaNumberDTO>>(villaNumbersList);
                _response.StatusCode = HttpStatusCode.OK;
                return _response;
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> CreateVillaNumberAsync([FromBody] VillaNumberCreateDTO villaNumberCreateDTO, CancellationToken token)
        {
            try
            {
                if (villaNumberCreateDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { $"{villaNumberCreateDTO} is null!" };
                    return BadRequest(_response);
                }

                if (await _villaNumber.GetAsync(token, n => n.VillaNo == villaNumberCreateDTO.VillaNo) != null)
                {
                    ModelState.AddModelError("VillaNumberController error", "Villa is already exist");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = ModelState.Values
                                      .SelectMany(v => v.Errors)
                                      .Select(e => e.ErrorMessage)
                                      .ToList();

                    return BadRequest(_response);
                }

                if(await _dbVilla.GetAsync(token, u => u.Id == villaNumberCreateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is invalid!");
                    return BadRequest(ModelState);
                }

                VillaNumber tempVillanumber = _mapper.Map<VillaNumber>(villaNumberCreateDTO);
                await _villaNumber.CreateAsync(tempVillanumber, token);
                _response.Result = _mapper.Map<VillaNumberDTO>(tempVillanumber);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetVillaNumber", new { id = tempVillanumber.VillaNo }, _response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess =false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages
                    = new List<string>() { ex.Message.ToString() };
            }
            return _response;
        }

        [MapToApiVersion("2.0")]
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Value1", "Value2" };
        }

        [HttpGet("{id:int}", Name = "GetVillaNumber")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetVillaNumberAsync(int id, CancellationToken token)
        {
            try
            {
                if (id <= 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages = new List<string>() { "id can not be less then 1" };
                    return BadRequest(_response);
                }
                VillaNumber villaNumber = await _villaNumber.GetAsync(token, n => n.VillaNo == id);
                VillaNumberDTO villaNumberDTO = _mapper.Map<VillaNumberDTO>(villaNumber);

                if (villaNumberDTO == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages
                        = new List<string>() { "Villa number not found! " };
                    return NotFound(_response);
                }
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = villaNumberDTO;
                return Ok(_response);
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages
                    = new List<string>() { ex.Message.ToString() };
                return BadRequest(_response);
            }
            
        }

        [HttpDelete("{id:int}", Name = "RemoveVillaNumberAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> RemoveVillaNumberAsync(int id, CancellationToken token)
        {
            try
            {
                if (id <= 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages
                        = new List<string>() { "id cannot be less then 1!" };

                    return BadRequest(_response);
                }

                VillaNumber tempVilla = await _villaNumber.GetAsync(token, n => n.VillaNo == id);

                if (tempVilla == null)
                {
                    ModelState.AddModelError("RemoveVillaNumberAsync error: ", "Villa number not found!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    return NotFound(_response);
                }

                await _villaNumber.RemoveAsync(tempVilla, token);
                return NoContent();
            }
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages
                    = new List<string>() { ex.Message.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> VillaNumberUpdateAsync([FromBody] VillaNumberUpdateDTO updateDTO, int id, CancellationToken token)
        {
            try
            {
                if (id <= 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages
                        = new List<string>() { "id cannot be less then 1!" };

                    return BadRequest(_response);
                }

                VillaNumber villaNumber = await _villaNumber.GetAsync(token, n => n.VillaNo == id, false);

                if (villaNumber == null)
                {
                    ModelState.AddModelError("VillaNumberUpdateAsync error: ", "Villa number not found!");
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessages = ModelState.Values
                        .SelectMany(x => x.Errors)
                        .Select(x => x.ErrorMessage)
                        .ToList();

                    return NotFound(_response);
                }
                if (await _dbVilla.GetAsync(token, u => u.Id == updateDTO.VillaID) == null)
                {
                    ModelState.AddModelError("CustomError", "Villa ID is invalid!");
                    return BadRequest(ModelState);
                }
                DateTime createdDate = villaNumber.CreatedDate;
                villaNumber = _mapper.Map<VillaNumber>(updateDTO);
                await _villaNumber.UpdateVillaNumberAsync(villaNumber, createdDate, token);
                return NoContent();
            } 
            catch (Exception ex) 
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages
                    = new List<string>() { ex.Message.ToString() };
                return BadRequest(_response);
            }
        }
    }
}
