using PseudoWolfenstein.Core.Raycasting;
using System;
using System.Drawing;

namespace PseudoWolfenstein.Core
{
    public class Camera
    {
        private readonly Viewport viewport;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void DrawView(Graphics graphics)
        {
            var distances = RaycastData.CrossingPointsDistances;
            if (distances is null || distances.Count <= 0) return;

            var sliceCount = distances.Count;
            var sliceWidth = MathF.Ceiling(viewport.Width / (float)sliceCount) + 2.0f;
            for (var i = 0; i < sliceCount; i++)
            {
                float dst = distances[i] / 64.0f;
                var ceiling = (int)MathF.Round(viewport.Height*0.5f - viewport.Height/dst);
                var floor = viewport.Height - ceiling;
                var wallHeight = floor - ceiling;
                var gap = 0.5f * (viewport.Width - sliceCount * (sliceWidth-0.5f));
                var wallRectangle = new RectangleF(gap + i*(sliceWidth-1f), ceiling, sliceWidth, wallHeight);

                using var wallFillBrush = new SolidBrush(Settings.GameObjectFillColor);
                graphics.FillRectangle(wallFillBrush, wallRectangle);
            }
        }
    }
}