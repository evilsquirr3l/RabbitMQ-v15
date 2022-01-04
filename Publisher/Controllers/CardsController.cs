using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Publisher.Models;
using RabbitMQ.Client;

namespace Publisher.Controllers;

[ApiController]
[Route("[controller]")]
public class CardsController : ControllerBase
{
    private readonly RabbitMqConfiguration _configuration;

    public CardsController(IOptions<RabbitMqConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    [HttpPost]
    public IActionResult CreateCard(OldCard card)
    {
        ValidateModel(card);
        PublishMessage(JsonConvert.SerializeObject(card));
        
        return Ok("Message is sent!");
    }

    private void ValidateModel(OldCard card)
    {
        if (card.PublishingYear < 1900)
        {
            throw new ArgumentException("Invalid publishing year!");
        }

        if (card.Author == "Jack London")
        {
            throw new ArgumentException("This is not an author!");
        }
    }

    private void PublishMessage(string card)
    {
        var factory = new ConnectionFactory
        {
            HostName = _configuration.Hostname,
            UserName = _configuration.UserName,
            Password = _configuration.Password
        };
        
        using var connection = factory.CreateConnection();
        using var model = connection.CreateModel();

        model.QueueDeclare(_configuration.QueueName, false, false, false, null);

        var body = Encoding.UTF8.GetBytes(card);

        model.BasicPublish(string.Empty, _configuration.QueueName, null, body);
    }
}