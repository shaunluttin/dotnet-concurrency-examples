using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();

            // TODO: run DoWorkAsync as a safe fire-and-forget operation
            program.DoWorkAsync().Wait();
        }

        private async Task DoWorkAsync()
        {
            await Task.Yield();
        }
    }
}
