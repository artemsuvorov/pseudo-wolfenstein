using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using PseudoWolfenstein.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class DebugInfo
    {
#if DEBUG
        private static readonly bool isDebugModeOnByDefault = true;
#else
        private static readonly bool isDebugModeOnByDefault = false;
#endif

        private readonly Player player;

        private string DebugInfoMessage =>
            $"DEBUG SESSION INFO PseudoWolfenstein\r\n" +
            $"FPS: {Time.FPS:0} deltaTime: {Time.DeltaTime:0.000000}\r\n" +
            $"Player Position: { player.Position }\r\n" +
            $"Player Rotation: { player.Rotation.ToDegrees() }\r\n" +
            $"Player SCORE:    { player.Score }";

        public bool IsDebugMode { get; private set; }

        public DebugInfo(Player player)
        {
            this.player = player;
        }

        public void Update()
        {
            if (Input.IsKeyToggled(Keys.P))
                IsDebugMode = !isDebugModeOnByDefault;
            else
                IsDebugMode = isDebugModeOnByDefault;
        }

        public void DrawInfo(Viewport viewport, Graphics graphics)
        {
            if (!IsDebugMode) return;

            using var uiBackgroundBrush = new SolidBrush(Settings.UIBackgroundColor);
            using var uiForegroundBrush = new SolidBrush(Settings.UIForegroundColor);
            using var font = new Font("Consolas", 16f);

            var size = graphics.MeasureString(DebugInfoMessage, font, viewport.Size);
            //graphics.FillRectangle(uiBackgroundBrush, 10, 10, 800, lineCount * 25 + 22);
            graphics.FillRectangle(uiBackgroundBrush, 10, 10, 750, size.Height + 20);
            graphics.DrawString(DebugInfoMessage, font, uiForegroundBrush, 20, 20);
        }
    }
}