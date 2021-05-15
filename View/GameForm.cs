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
        void LoadScene(Scene scene, Player player);
        Viewport GetViewport();
        void Refresh();
    }

    public partial class GameForm : Form, IGameForm, IInputClient, IViewport
    {
        public DebugInfo DebugInfo { get; private set; }

        private readonly Viewport viewport;
        private readonly CameraView cameraView;
        private UserInterface userInterface;

        public GameForm()
        {
            InitializeComponent();
            viewport = new Viewport(this);
            MinimumSize = new Size(Viewport.DefaultWidth, Viewport.DefaultHeight);
            cameraView = new CameraView(viewport);
            Controls.Add(cameraView);
            Paint += Redraw;
        }

        public void LoadScene(Scene scene, Player player)
        {
            DebugInfo = new DebugInfo(player);
            userInterface = new UserInterface(player);
            cameraView.LoadScene(scene, player);
            cameraView.LoadUI(userInterface, DebugInfo);
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
            graphics.FillRectangle(backgroundBrush, 0, 0, width, height);
        }
    }
}