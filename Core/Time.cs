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
        private static DateTime lastCheckTime = DateTime.Now;
        private static long lastCheckTimeTicks = DateTime.Now.Ticks;
        private static long frameCount = 0;

        public static double FPS => CalculateFramesPerSecond();
        public static double DeltaTime => CalculateDeltaTime();

        // source : https://stackoverflow.com/a/6738258
        // author : https://stackoverflow.com/users/69809/groo

        internal static void OnGlobalTick(object sender, EventArgs e)
        {
            Interlocked.Increment(ref frameCount);
        }

        private static double CalculateFramesPerSecond()
        {
            double secondsElapsed = (DateTime.Now - lastCheckTime).TotalSeconds;
            long count = Interlocked.Exchange(ref frameCount, 0);
            double fps = count / secondsElapsed;
            lastCheckTime = DateTime.Now;
            return fps;
        }

        // source : https://stackoverflow.com/q/26110228
        // author : https://stackoverflow.com/users/4092928/kwg

        private static double CalculateDeltaTime()
        {
            long now = DateTime.Now.Ticks;
            double deltaTime = (now - lastCheckTimeTicks) / 1000000.0;
            lastCheckTimeTicks = now;
            return deltaTime;
        }
    }
}