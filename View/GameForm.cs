using PseudoWolfenstein.Core;
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
        Viewport Viewport { get; }

        void Refresh();
    }

    public partial class GameForm : Form, IGameForm, IInputClient, IViewport
    {
        public UserInterface UserInterface { get; private set; }
        public Viewport Viewport { get; private set; }

        private Camera camera;
        private Scene scene;

        public GameForm()
        {
            InitializeComponent();
            Viewport = new Viewport(this);
            MinimumSize = new Size(Viewport.DefaultWidth, Viewport.DefaultHeight);
            Paint += Redraw;
        }

        public void Initialize(Input input, Player player, Scene scene)
        {
            UserInterface = new UserInterface(input, player);
            camera = new Camera(Viewport, scene);
            Controls.Add(UserInterface);
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