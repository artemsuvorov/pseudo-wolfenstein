using PseudoWolfenstein.Core;
using PseudoWolfenstein.Core.Raycasting;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    internal class Gizmos
    {
#if DEBUG
        private static readonly bool isGizmosEnabledByDefault = true;
#else
        private static readonly bool isGizmosEnabledByDefault = false;
#endif

        public static bool Enabled { get; private set; }

        public static void Update()
        {
            if (Input.IsKeyToggled(Keys.F4))
                Enabled = !isGizmosEnabledByDefault;
            else
                Enabled = isGizmosEnabledByDefault;
        }

        internal void Redraw(object sender, PaintEventArgs e)
        {
            if (!Enabled) return;

            DrawVisibleArea(e.Graphics);
            //DrawPlayerFoV(e.Graphics);
            //DrawVectors(e.Graphics);
            //DrawRays(e.Graphics);
            DrawPostions(e.Graphics);
            DrawCrossingPosition(e.Graphics);
        }

        private void DrawVisibleArea(Graphics graphics)
        {
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);
            using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1);

            var centerPoint = Camera.ScreenCenterPosition;

            var points = RaycastData.CrossingPoints.Select(vec => new PointF(centerPoint.X + vec.X, centerPoint.Y + vec.Y))
                .Concat(new[] { new PointF(centerPoint.X + Player.X, centerPoint.Y + Player.Y) }).ToArray();

            graphics.DrawPolygon(gizmosStrokePen1, points);
            graphics.FillPolygon(gizmosFillBrush, points);
        }

        private void DrawRays(Graphics graphics)
        {
            using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1);

            var centerPoint = Camera.ScreenCenterPosition;

            foreach (var ray in RaycastData.Rays)
            {
                var start = new PointF(centerPoint.X + ray.Start.X, centerPoint.Y + ray.Start.Y);
                var end = new PointF(centerPoint.X + ray.End.X, centerPoint.Y + ray.End.Y);
                graphics.DrawLine(gizmosStrokePen1, start, end);
            }
        }

        private void DrawCrossingPosition(Graphics graphics)
        {
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);

            var centerPoint = Camera.ScreenCenterPosition;

            foreach (var crossingPoint in RaycastData.CrossingPoints)
                graphics.FillEllipse(gizmosFillBrush, centerPoint.X + crossingPoint.X - 5, centerPoint.Y + crossingPoint.Y - 5, 10, 10);
        }

        private void DrawPlayerFoV(Graphics graphics)
        {
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);
            using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1);

            var fovRadius = 480f;
            var x = Camera.ClientRectangle.Width/2f + Player.Position.X - fovRadius/2f;
            var y = Camera.ClientRectangle.Height/2f + Player.Position.Y - fovRadius/2f;
            var r = MathF2D.ToDegrees(Player.Rotation);
            var fov = MathF2D.ToDegrees(Player.FieldOfView);
            graphics.FillPie(gizmosFillBrush, x, y, fovRadius, fovRadius, r-fov/2f, fov);
            graphics.DrawPie(gizmosStrokePen1, x, y, fovRadius, fovRadius, r-fov/2f, fov);
        }

        private void DrawVectors(Graphics graphics)
        {
            using var gizmosStrokePen2 = new Pen(Settings.GizmosStrokeColor2, width: 8f);

            graphics.DrawLine(gizmosStrokePen2,
                Camera.ClientRectangle.Width/2 + Player.X, Camera.ClientRectangle.Height/2 + Player.Y, 
                Player.MotionDirection.X*100 + Camera.ClientRectangle.Width/2 + Player.X, 
                Player.MotionDirection.Y*100 + Camera.ClientRectangle.Height/2 + Player.Y);
        }

        private void DrawPostions(Graphics graphics)
        {
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);

            var centerPoint = Camera.ScreenCenterPosition;
            var center = new System.Numerics.Vector2(centerPoint.X, centerPoint.Y);

            var relMousePos = Input.RelMousePosition;
            var target = new System.Numerics.Vector2(relMousePos.X, relMousePos.Y);

            var t1 = (System.Numerics.Vector2.UnitX + center + Player.Position);
            var t2 = (target);

            graphics.FillEllipse(gizmosFillBrush, t1.X - 5, t1.Y - 5, 10, 10);
            graphics.FillEllipse(gizmosFillBrush, t2.X - 5, t2.Y - 5, 10, 10);
        }
    }
}