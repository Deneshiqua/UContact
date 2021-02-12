using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UContact.MyContactApi.Database.Entities;
using UContact.MyContactApi.Database.Services;
using UContact.MyContactApi.Models;

namespace UContact.MyContactApi.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    [Route("api/v{version:apiVersion}/mycontacts")]
    public class MyContactController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;
        public MyContactController(IPersonService personService,
            IMapper mapper)
        {
            this._personService = personService;
            this._mapper= mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(int skip = 0, int take = 10)
        {
            var contacts = await _personService.GetAll(take, skip);
            return Ok(contacts);
        }

        [HttpGet("{id:Guid?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
                return BadRequest();

            var person = await _personService.GetById(id.Value);
            if (person == null)
                return NotFound("Contact not found.");

            var model  = _mapper.Map<Person,PersonModel>(person);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create([FromBody] PersonModel model)
        {
            if (model is null)
                return BadRequest();

            var entity = _mapper.Map<PersonModel, Person>(model);

            var contactModel = await _personService.Insert(entity);
            return Ok(contactModel);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update([FromBody] PersonModel model)
        {
            if (model is null)
                return BadRequest();

            var entity = _mapper.Map<PersonModel, Person>(model);

            var contactModel = await _personService.Update(entity);
            return Ok(contactModel);
        }

        [HttpDelete("{id:Guid?}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete([FromRoute] Guid? id)
        {
            if (!id.HasValue || id.Value == Guid.Empty)
                return BadRequest();

            await _personService.Delete(id.Value);
            return NoContent();
        }

    }
}
