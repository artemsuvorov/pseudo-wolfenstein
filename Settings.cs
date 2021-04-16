using PseudoWolfenstein.Utils;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PseudoWolfenstein
{
    public static class Settings
    {
        public const SmoothingMode MinimapSmoothingMode = SmoothingMode.None;
        public const SmoothingMode GraphicsSmoothingMode = SmoothingMode.AntiAlias;

        public const float PlayerMoveSpeed = 0.14f;
        public const float PlayerRotationSpeed = 0.05f;
        public const float PlayerFieldOfView = MathF.PI / 3.0f;
        public const int PlayerRadius = 32;

        public static readonly int RaycastRayCount = (int)(PlayerFieldOfView.ToDegrees() / RaycastRayDensity);
        public const float RaycastRayDensity =
#if DEBUG
            0.5f;
#else
            0.25f;
#endif

        public const float Depth = 128.0f;

        public const float ObjectStrokeWidth = 4.0f;

        public static readonly Color ViewportBackgroundColor = Color.FromArgb(56, 56, 56);
        public static readonly Color FormBackgroundColor = Color.FromArgb(0, 64, 64);
        public static readonly Color GameObjectFillColor = Color.FromArgb(207, 0, 49);
        public static readonly Color GameObjectStrokeColor = Color.FromArgb(128, 0, 25);

        public static readonly Color UIForegroundColor = Color.FromArgb(217, 217, 217);
        public static readonly Color UIBackgroundColor = Color.FromArgb(125, 28, 28, 28);

        public static readonly Color GizmosFillColor = Color.FromArgb(100, 0, 128, 0);
        public static readonly Color GizmosStrokeColor1 = Color.FromArgb(255, 0, 128, 0);
        public static readonly Color GizmosStrokeColor2 = Color.FromArgb(255, 0, 0, 128);
    }
}