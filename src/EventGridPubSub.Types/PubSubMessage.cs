using System;
using Microsoft.Azure.EventGrid.Models;

namespace EventGridPubSub.Types
{
    public abstract class PubSubMessage : EventGridEvent
    {
        protected PubSubMessage(string id,
            string subject,
            MessageVersion version,
            object payload)
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