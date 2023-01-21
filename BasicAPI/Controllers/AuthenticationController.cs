using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BasicAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;

    public record AuthenticationData(string? UserName, string? Password);
    public record UserData(int UserId, string UserName, string Title, string EmployeeId);

    public AuthenticationController(IConfiguration config)
    {
        _config = config;
    }

    // api/Authentication/token
    [HttpPost("token")]
    public ActionResult<string> Authenticate([FromBody] AuthenticationData data)
    {
        var user = ValidateCredentials(data);

        if(user is null)
        {
            return Unauthorized();
        }

        var token = GenerateToken(user);

        return Ok(token);
    }

    private string GenerateToken(UserData user)
    {
        var secretKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(
                _config.GetValue<string>("Authentication:SecretKey")));

        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256 );

        List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        claims.Add(new("title", user.Title));
        claims.Add(new("employeeId", user.EmployeeId));



        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,//When ticeb become valid
            DateTime.UtcNow.AddMinutes(2),//When token expires
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);

    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        // DEMO CODE TO MAKE CREATING TOKEN WORK, IRL USE AUTHENTICATION SYSTEM HERE


        if(CompareValues(data.UserName, "FirstUN") && CompareValues(data.Password, "Test.123"))
        {
            return new UserData(1, data.UserName!, "CEO", "E001");
        }

        if (CompareValues(data.UserName, "SecondUN") && CompareValues(data.Password, "Test.123"))
        {
            return new UserData(2, data.UserName!, "HOP", "E007");
        }

        return null;

    }

    private bool CompareValues(string? actual, string expected)
    {
        if(actual is not null)
        {
            if(actual.Equals(expected))
            {
                return true;
            }
        }
        return false;
    }

}
