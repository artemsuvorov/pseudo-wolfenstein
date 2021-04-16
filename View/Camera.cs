using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System;
using System.Drawing;

namespace PseudoWolfenstein.View
{
    public class Camera
    {
        private readonly Viewport viewport;
        private readonly Raycast raycast;

        private readonly Image texture;

        public Camera(Viewport viewport, Scene scene)
        {
            this.viewport = viewport;
            this.raycast = new Raycast(scene);

            var path = Repository.GetTexturePath("WALL14.bmp");
            texture = Image.FromFile(path);
        }

        ~Camera()
        {
            texture.Dispose();
        }

        public void DrawView(Graphics graphics)
        {
            using var backgroundBrush = new SolidBrush(Settings.ViewportBackgroundColor);
            graphics.FillRectangle(backgroundBrush, viewport.ClientRectangle);
            graphics.TranslateTransform(viewport.X, viewport.Y);

            var raycastData = raycast.GetCrossingPointsAndDistances();
            var distances = raycastData.CrossingPointsDistances;
            if (distances is null || distances.Length <= 0) return;

            var sliceCount = distances.Length;
            var sliceWidth = viewport.Width / (float)sliceCount + 1f;
            for (var i = 0; i < sliceCount; i++)
            {
                float dst = distances[i];
                var ceiling = (int)MathF.Max(0, MathF.Round(viewport.Height * 0.5f - Player.FieldOfView * viewport.Height / dst));
                var floor = viewport.Height - ceiling;
                var wallHeight = floor - ceiling;
                var wallRectangle = new RectangleF(0.5f + i * (sliceWidth - 1f), ceiling, sliceWidth, wallHeight);

                using var wallFillBrush = new SolidBrush(Settings.GameObjectFillColor);
                graphics.FillRectangle(wallFillBrush, wallRectangle);
            }

            //DrawWallTextureInCenter(graphics, 16);
            graphics.ResetTransform();
        }

        //private void DrawWallTextureInCenter(Graphics graphics, int sliceCount)
        //{
        //    var center = viewport.Center - texture.Size / 2;
        //    var sliceWidth = texture.Width / sliceCount;

        //    for (var i = 0; i < sliceCount; i++)
        //    {
        //        var destRect = new Rectangle(center.X+sliceWidth*i, center.Y-i*2, sliceWidth, texture.Height+i*4);
        //        var sourceRect = new Rectangle(sliceWidth*i, 0, sliceWidth, texture.Height);
        //        graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
        //    }
        //}
    }
}