using System;
using System.Threading.Tasks;
using EventGridPubSub.Publisher;
using EventGridPubSub.Types;
using Xunit;

namespace EventGrid.Tests.Publisher
{
    public class PublishTests
    {
        [Fact]
        public async Task Publisher_can_publish_event()
        {
            // Given a publisher
            Topic topic = "eventgridpubsubtesttopic";
            Region region = "northeurope";
            IEventGridPublisher<TestMessage> publisher = new EventGridPublisher<TestMessage>(topic, region, "hR/7WV5IC4EorEorOll5CeXHUD6Kk2nnR72kVpDjlk4=");

            // And a message
            var message = new TestMessage();

            // When I publish a message
            await publisher.PublishAsync(message);

            // Then the message is published
        }
    }

    public class TestMessage : PubSubMessage
    {
        public TestMessage() : base(Guid.NewGuid().ToString(),
            "Publisher_can_publish_event",
            "0.0.1",
            new object())
        {
        }
    }
}
