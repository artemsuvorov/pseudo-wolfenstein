using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
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

        private readonly Player player;
        private readonly Input input;
        private readonly Viewport viewport;

        public Gizmos(Viewport viewport, Input input, Player player)
        {
            this.viewport = viewport;
            this.input = input;
            this.player = player;
        }

        public void Update()
        {
            if (input.IsKeyToggled(Keys.F4))
                Enabled = !isGizmosEnabledByDefault;
            else
                Enabled = isGizmosEnabledByDefault;
        }

        internal void Redraw(object sender, PaintEventArgs e)
        {
            if (!Enabled) return;

            //DrawVisibleArea(e.Graphics);
            //DrawPlayerFoV(e.Graphics);
            //DrawVectors(e.Graphics);
            //DrawRays(e.Graphics);
            DrawPostions(e.Graphics);
            //DrawCrossingPosition(e.Graphics);
        }

        //private void DrawVisibleArea(Graphics graphics)
        //{
        //    using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);
        //    using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1);

        //    var centerPoint = viewport.Center;

        //    var points = RaycastData.CrossingPoints.Select(vec => new PointF(centerPoint.X + vec.X, centerPoint.Y + vec.Y))
        //        .Concat(new[] { new PointF(centerPoint.X + player.X, centerPoint.Y + player.Y) }).ToArray();

        //    graphics.DrawPolygon(gizmosStrokePen1, points);
        //    graphics.FillPolygon(gizmosFillBrush, points);
        //}

        //private void DrawRays(Graphics graphics)
        //{
        //    using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1);

        //    var centerPoint = viewport.Center;

        //    foreach (var ray in RaycastData.Rays)
        //    {
        //        var start = new PointF(centerPoint.X + ray.Start.X, centerPoint.Y + ray.Start.Y);
        //        var end = new PointF(centerPoint.X + ray.End.X, centerPoint.Y + ray.End.Y);
        //        graphics.DrawLine(gizmosStrokePen1, start, end);
        //    }
        //}

        //private void DrawCrossingPosition(Graphics graphics)
        //{
        //    using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);

        //    var centerPoint = viewport.Center;

        //    foreach (var crossingPoint in RaycastData.CrossingPoints)
        //        graphics.FillEllipse(gizmosFillBrush, centerPoint.X + crossingPoint.X - 5, centerPoint.Y + crossingPoint.Y - 5, 10, 10);
        //}

        private void DrawPlayerFoV(Graphics graphics)
        {
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);
            using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1);

            var fovRadius = 480f;
            var x = viewport.ClientRectangle.Width/2f + player.Position.X - fovRadius/2f;
            var y = viewport.ClientRectangle.Height/2f + player.Position.Y - fovRadius/2f;
            var r = player.Rotation.ToDegrees();
            var fov = Player.FieldOfView.ToDegrees();
            graphics.FillPie(gizmosFillBrush, x, y, fovRadius, fovRadius, r-fov/2f, fov);
            graphics.DrawPie(gizmosStrokePen1, x, y, fovRadius, fovRadius, r-fov/2f, fov);
        }

        private void DrawVectors(Graphics graphics)
        {
            using var gizmosStrokePen2 = new Pen(Settings.GizmosStrokeColor2, width: 8f);

            graphics.DrawLine(gizmosStrokePen2,
                viewport.ClientRectangle.Width/2 + player.X, viewport.ClientRectangle.Height/2 + player.Y,
                player.MotionDirection.X*100 + viewport.ClientRectangle.Width/2 + player.X,
                player.MotionDirection.Y*100 + viewport.ClientRectangle.Height/2 + player.Y);
        }

        private void DrawPostions(Graphics graphics)
        {
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);

            var centerPoint = viewport.Center;
            var center = new System.Numerics.Vector2(centerPoint.X, centerPoint.Y);

            var relMousePos = input.RelMousePosition;
            var target = new System.Numerics.Vector2(relMousePos.X, relMousePos.Y);

            var t1 = (System.Numerics.Vector2.UnitX + center + player.Position);
            var t2 = (target);

            graphics.FillEllipse(gizmosFillBrush, t1.X - 5, t1.Y - 5, 10, 10);
            graphics.FillEllipse(gizmosFillBrush, t2.X - 5, t2.Y - 5, 10, 10);
        }
    }
}