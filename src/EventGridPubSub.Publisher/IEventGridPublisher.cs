using System.Threading.Tasks;
using EventGridPubSub.Types;

namespace EventGridPubSub.Publisher
{
    public interface IEventGridPublisher<in T>
        where T : PubSubMessage
    {
        Task PublishAsync(T message);
    }
}