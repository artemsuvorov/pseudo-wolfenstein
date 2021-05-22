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
        void InvalidateAll();
        void ShowWinScreen();
        void ShowGameOverScreen();
        void Animate();
        void HideGameEndScreen();
    }

    public partial class GameForm : Form, IGameForm, IInputClient, IViewport
    {
        public DebugInfo DebugInfo { get; private set; }

        private readonly Viewport viewport;
        private readonly CameraView cameraView;
        private UserInterface userInterface;

        private bool isGameOverScreen = false;
        private bool isWinScreen = false;

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

        public void Animate()
        {
            cameraView.Animate();
        }

        public void InvalidateAll()
        {
            if (isWinScreen || isGameOverScreen)
                Invalidate();
            else
                Refresh();
        }

        public void ShowWinScreen()
        {
            isWinScreen = true;
            cameraView.Disable();
        }

        public void ShowGameOverScreen()
        {
            isGameOverScreen = true;
            cameraView.Disable();
        }

        public void HideGameEndScreen()
        {
            isGameOverScreen = false;
            isWinScreen = false;
            cameraView.Enable();
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = Settings.GraphicsSmoothingMode;
            DrawBackground(e.Graphics, e.ClipRectangle.Width, e.ClipRectangle.Height);
            if (isWinScreen) DrawWinScreen(e.Graphics);
            else if (isGameOverScreen) DrawGameOverScreen(e.Graphics);
        }

        private void DrawWinScreen(Graphics graphics)
        {
            const string WinMessage = "You Win";

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillRectangle(Brushes.Black, viewport.ClientRectangle);

            using var font = new Font("Consolas", 72f, FontStyle.Bold);
            var size = graphics.MeasureString(WinMessage, font, viewport.Size);
            var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            graphics.DrawString(WinMessage, font, Brushes.White, viewport.ClientRectangle, format);
        }

        private void DrawGameOverScreen(Graphics graphics)
        {
            const string WinMessage = "You Died";

            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphics.FillRectangle(Brushes.Black, viewport.ClientRectangle);

            using var font = new Font("Consolas", 72f, FontStyle.Bold);
            var size = graphics.MeasureString(WinMessage, font, viewport.Size);
            var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            graphics.DrawString(WinMessage, font, Brushes.White, viewport.ClientRectangle, format);
        }

        private void DrawBackground(Graphics graphics, int width, int height)
        {
            using var backgroundBrush = new SolidBrush(Settings.FormBackgroundColor);
            graphics.FillRectangle(backgroundBrush, 0, 0, width, height);
        }
    }
}