using System;
using System.Diagnostics;
using System.Threading;

namespace Bottles.Services.Embedded.Tests
{
    public static class Wait
    {
        public static void Until(Func<bool> condition, int millisecondPolling = 500, int timeoutInMilliseconds = 5000)
        {
            if (condition()) return;

            var clock = new Stopwatch();
            clock.Start();

            while (clock.ElapsedMilliseconds < timeoutInMilliseconds)
            {
                Thread.Yield();
                Thread.Sleep(millisecondPolling);

                if (condition()) return;
            }
        }
    }
}