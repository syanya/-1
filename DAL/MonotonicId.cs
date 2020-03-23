using System;
using System.Diagnostics;
using System.Threading;

namespace DAL
{
    public static class MonotonicId
    {
        private static readonly DateTime StartTick = new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static long count = new Random(Process.GetCurrentProcess().Id).Next(int.MaxValue);

        /// <summary>
        /// Generate a monotonic unique ID
        /// 
        /// +----------------------+--------------+--------------+
        /// | Timestamp since 2015 |    Counter   |  Machine ID  |
        /// |          (32)        |      (24)    |      (8)     |
        /// +----------------------+--------------+--------------+
        /// </summary>
        /// <returns></returns>
        public static long Generate()
        {
            long idx = (long)(DateTime.UtcNow - StartTick).TotalSeconds;
            idx = (idx % 0x100000000) << 32;
            idx = idx | ((Interlocked.Increment(ref count) % 0x1000000) << 8);

            return idx / 1000;
        }
    }
}
