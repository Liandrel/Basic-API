using BasicAPI.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BasicAPI.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    // GET: api/<UsersController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "version 1: value1", "version 1: value2" };
    }

    // GET api/<UsersController>/5
    [HttpGet("{id}")]
    [Authorize(Policy = PolicyConstans.MustHaveEmployeeId)]
    [Authorize(Policy = PolicyConstans.MustBeAVeteranEmployee)]
    public string Get(int id)
    {
        return $"value #{id}";
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
