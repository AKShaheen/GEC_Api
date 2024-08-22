using GEC.Runtime.Connections;
using GEC.Runtime.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddInfrastructureServices()
        .AddBusinessServices()
        .AddValidationServices();

    builder.Services.AddControllers();
    
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
}

var app = builder.Build();
{

}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

