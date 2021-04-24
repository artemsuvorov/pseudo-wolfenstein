using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class MinimapForm : Form
    {
        private readonly Viewport viewport;
        private readonly Scene scene;

        internal Gizmos Gizmos { get; private set; }

        public MinimapForm(Viewport viewport, Scene scene)
        {
            this.viewport = viewport;
            this.scene = scene;

            DoubleBuffered = true;
            Size = new Size(1000, 800);

            Gizmos = new Gizmos(this.viewport, this.scene.Player);
            Paint += Redraw;
            Paint += Gizmos.Redraw;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            using var backgroundBrush = new SolidBrush(Settings.FormBackgroundColor);
            e.Graphics.SmoothingMode = Settings.MinimapSmoothingMode;

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);

            var center = viewport.Center;
            e.Graphics.TranslateTransform(center.X, center.Y);
            DrawGameObjects(e.Graphics);
            e.Graphics.TranslateTransform(-center.X, -center.Y);
        }

        private void DrawGameObjects(Graphics graphics)
        {
            scene.Player.Draw(graphics);
            foreach (var shape in scene.Obstacles)
                shape.Draw(graphics);

            //using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1, 0.1f);
            //var player = scene.Player;
            //graphics.DrawLine(gizmosStrokePen1,
            //    player.X, player.Y,
            //    player.MotionDirection.X * 100 + player.X,
            //    player.MotionDirection.Y * 100 + player.Y);

            //var raycast = new Raycast(scene).GetCrossingPointsAndDistances();
            //using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);
            //foreach (var crossingPoint in raycast.CrossingPoints)
            //    graphics.FillEllipse(gizmosFillBrush, crossingPoint.X-5, crossingPoint.Y-5, 10, 10);

            //var rays = new[] { raycast.Rays[0], raycast.Rays[raycast.Length - 1] };
            //foreach (var ray in rays)
            //{
            //    var start = new PointF(ray.Start.X, ray.Start.Y);
            //    var end = new PointF(ray.End.X, ray.End.Y);
            //    graphics.DrawLine(gizmosStrokePen1, start, end);
            //}
        }
    }
}