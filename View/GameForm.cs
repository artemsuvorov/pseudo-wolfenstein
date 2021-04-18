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
            using var backgroundBrush = new SolidBrush(Settings.FormBackgroundColor);
            e.Graphics.SmoothingMode = Settings.GraphicsSmoothingMode;

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);
            camera.DrawView(e.Graphics);
        }
    }
}