using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PseudoWolfenstein.View
{
    public class Camera
    {
        private readonly Scene scene;
        private readonly Raycast raycast;
        private readonly float viewWidth, viewHeight;

        public Camera(Viewport viewport, Scene scene, Player player)
        {
            this.scene = scene;
            this.raycast = new Raycast(scene, player);

            viewWidth = viewport.Width;
            viewHeight = viewport.Height - 100f;
        }

        public void DrawView(Graphics graphics)
        {
            DrawBackground(graphics);

            var raycastData = raycast.CastRaysAt(scene.Obstacles);
            DrawObstacles(graphics, raycastData);

            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.CompositingMode = CompositingMode.SourceOver;
        }

        private void DrawObstacles(Graphics graphics, RaycastData raycastData)
        {
            if (raycastData is null || raycastData.Count <= 0) return;

            var sliceCount = raycastData.Count;
            var sliceWidth = viewWidth / sliceCount;

            for (var i = 0; i < sliceCount; i++)
                DrawCrossingPoints(graphics, raycastData, i, sliceWidth);
        }

        private void DrawBackground(Graphics graphics)
        {
            using var ceilingBrush = new SolidBrush(Settings.CeilingColor);
            using var floorBrush = new SolidBrush(Settings.FloorColor);
            var ceilingRect = new RectangleF(0, 0, viewWidth, viewHeight / 2f);
            var floorRect = new RectangleF(0, viewHeight / 2f, viewWidth, viewHeight);
            graphics.FillRectangle(ceilingBrush, ceilingRect);
            graphics.FillRectangle(floorBrush, floorRect);
        }

        private float CalculateCeiling(float totalDistance)
        {
            var dst = totalDistance * Settings.RaycastProjectionCoeff / Settings.WorldWallSize;
            var ceiling = 0.5f * viewHeight - Player.FieldOfView * viewHeight / dst;
            return ceiling;
        }

        private float CalculateWallHeight(float ceiling)
        {
            return viewHeight - 2.0f * ceiling;
        }

        private void DrawCrossingPoints(Graphics graphics, RaycastData raycastData, int i, float sliceWidth)
        {
            if (raycastData[i] is null || raycastData[i].Count <= 0)
                return;

            if (raycastData[i][0].CrossedObstacle is Wall || raycastData[i][0].CrossedObstacle is Door)
            {
                graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                graphics.CompositingMode = CompositingMode.SourceCopy;
                DrawSlice(graphics, raycastData[i][0], i, sliceWidth);
                return;
            }

            graphics.InterpolationMode = InterpolationMode.Bilinear;
            graphics.CompositingMode = CompositingMode.SourceOver;
            for (var j = raycastData[i].Count - 1; j >= 0; j--)
            {
                if (raycastData[i][j] is null) return;
                DrawSlice(graphics, raycastData[i][j], i, sliceWidth);
            }

            //graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            //graphics.CompositingMode = CompositingMode.SourceCopy;
            //DrawSlice(graphics, raycastData[i][^1], i, sliceWidth);
            //graphics.InterpolationMode = InterpolationMode.Low;
            //graphics.CompositingMode = CompositingMode.SourceOver;
            //for (var j = raycastData[i].Count - 2; j >= 0; j--)
            //{
            //    if (raycastData[i][j] is null) return;
            //    DrawSlice(graphics, raycastData[i][j], i, sliceWidth);
            //}
        }

        private void DrawSlice(Graphics graphics, Cross crossData, int i, float sliceWidth)
        {
            if (crossData is null || !crossData.Exists) return;
            var ceiling = CalculateCeiling(crossData.Distance);
            var wallHeight = CalculateWallHeight(ceiling);

            var texture = crossData.CrossedObstacle.Texture;
            var crossingPoint = crossData.Location;
            var crossedSide = crossData.CrossedSide;

            var edgeStart = crossData.CrossedObstacle.Vertices[0];
            var wallCrossX = crossedSide == SideDirection.Vertical ? 
                edgeStart.Y - crossingPoint.Y : edgeStart.X - crossingPoint.X;
            var offsetX = MathF.Abs(wallCrossX) / Settings.WorldWallSize;

            var d = ceiling * 32f - viewHeight * 16f + wallHeight * 16f;
            var texX = offsetX * texture.Width;
            var texY = d * texture.Height / wallHeight / 32f;

            var destRect = new RectangleF(i * sliceWidth, ceiling, sliceWidth, wallHeight);
            var sourceRect = new RectangleF(texX, texY, 1f, texture.Height);
            graphics.DrawImage(texture, destRect, sourceRect, GraphicsUnit.Pixel);
        }
    }
}