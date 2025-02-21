using CommonService.Utility;
using MasterServiceDemo.Utility;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Swagger Custom Documentation with Authorize the header functionality
builder.Services.AddSwaggerDocumentation();

//run RabbitMq by default and if there is any task in the queue complete it.
builder.Services.AddHostedService<RabbitMQBackgroundService>();


// add the utility servicetoscoped folder 
ServiceToScope oServiceToScope = new ServiceToScope(builder.Configuration);
oServiceToScope.AddToScope(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
