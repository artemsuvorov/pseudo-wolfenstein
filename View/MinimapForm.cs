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
        private readonly Player player;

        internal Gizmos Gizmos { get; private set; }

        public MinimapForm(Viewport viewport, Scene scene, Player player)
        {
            this.viewport = viewport;
            this.scene = scene;
            this.player = player;

            DoubleBuffered = true;
            Size = new Size(1000, 800);

            Gizmos = new Gizmos(this.viewport, player);
            Paint += Redraw;
            Paint += Gizmos.Redraw;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            using var backgroundBrush = new SolidBrush(Settings.FormBackgroundColor);
            e.Graphics.SmoothingMode = Settings.MinimapSmoothingMode;

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);

            e.Graphics.TranslateTransform(viewport.Center.X, viewport.Center.Y);
            e.Graphics.TranslateTransform(-player.X, -player.Y);
            DrawGameObjects(e.Graphics);
            e.Graphics.ResetTransform();
        }

        private void DrawGameObjects(Graphics graphics)
        {
            player.Draw(graphics);
            foreach (var shape in scene.Obstacles)
                shape.Draw(graphics);

            using var gizmosStrokePen1 = new Pen(Settings.GizmosStrokeColor1, 0.1f);
            graphics.DrawLine(gizmosStrokePen1,
                player.X, player.Y,
                player.MotionDirection.X * 100 + player.X,
                player.MotionDirection.Y * 100 + player.Y);

            var raycast = new Raycast(scene, player).CastRaysAt(scene.Obstacles);
            using var gizmosFillBrush = new SolidBrush(Settings.GizmosFillColor);

            foreach (var entry in raycast)
                foreach (var cross in entry)
                    if (cross is object)
                        graphics.FillEllipse(gizmosFillBrush, cross.Location.X - 5, cross.Location.Y - 5, 10, 10);

            var rays = new[] { raycast[0].Ray, raycast[^1].Ray };
            foreach (var ray in rays)
            {
                var start = new PointF(ray.Start.X, ray.Start.Y);
                var end = new PointF(ray.End.X, ray.End.Y);
                graphics.DrawLine(gizmosStrokePen1, start, end);
            }

            var r = new Ray(player.Position, -player.Rotation);
            graphics.DrawLine(gizmosStrokePen1, r.Start.X, r.Start.Y, r.End.X, r.End.Y);
            foreach (var pane in scene.Panes)
                foreach (var vertex in pane.Vertices)
                    graphics.FillEllipse(gizmosFillBrush, vertex.X - 5, vertex.Y - 5, 10, 10);
        }
    }
}