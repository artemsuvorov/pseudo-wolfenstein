using PseudoWolfenstein.Model;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PseudoWolfenstein.View
{
    public class UserInterface
    {
        private readonly Player player;

        public UserInterface(Player player)
        {
            this.player = player;
        }

        public void DrawUI(Viewport viewport, Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            player.Weaponry.DrawWeapon(viewport, graphics);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
        }
    }
}