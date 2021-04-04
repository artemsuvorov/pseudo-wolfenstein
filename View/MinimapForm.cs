using PseudoWolfenstein.Core;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class MinimapForm : Form
    {
        private static readonly Gizmos gizmos = new Gizmos();

        private static MinimapForm instance;
        public static MinimapForm Instance
        {
            get
            {
                if (instance is null)
                    instance = new MinimapForm();
                return instance;
            }
        }

        public MinimapForm()
        {
            DoubleBuffered = true;
            Size = new Size(600, 400);
            Paint += Redraw;
            Paint += gizmos.Redraw;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            using var backgroundBrush = new SolidBrush(Settings.BackgroundColor);
            e.Graphics.SmoothingMode = Settings.MinimapSmoothingMode;

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);

            var center = Camera.ScreenCenterPosition;
            e.Graphics.TranslateTransform(center.X, center.Y);
            DrawGameObjects(e.Graphics);
            e.Graphics.TranslateTransform(-center.X, -center.Y);
        }

        private void DrawGameObjects(Graphics graphics)
        {
            Player.Draw(graphics);

            foreach (var polygon in GameScene.Polygons)
                polygon.Draw(graphics);
        }
    }
}