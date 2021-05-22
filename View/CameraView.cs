using PseudoWolfenstein.Model;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class CameraView : Control
    {
        private readonly BufferedGraphics graphicsBuffer;
        private readonly Viewport viewport;

        private Camera camera;
        private UserInterface userInterface;
        private DebugInfo debugInfo;

        public CameraView(Viewport viewport)
        {
            Location = viewport.Location;
            Size = viewport.Size;
            DoubleBuffered = true;
            Anchor = AnchorStyles.None;
            this.viewport = viewport;

            using var graphics = CreateGraphics();
            var rect = new Rectangle(0, 0, viewport.Width, viewport.Height);
            graphicsBuffer = BufferedGraphicsManager.Current.Allocate(graphics, rect);

            Paint += Redraw;
        }

        ~CameraView()
        {
            graphicsBuffer.Dispose();
        }

        public void Animate()
        {
            userInterface?.Animate();
        }

        public void LoadScene(Scene scene, Player player)
        {
            this.camera = new Camera(viewport, scene, player);
        }

        public void LoadUI(UserInterface userInterface, DebugInfo debugInfo)
        {
            this.userInterface = userInterface;
            this.debugInfo = debugInfo;
        }

        public void Enable()
        {
            this.Size = new Size(viewport.Width, viewport.Height);
            this.Enabled = true;
        }

        public void Disable()
        {
            this.Size = new Size(0, 0);
            this.Enabled = false;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            if (camera is null && userInterface is null && debugInfo is null) 
                return;

            var graphics = graphicsBuffer.Graphics;

            if (!Enabled)
            {
                graphics.FillRectangle(Brushes.Black, 0, 0, viewport.Width, viewport.Height);
                return;
            }

            camera.DrawView(graphics);
            userInterface.DrawUI(viewport, graphics);
            debugInfo.DrawInfo(viewport, graphics);

            graphicsBuffer.Render(e.Graphics);
        }
    }
}