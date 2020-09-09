using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kafka.Order.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kafka.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderViewModel order)
        {
            var bootstrapServers = "localhost:9092";
            var topic = "orders";

            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = $"{topic}-group-00",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
           
            await producer.ProduceAsync(topic, new Message<Null, string>()
                {
                    Value = JsonSerializer.Serialize(order)
                }
            );

            return Ok();
        }
        [HttpGet, Route("ping")]
        public async Task<IActionResult> Ping(OrderViewModel order)
        {
            return Ok("pong");
        }
    }
}
