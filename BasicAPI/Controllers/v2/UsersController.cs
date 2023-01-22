using BasicAPI.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BasicAPI.Controllers.v2;

[Route("api/v2/[controller]")]
[ApiController]
[ApiVersion("2.0")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;

    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }


    // GET: api/<UsersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "version 2: value1", "version 2: value2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    [Authorize(Policy = PolicyConstans.MustHaveEmployeeId)]
    [Authorize(Policy = PolicyConstans.MustBeAVeteranEmployee)]
    public IActionResult Get(int id)
    {
        try
        {
            if (id < 0 || id > 100)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            _logger.LogInformation(@"The api\v2\Users\{id}  was called", id);
            return Ok($"Value{id}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The given Id of {Id} was invalid.", id);
            return BadRequest("The index was out of range.");
        }
    }

    // POST api/<UsersController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<UsersController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // PATCH api/<UsersController>/5
    [HttpPatch("{id}")]
    public void Patch(int id, [FromBody] string value)
    {
    }

    // DELETE api/<UsersController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
