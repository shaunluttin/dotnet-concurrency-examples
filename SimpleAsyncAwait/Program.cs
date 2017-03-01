using System;
using System.Threading;
using System.Threading.Tasks;

public class Program
{
    private static int NormalStaticField;

    private static ThreadLocal<int> ThreadLocalStaticField = new ThreadLocal<int>();

    public static void Main()
    {
        DoAsync().Wait();
    }

    public static async Task DoAsync()
    {
        var methodVariable = 0;
        NormalStaticField = 0;
        ThreadLocalStaticField.Value = 0;

        methodVariable++;
        NormalStaticField++;
        ThreadLocalStaticField.Value++;

        await Task.Delay(1000);

        // The continuation happens in "any" thread.
        // Normally thread-specific data is not put into the state machine.

        methodVariable++;
        NormalStaticField++;
        ThreadLocalStaticField.Value++;

        Console.WriteLine($"MethodVariable: {NormalStaticField}");
        Console.WriteLine($"NormalStaticField: {NormalStaticField}");
        Console.WriteLine($"ThreadLocalStaticField: {ThreadLocalStaticField}");
    }
}

