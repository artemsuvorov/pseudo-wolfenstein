using System;
using System.Threading;

namespace PseudoWolfenstein.Core
{
    public static class TimeF
    {
        public static float FPS => (float)Time.FPS;
        public static float DeltaTime => (float)Time.DeltaTime;
    }

    public static class Time
    {
        public static double FPS { get; private set; }
        public static double DeltaTime { get; private set; }

        internal static void Update()
        {
            CalculateDeltaTime();
            CalculateFramesPerSecond();
        }

        // source : https://stackoverflow.com/a/6738258
        // author : https://stackoverflow.com/users/69809/groo

        private static DateTime lastCheckTime = DateTime.Now;
        private static long frameCount = 0;

        internal static void OnGlobalTick(object sender, EventArgs e)
        {
            Interlocked.Increment(ref frameCount);
        }

        private static void CalculateFramesPerSecond()
        {
            double secondsElapsed = (DateTime.Now - lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref frameCount, 0);
            double fps = count / secondsElapsed;
            lastCheckTime = DateTime.Now;
            FPS = fps;
        }

        // source : https://stackoverflow.com/q/26110228
        // author : https://stackoverflow.com/users/12019652/grave18
        private static DateTime time1 = DateTime.Now;
        private static DateTime time2 = DateTime.Now;

        private static void CalculateDeltaTime()
        {
            time2 = DateTime.Now;
            DeltaTime = (time2.Ticks - time1.Ticks) / 10000000f;
            time1 = time2;
        }
    }
}