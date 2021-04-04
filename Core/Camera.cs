using PseudoWolfenstein.Core.Raycasting;
using PseudoWolfenstein.View;
using System.Drawing;

namespace PseudoWolfenstein.Core
{
    public static class Camera
    {
        public static Rectangle ClientRectangle => GameForm.Instance.ClientRectangle;

        public static Point ScreenCenterPosition =>
            new Point(GameForm.Instance.ClientSize.Width/2, GameForm.Instance.ClientSize.Height/2);

        public static void DrawView(Graphics graphics)
        {
            //using var objectStrokePen = new Pen(Settings.GameObjectStrokeColor, Settings.ObjectStrokeWidth);
            //using var objectFillBrush = new SolidBrush(Settings.GameObjectFillColor);

            var distances = RaycastData.CrossingPointsDistances;
            if (distances is null || distances.Count <= 0) return;

            //var ceilingPoints = new PointF[distances.Count];
            //var floorPoints = new PointF[distances.Count];
            for (var i = 0; i < distances.Count; i++)
            {
                //var ceiling = ClientRectangle.Height / 2.0f + 35.0f * ClientRectangle.Height * Player.FieldOfView / distances[i];
                float dst = distances[i] / 40.0f;
                var ceiling = ClientRectangle.Height / 2.0f - ClientRectangle.Height * Player.FieldOfView / dst;
                var floor = ClientRectangle.Height - ceiling;
                //var eq = System.MathF.Abs(ceiling+floor - ClientRectangle.Height) <= System.MathF.Abs(ceiling+floor * .0001f);
                //if (eq == false) { }

                var topCeilingPointLeft   = new PointF(i     * ClientRectangle.Width/distances.Count, 0);
                var topCeilingPointRight  = new PointF((i+1) * ClientRectangle.Width/distances.Count, 0);
                var bottomFloorPointRight = new PointF((i+1) * ClientRectangle.Width/distances.Count, ClientRectangle.Height);
                var bottomFloorPointLeft  = new PointF(i     * ClientRectangle.Width/distances.Count, ClientRectangle.Height);

                var ceilingPointLeft  = new PointF(i     * ClientRectangle.Width/distances.Count, ceiling);
                var ceilingPointRight = new PointF((i+1) * ClientRectangle.Width/distances.Count, ceiling);
                var floorPointRight   = new PointF((i+1) * ClientRectangle.Width/distances.Count, floor);
                var floorPointLeft    = new PointF(i     * ClientRectangle.Width/distances.Count, floor);

                //ceilingPoints[i] = ceilingPointLeft;
                //floorPoints[i] = floorPointLeft;

                var ceilingPoints = new[] { topCeilingPointLeft, topCeilingPointRight, ceilingPointRight, ceilingPointLeft };
                var floorPoints   = new[] { floorPointLeft, floorPointRight, bottomFloorPointRight, bottomFloorPointLeft };
                var wallPoints    = new[] { ceilingPointLeft, ceilingPointRight, floorPointRight, floorPointLeft };

                //graphics.DrawPolygon(objectStrokePen, polygonPoints);
                var alpha = System.Math.Clamp(255f * (1f - dst / Settings.Depth), 0f, 255f);
                var wallColor = Color.FromArgb((int)alpha, Settings.GameObjectFillColor);
                using var wallFillBrush = new SolidBrush(wallColor);
                graphics.FillPolygon(wallFillBrush, wallPoints);

                using var ceilingFillBrush = new SolidBrush(Color.Azure);
                using var floorFillBrush = new SolidBrush(Color.Aqua);
                graphics.FillPolygon(ceilingFillBrush, ceilingPoints);
                graphics.FillPolygon(floorFillBrush, floorPoints);
            }

            //var points = ceilingPoints.Concat(floorPoints.Reverse()).ToArray();

            //graphics.DrawPolygon(objectStrokePen, points);
            //graphics.FillPolygon(objectFillBrush, points);
        }

        //public static Point ConvertAbsoluteToRelative(Point absolutePosition)
        //{
        //    var center = CenterPosition;
        //    return new Point(absolutePosition.X + center.X, center.Y - absolutePosition.Y);
        //}

        //public static Point ConvertRelativeToAbsolute(Point relativePosition)
        //{
        //    var center = CenterPosition;
        //    return new Point(relativePosition.X - center.X, center.Y - relativePosition.Y);
        //}
    }
}