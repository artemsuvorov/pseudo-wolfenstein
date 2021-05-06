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
        DebugInfo DebugInfo { get; }
        Viewport GetViewport();
        void Refresh();
    }

    public partial class GameForm : Form, IGameForm, IInputClient, IViewport
    {
        public DebugInfo DebugInfo { get; private set; }

        private readonly UserInterface userInterface;
        private readonly Viewport viewport;
        private readonly CameraView cameraView;

        public GameForm(Scene scene)
        {
            InitializeComponent();
            viewport = new Viewport(this);
            DebugInfo = new DebugInfo(scene.Player);
            userInterface = new UserInterface(scene.Player, viewport);
            cameraView = new CameraView(viewport, scene, userInterface, DebugInfo);
            Controls.Add(cameraView);
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