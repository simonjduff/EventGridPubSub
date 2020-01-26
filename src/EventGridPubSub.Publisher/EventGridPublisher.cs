using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventGridPubSub.Types;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;

namespace EventGridPubSub.Publisher
{
    public class EventGridPublisher<T> : IEventGridPublisher<T>
    where T : PubSubMessage
    {
        private readonly string _endpoint;
        private readonly string _accessKey;

        public EventGridPublisher(Topic topic, Region region, string accessKey) 
            : this($"https://{topic}.{region}-1.eventgrid.azure.net/api/events",
                accessKey)
        {
        }

        public EventGridPublisher(string endpoint,
            string accessKey)
        {
            _accessKey = accessKey;
            _endpoint = endpoint;
        }

        public async Task PublishAsync(T message)
        {
            string topicHostname = new Uri(_endpoint).Host;
            TopicCredentials topicCredentials = new TopicCredentials(_accessKey);
            EventGridClient client = new EventGridClient(topicCredentials);

            await client.PublishEventsAsync(topicHostname,
                new List<EventGridEvent>
                {
                    message
                });
        }
    }
}