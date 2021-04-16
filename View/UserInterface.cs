using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class UserInterface : UserControl
    {
        private readonly Player player;

        internal DebugInfo DebugInfo { get; private set; }

        public UserInterface(Player player)
        {
            this.player = player;

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Dock = DockStyle.Fill;

            DebugInfo = new DebugInfo(this.player);
            Controls.Add(DebugInfo);
        }
    }
}