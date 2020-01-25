using System;
using System.Threading.Tasks;
using EventGridPubSub.InMemoryMock;
using EventGridPubSub.Types;
using Xunit;

namespace EventGrid.Tests.InMemoryMock
{
    public class InMemoryMockTests
    {
        [Fact]
        public async Task Published_message_can_be_retrieved()
        {
            // Given a test message
            var message = new TestMessage(Guid.NewGuid().ToString(), 
                "Published_message_can_be_retrieved", 
                "0.0.1",
                new TestMessage.TestMessagePayload
                {
                    Value = "TestValue"
                });

            // and an in memory pub sub publisher
            InMemoryEventGridPublisher<TestMessage> publisher = new InMemoryEventGridPublisher<TestMessage>();
            TestMessage retrievedMessage = null;
            publisher.Subscribe(m =>
            {
                if (nameof(TestMessage).Equals(m.EventType) && message.Id.Equals(m.Id))
                {
                    retrievedMessage = m;
                }
            });

            // When the message is published
            await publisher.PublishAsync(message);

            // Then the message can be retrieved
            Assert.NotNull(retrievedMessage);
            Assert.Equal(message.Id, retrievedMessage.Id);
            Assert.Equal("TestValue", ((TestMessage.TestMessagePayload)retrievedMessage.Data).Value);
        }

        public class TestMessage : PubSubMessage
        {
            public TestMessage(string id, 
                string subject, 
                MessageVersion version, 
                TestMessagePayload payload) : base(id, subject, version, payload)
            {
            }

            public class TestMessagePayload
            {
                public string Value { get; set; }
            }
        }
    }
}
