using System;
using System.Threading;
using System.Threading.Tasks;

namespace EventGrid.Tests.Publisher.Utils
{
    public static class Patiently
    {
        public static async Task<T> WaitAsync<T>(Func<Task<T>> function, CancellationToken token)
        where T : class

        {
            T result = null;
            while (!token.IsCancellationRequested && result == null)
            {
                result = await function();

                if (result == null)
                {
                    await Task.Delay(100, token);
                }
            }

            return result;
        }

        public static Task<T> WaitAsync<T>(Func<Task<T>> function, TimeSpan timeout)
        where T : class
        {
            var cancellation = new CancellationTokenSource(timeout);
            return WaitAsync<T>(function, cancellation.Token);
        }
    }
}
