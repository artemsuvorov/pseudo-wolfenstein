using System;
using System.Drawing;

namespace PseudoWolfenstein.Core
{
    public class Camera
    {
        private readonly Viewport viewport;
        private readonly Raycast raycast;

        public Camera(Viewport viewport, Scene scene)
        {
            this.viewport = viewport;
            this.raycast = new Raycast(scene);
        }

        public void DrawView(Graphics graphics)
        {
            var raycastData = raycast.GetCrossingPointsAndDistances();
            var distances = raycastData.CrossingPointsDistances;
            if (distances is null || distances.Length <= 0) return;

            var sliceCount = distances.Length;
            var sliceWidth = MathF.Ceiling(viewport.Width / (float)sliceCount) + 2.0f;
            for (var i = 0; i < sliceCount; i++)
            {
                float dst = distances[i] / 64.0f;
                var ceiling = (int)MathF.Round(viewport.Height*0.5f - Player.FieldOfView*viewport.Height/dst);
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