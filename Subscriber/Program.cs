using EventBus.RabbitMQ.Standard.Configuration;
using EventBus.RabbitMQ.Standard.Options;
using Microsoft.EntityFrameworkCore;
using Subscriber;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigureRabbitMq();

void ConfigureRabbitMq()
{
    builder.Services.AddOptions();
    builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMq"));
    var rabbitMqOptions = builder.Configuration.GetSection("RabbitMq").Get<RabbitMqOptions>();

    builder.Services.AddRabbitMqConnection(rabbitMqOptions);
    builder.Services.AddRabbitMqRegistration(rabbitMqOptions);
}

builder.Services.AddDbContextFactory<CardsDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("CardsDb")));


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