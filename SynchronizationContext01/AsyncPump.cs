// a message pump
using System;
using System.Threading;
using System.Threading.Tasks;

public class AsyncPump
{
    public static void Run(Func<Task> func)
    {
        var previousContext = SynchronizationContext.Current;

        try
        {
            var contextToUse = new SingleThreadSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(contextToUse);

            var task = func();
            task.ContinueWith(
                (t, o) => contextToUse.Complete(),
                TaskScheduler.Default);

            contextToUse.RunOnCurrentThread();
            task.GetAwaiter().GetResult();
        }
        finally
        {
            SynchronizationContext.SetSynchronizationContext(previousContext);
        }
    }
}