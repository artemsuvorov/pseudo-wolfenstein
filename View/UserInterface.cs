using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class UserInterface : UserControl
    {
        private readonly DebugInfo debugInfo;

        public UserInterface()
        {
            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Dock = DockStyle.Fill;

            debugInfo = new DebugInfo();
            Controls.Add(debugInfo);
        }
    }
}