using System;
using Microsoft.Azure.EventGrid.Models;

namespace EventGridPubSub.Types
{
    public class PubSubMessage<T> : EventGridEvent
    where T : class
    {
        public PubSubMessage(string id,
            string subject,
            MessageVersion version,
            T payload)
        {
            Id = id;
            EventTime = DateTime.UtcNow;
            EventType = this.GetType().Name;
            Subject = subject;
            DataVersion = version.ToString();
            Data = payload;
        }
    }
}