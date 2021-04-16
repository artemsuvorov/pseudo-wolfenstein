using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class UserInterface : UserControl
    {
        private readonly Input input;
        private readonly Player player;

        internal DebugInfo DebugInfo { get; private set; }

        public UserInterface(Input input, Player player)
        {
            this.input = input;
            this.player = player;

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Dock = DockStyle.Fill;

            DebugInfo = new DebugInfo(this.input, this.player);
            Controls.Add(DebugInfo);
        }
    }
}