using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExtensions.NetCore.Async
{
    public static class Extensions
    {
        public static void FireAndForget(this Task task, Action<Exception> errorHandler = null)
        {
            task.ContinueWith(t =>
            {
                if (t.IsFaulted && errorHandler != null)
                    errorHandler(t.Exception);
            }, TaskContinuationOptions.OnlyOnFaulted);
        }

        public static async Task<TResult> Retry<TResult>(this Func<Task<TResult>> taskFactory, int maxRetries, TimeSpan delay)
        {
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    return await taskFactory().ConfigureAwait(false);
                }
                catch
                {
                    if (i == maxRetries - 1)
                        throw;
                    await Task.Delay(delay).ConfigureAwait(false);
                }
            }

            return default(TResult);
        }

        public static async Task OnFailure(this Task task, Action<Exception> onFailure)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                onFailure(ex);
            }
        }

        public static async Task WithTimeout(this Task task, TimeSpan timeout)
        {
            var delayTask = Task.Delay(timeout);
            var completedTask = await Task.WhenAny(task,delayTask);
            if (completedTask == delayTask)
                throw new TimeoutException();

            await task;
        }

        public static async Task<TResult> Fallback<TResult>(this Task<TResult> task, TResult fallbackValue)
        {
            try
            {
                return await task.ConfigureAwait(false);
            }
            catch
            {
                return fallbackValue;
            }
        }
    }
}
