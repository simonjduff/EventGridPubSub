using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventGridPubSub.Publisher;
using Microsoft.Azure.EventGrid.Models;
using Xunit;

namespace EventGrid.Tests.InMemoryMock
{
    public class InMemoryMockTests
    {
        [Fact]
        public async Task Published_message_can_be_retrieved()
        {
            // Given a test message
            var message = new TestMessage(Guid.NewGuid().ToString());

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
        }

        public class TestMessage : EventGridEvent
        {
            public TestMessage(string id)
            {
                Id = id;
            }
        }
    }

    public class InMemoryEventGridPublisher<T> : IEventGridPublisher<T>
    where T : EventGridEvent
    {
        private readonly List<Action<T>> _callbacks = new List<Action<T>>();

        public void Subscribe(Action<T> callback)
        {
            _callbacks.Add(callback);
        }

        public Task PublishAsync(T message)
        {
            foreach (var callback in _callbacks)
            {
                callback(message);
            }

            return Task.CompletedTask;
        }
    }
}
