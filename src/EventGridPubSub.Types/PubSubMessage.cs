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
            // ReSharper disable once JoinNullCheckWithUsage
            if (payload == null)
            {
                throw new ArgumentNullException(nameof(payload));
            }

            Id = id;
            EventTime = DateTime.UtcNow;
            EventType = this.GetType().Name;
            Subject = subject;
            DataVersion = version.ToString();
            Data = payload;
        }
    }
}