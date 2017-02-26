using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        DefaultContext();
        SingleThreadedContext();
    }

    public static void SingleThreadedContext()
    {
        Console.WriteLine("SingleThreadedContext");
        AsyncPump.Run(async () => await DoWorkAsync());
    }

    public static void DefaultContext()
    {
        Console.WriteLine("DefaultContext");
        DoWorkAsync().Wait();
    }

    private static async Task DoWorkAsync()
    {
        const int iterations = 1000000;
        const int graphScale = 10000;

        var dictionary = new Dictionary<int, int>();
        for (var i = 0; i < iterations; i = i + 1)
        {
            var threadId = Thread.CurrentThread.ManagedThreadId;
            int count;
            dictionary[threadId] = dictionary.TryGetValue(threadId, out count)
                ? (count + 1)
                : 1;

            await Task.Yield();
        }

        foreach (var pair in dictionary)
        {
            Console.WriteLine($"{pair.Key} {new string('.', pair.Value / graphScale)}");
        }
    }
}