using System.Drawing;
using System.Drawing.Drawing2D;

namespace PseudoWolfenstein.Core
{
    public static class Settings
    {
        public const SmoothingMode MinimapSmoothingMode = SmoothingMode.None;
        public const SmoothingMode GraphicsSmoothingMode = SmoothingMode.AntiAlias;

        public const float RaycastRayDencity =
#if DEBUG
            2.0f;
#else
            0.25f;
#endif

        public const float Depth = 128.0f;

        public const float ObjectStrokeWidth = 4.0f;
        public const int PlayerRadius = 32;

        public static readonly Color BackgroundColor = Color.FromArgb(38, 38, 38);
        public static readonly Color GameObjectFillColor = Color.FromArgb(207, 0, 49);
        public static readonly Color GameObjectStrokeColor = Color.FromArgb(128, 0, 25);

        public static readonly Color UIForegroundColor = Color.FromArgb(217, 217, 217);
        public static readonly Color UIBackgroundColor = Color.FromArgb(125, 28, 28, 28);

        public static readonly Color GizmosFillColor = Color.FromArgb(100, 0, 128, 0);
        public static readonly Color GizmosStrokeColor1 = Color.FromArgb(255, 0, 128, 0);
        public static readonly Color GizmosStrokeColor2 = Color.FromArgb(255, 0, 0, 128);

    }
}