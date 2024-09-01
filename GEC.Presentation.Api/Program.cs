//Author @AKShaheen
using System.Text;
using GEC.Business.Interfaces;
using GEC.Runtime.Connections;
using GEC.Runtime.DependencyInjection;
using GEC.Presentation.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.AspNetCore.Mvc;
using GEC.Business.Contracts.Response;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddInfrastructureServices()
        .AddBusinessServices()
        .AddValidationServices();

    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    
#if AuthMode
// Authentication And Authorization Swagger Configuration
    builder.Services.AddSwaggerGen(options => {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme {
            Description = "Standard Authorization Header Using the Bearer Scheme (\"bearer {token}\")",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    });
#else
    builder.Services.AddSwaggerGen();
#endif
    builder.Services.AddSqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));

#if AuthMode
// Authentication Service
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options => {
            options.TokenValidationParameters = new TokenValidationParameters{
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(builder.Configuration.GetSection("JwtSettings:Secret").Value)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
#endif
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var baseResponse = new BaseResponse<string>
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = "Invalid Request",
            Data = ""
        };

        return new BadRequestObjectResult(baseResponse);
    };
});
}

var app = builder.Build();
{

}
try 
{
    var seeder = app.Services.CreateScope().ServiceProvider.GetRequiredService<IDataSeeder>();
    await seeder.SeedAdminDataAsync();
}catch (Exception e){
    Console.WriteLine($"An error occurred while seeding the database: {e.Message}");
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#if AuthMode
//Auth services app config
    app.UseAuthentication();
    app.UseAuthorization();
#endif
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();

