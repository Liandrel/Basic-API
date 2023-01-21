using BasicAPI.Constans;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasicAPI.Services;

public static class AuthServices
{
    public static void AddPoliciesOptions(AuthorizationOptions opts)
    {
        opts.AddPolicy(PolicyConstans.MustHaveEmployeeId, policy =>
        {
            policy.RequireClaim("employeeId");
        });


        opts.AddPolicy(PolicyConstans.MustBeTheCEO, policy =>
        {
            policy.RequireClaim("title", "CEO");
        });

        opts.AddPolicy(PolicyConstans.MustBeAVeteranEmployee, policy =>
        {
            policy.RequireClaim("employeeId", "E001", "E002", "E003", "E004");
        });

        opts.FallbackPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
    }


    public static void AddJwtBearerOptions(WebApplicationBuilder? builder, JwtBearerOptions opts)
    {
        opts.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration.GetValue<string>("Authentication:Issuer"),
            ValidAudience = builder.Configuration.GetValue<string>("Authentication:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(
                    builder.Configuration.GetValue<string>("Authentication:SecretKey")))
        };
    }
}
