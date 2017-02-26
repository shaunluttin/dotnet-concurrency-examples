using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

// a scheduler
public class SingleThreadSynchronizationContext
    : SynchronizationContext
{
    private readonly BlockingCollection<KeyValuePair<SendOrPostCallback, object>> _queue
        = new BlockingCollection<KeyValuePair<SendOrPostCallback, object>>();

    public override void Post(SendOrPostCallback operation, object state)
    {
        var pair = new KeyValuePair<SendOrPostCallback, object>(operation, state);
        _queue.Add(pair);
    }

    public void RunOnCurrentThread()
    {
        KeyValuePair<SendOrPostCallback, object> workItem;
        while (_queue.TryTake(out workItem, Timeout.Infinite))
        {
            workItem.Key(workItem.Value);
        }
    }

    public void Complete()
    {
        _queue.CompleteAdding();
    }
}