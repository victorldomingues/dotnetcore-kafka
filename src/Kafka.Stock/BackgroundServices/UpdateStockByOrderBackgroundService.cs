using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Kafka.Stock.BackgroundServices
{
    public class UpdateStockByOrderBackgroundService : BackgroundService
    {

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bootstrapServers = "localhost:9092";
            var topic = "orders";

            

            try
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = bootstrapServers,
                    GroupId = $"{topic}-group-00",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                };

                using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

                consumer.Subscribe(topic);


                while (true)
                {
                    var consume = consumer.Consume(stoppingToken);
                    if (!string.IsNullOrEmpty(consume?.Message?.Value))
                        Console.WriteLine(consume.Message.Value);

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
    }
}
