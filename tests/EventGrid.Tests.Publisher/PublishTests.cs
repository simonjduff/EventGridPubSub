using System;
using System.IO;
using System.Threading.Tasks;
using EventGridPubSub.Publisher;
using EventGridPubSub.Types;
using Microsoft.Azure.Cosmos.Table;
using Xunit;
using EventGrid.Tests.Publisher.Utils;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace EventGrid.Tests.Publisher
{
    public class PublishTests
    {
        public PublishTests()
        {
            var secretsJson = "secrets.json";
            ConfigurationBuilder config = new ConfigurationBuilder();

            if (File.Exists(secretsJson))
            {
                config.AddJsonFile(secretsJson);
            }

            config.AddEnvironmentVariables();

            Config = config.Build();
        }

        private IConfiguration Config;

        [Fact]
        public async Task Publisher_can_publish_event()
        {
            // Given a publisher
            Topic topic = "eventgridpubsubtesttopic";
            Region region = "northeurope";
            IEventGridPublisher<TestMessage> publisher = new EventGridPublisher<TestMessage>(topic, region, Config["eventKey"]);

            // And a message
            var message = new TestMessage();
            
            // When I publish a message
            await publisher.PublishAsync(message);

            // Then the message is published
            var reader = new TableReader<TestMessageRow>(
                Config["connectionString"]);
            var row = await Patiently.WaitAsync(() => reader.ReadDataAsync(message.Id), TimeSpan.FromSeconds(10));
            Assert.NotNull(row);
            Assert.Equal(message.Id, ((TestMessage.Payload)row.Message.Data).Id.ToString());
        }
    }

    public class TestMessage : PubSubMessage
    {
        public TestMessage(Guid id) : base(id.ToString(),
            "Publisher_can_publish_event",
            "0.0.1",
            new Payload(id))
        {
        }

        public TestMessage( ): this(Guid.NewGuid())
        {
        }
        public class Payload
        {
            public Payload()
            {
            }

            public Payload(Guid id)
            {
                Id = id;
            }
            public Guid Id { get; set; }
        }
    }

    public class TestMessageRow : TableEntity
    {
        public string Payload { get; set; }
        public TestMessage Message => JsonConvert.DeserializeObject<TestMessage>(Payload);
    }
}
