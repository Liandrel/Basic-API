using BasicAPI.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opts => SwaggerServices.AddSwaggerGenOptions(opts));
builder.Services.AddAuthorization(opts => AuthServices.AddPoliciesOptions(opts));
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(opts => AuthServices.AddJwtBearerOptions(builder, opts));
builder.Services.AddApiVersioning(opts => VersionServices.AddVersionOptions(opts));
builder.Services.AddVersionedApiExplorer(opts => VersionServices.AddVersionedApiExplorerOptions(opts));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opts => SwaggerServices.AddSwaggeroptions(opts));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
