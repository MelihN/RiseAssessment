using Confluent.Kafka;
using MongoDB.Bson.Serialization.Serializers;
using RaModels;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace RAUI.ServiceHelper
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly string topic = "personreport";
        private readonly string groupId = "reportgroup";
        private readonly string bootstrapServers;

        public KafkaConsumerService(IConfiguration configuration)
        {
            bootstrapServers = configuration.GetValue<string>("KafkaUrl");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig { GroupId = "reportgroup", BootstrapServers = bootstrapServers };

            using (var consumer = new ConsumerBuilder<string, string>(config).Build())
            {
                consumer.Subscribe(topic);
                while (true)
                {
                    ConsumeResult<string, string> consumeResult = consumer.Consume();

                    consumer.Commit();
                }
            }
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
