using System.Threading.Tasks;
using Microsoft.Azure.EventGrid.Models;

namespace EventGridPubSub.Publisher
{
    public interface IEventGridPublisher<in T>
        where T : EventGridEvent
    {
        Task PublishAsync(T message);
    }
}