using PseudoWolfenstein.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public interface IInputClient
    {
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;

        event MouseEventHandler MouseMove;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseUp;
    }

    public interface IGameForm : IInputClient, IViewport
    {
        event EventHandler Load;
        UserInterface UserInterface { get; }
        Viewport GetViewport();
        void Refresh();
    }

    public partial class GameForm : Form, IGameForm, IInputClient, IViewport
    {
        public UserInterface UserInterface { get; private set; }

        private readonly Viewport viewport;
        private readonly Camera camera;

        public GameForm(Scene scene)
        {
            InitializeComponent();
            viewport = new Viewport(this);
            UserInterface = new UserInterface(scene.Player);
            camera = new Camera(viewport, scene);
            Controls.Add(UserInterface);
            MinimumSize = new Size(Viewport.DefaultWidth, Viewport.DefaultHeight);
            Paint += Redraw;
        }

        public Viewport GetViewport()
        {
            return viewport;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = Settings.GraphicsSmoothingMode;

            camera.DrawView(e.Graphics);
            DrawBackground(e.Graphics, e.ClipRectangle.Width, e.ClipRectangle.Height);
        }

        private void DrawBackground(Graphics graphics, int width, int height)
        {
            using var backgroundBrush = new SolidBrush(Settings.FormBackgroundColor);
            graphics.FillRectangle(backgroundBrush, 0, 0, width, viewport.Y);
            graphics.FillRectangle(backgroundBrush, 0, viewport.Y, viewport.X, height);
            graphics.FillRectangle(backgroundBrush, viewport.X, viewport.Y+viewport.Height,
                width-viewport.X, height-viewport.Y-viewport.Height);
            graphics.FillRectangle(backgroundBrush, viewport.X+viewport.Width, viewport.Y,
                width-viewport.X-viewport.Width, height-viewport.Y);
        }
    }
}