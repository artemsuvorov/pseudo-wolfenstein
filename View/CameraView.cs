﻿using PseudoWolfenstein.Model;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class CameraView : Control
    {
        private readonly Camera camera;
        private readonly BufferedGraphics graphicsBuffer;
        private readonly UserInterface userInterface;
        private readonly DebugInfo debugInfo;

        public CameraView(Viewport viewport, Scene scene, UserInterface userInterface, DebugInfo debugInfo)
        {
            this.camera = new Camera(viewport, scene);
            Location = viewport.Location;
            Size = viewport.Size;
            DoubleBuffered = true;
            Anchor = AnchorStyles.None;

            this.userInterface = userInterface;
            this.debugInfo = debugInfo;

            using var graphics = CreateGraphics();
            var rect = new Rectangle(0, 0, viewport.Width, viewport.Height);
            graphicsBuffer = BufferedGraphicsManager.Current.Allocate(graphics, rect);

            Paint += Redraw;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            var graphics = graphicsBuffer.Graphics;
            camera.DrawView(graphics);
            userInterface.DrawUI(graphics);
            debugInfo.DrawInfo(graphics);

            graphicsBuffer.Render(e.Graphics);
        }
    }
}