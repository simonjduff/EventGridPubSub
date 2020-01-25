using System;
using System.Threading.Tasks;
using EventGridPubSub.Publisher;
using EventGridPubSub.Types;
using Microsoft.Azure.EventGrid.Models;
using Xunit;

namespace EventGrid.Tests.Publisher
{
    public class PublishTests
    {
        [Fact]
        public async Task Publisher_can_publish_event()
        {
            // Given a publisher
            const string topic = "EventGridPubSubTopic";
            const string region = "https://eventgridpsubsubtopic.westeurope-1.eventgrid.azure.net/api/events";
            IEventGridPublisher<TestMessage> publisher = new EventGridPublisher<TestMessage>(topic, region);

            // And a message
            var message = new TestMessage();

            // When I publish a message
            await publisher.PublishAsync(message);

            // Then the message is published
        }
    }

    public class TestMessage : EventGridEvent
    {
        public TestMessage()
        {
            EventTime = DateTime.UtcNow;
            EventType = nameof(TestMessage);
            Id = Guid.NewGuid().ToString();
            Data = null;
            Subject = "Publisher_can_publish_event";
            DataVersion = "0.0.1";
        }
    }

    public class EventGridPublisher<T> : IEventGridPublisher<T>
    where T : EventGridEvent
    {
        public EventGridPublisher(Topic topic, Region region) : this($"https://{topic}.{region}-1.eventgrid.azure.net/api/events")
        {
        }

        public EventGridPublisher(string endpoint)
        {
            
        }

        public Task PublishAsync(T message)
        {
            throw new System.NotImplementedException();
        }
    }
}
