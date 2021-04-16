using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class MinimapForm : Form
    {
        private readonly Viewport viewport;
        private readonly Input input;
        private readonly Scene scene;

        internal Gizmos Gizmos { get; private set; }

        public MinimapForm(Viewport viewport, Input input, Scene scene)
        {
            this.viewport = viewport;
            this.input = input;
            this.scene = scene;

            DoubleBuffered = true;
            Size = new Size(600, 400);

            Gizmos = new Gizmos(this.viewport, this.input, this.scene.Player);
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
        }
    }
}