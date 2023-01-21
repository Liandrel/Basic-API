using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace BasicAPI.Services
{
    public static class VersionServices
    {
        public static void AddVersionOptions(ApiVersioningOptions opts)
        {
            opts.AssumeDefaultVersionWhenUnspecified = true;
            opts.DefaultApiVersion = new(1, 0);
            opts.ReportApiVersions = true;
        }

        public static void AddVersionedApiExplorerOptions(ApiExplorerOptions opts)
        {
            opts.GroupNameFormat = "'v'VVV";
            opts.SubstituteApiVersionInUrl = true;

        }
    }

}
