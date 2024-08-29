using Microsoft.AspNetCore.Mvc;
using RestWithASPNet.Business;
using RestWithASPNet.Data.VO;
using RestWithASPNet.HyperMedia.Filters;

namespace RestWithASPNet.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonBusiness _personService;
        public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
        {
            _logger = logger;
            _personService = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personService.FindAll());
        } 
        
        [HttpGet("{id:long}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(long id)
        {
            var person = _personService.FindById(id);
            if (person == null)
                return NotFound();

            return Ok(person);
        }

        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();

            return Ok(_personService.Create(person));
        }

        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            if (person == null)
                return BadRequest();

            return Ok(_personService.Update(person));
        }

        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
