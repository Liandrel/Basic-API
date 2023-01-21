using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace BasicAPI.Services
{
    public static class SwaggerServices
    {
        public static void AddSwaggeroptions(SwaggerUIOptions opts)
        {
            opts.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        }

        public static void AddSwaggerGenOptions(SwaggerGenOptions opts)
        {
            var title = "Versione API";
            var description = "Web Api with versioning";
            var terms = new Uri("https://localhost:7197"); //usually for api terms
            var license = new OpenApiLicense()
            {
                Name = "Full license information/link"
            };
            var contact = new OpenApiContact()
            {
                Name = "Api Name",
                Email = "Api contact Email",
                Url = new Uri("https://www.liandrel.com")
            };

            opts.SwaggerDoc("v1", new OpenApiInfo()
            {
                Version = "v1",
                Title = $"{title} v1",
                Description = description,
                TermsOfService = terms,
                License = license,
                Contact = contact
            });

            opts.SwaggerDoc("v2", new OpenApiInfo()
            {
                Version = "v2",
                Title = $"{title} v2",
                Description = description,
                TermsOfService = terms,
                License = license,
                Contact = contact
            });
        }
    }
}
