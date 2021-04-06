using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    internal class DebugInfo : UserControl
    {
#if DEBUG
        private static readonly bool isDebugModeOnByDefault = true;
#else
        private static readonly bool isDebugModeOnByDefault = false;
#endif

        private readonly Input input;
        private readonly Player player;

        private string DebugInfoMessage =>
            $"DEBUG SESSION INFO PseudoWolfenstein\r\n" +
            $"FPS: {Time.FPS:0} deltaTime: {Time.DeltaTime}\r\n" +
            $"Player Position: { player.Position }\r\n" +
            $"Player Rotation: { MathF2D.ToDegrees(player.Rotation) }\r\n" +
            $"Rel Mouse Position: { input.RelMousePosition }";

        private readonly int lineCount;

        public bool IsDebugMode { get; private set; }

        public DebugInfo(Input input, Player player)
        {
            this.input = input;
            this.player = player;

            lineCount = StringUtils.CountLines(DebugInfoMessage);
            
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Dock = DockStyle.Fill;

            Paint += Redraw;
        }

        public new void Update()
        {
            if (input.IsKeyToggled(Keys.P))
                IsDebugMode = !isDebugModeOnByDefault;
            else
                IsDebugMode = isDebugModeOnByDefault;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            if (!IsDebugMode) return;

            using var uiBackgroundBrush = new SolidBrush(Settings.UIBackgroundColor);
            using var uiForegroundBrush = new SolidBrush(Settings.UIForegroundColor);

            e.Graphics.FillRectangle(uiBackgroundBrush, 10, 10, 800, lineCount * 25 + 22);
            e.Graphics.DrawString(DebugInfoMessage, new Font("Consolas", 16f), uiForegroundBrush, 20, 20);
        }
    }
}