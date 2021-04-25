using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public class UserInterface : UserControl
    {
        private readonly Player player;
        private readonly Viewport viewport;

        internal DebugInfo DebugInfo { get; private set; }

        public UserInterface(Player player, Viewport viewport)
        {
            this.player = player;
            this.viewport = viewport;

            DoubleBuffered = true;
            BackColor = Color.Transparent;
            Dock = DockStyle.Fill;

            DebugInfo = new DebugInfo(this.player);
            Controls.Add(DebugInfo);

            Paint += Redraw;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(viewport.X, viewport.Y);
            DrawWeapon(e.Graphics);
            e.Graphics.ResetTransform();
        }

        private void DrawWeapon(Graphics graphics)
        {
            const float WeaponSpriteSize = 128f, DstWeaponScale = 2.5f;
            const int weaponAnimationFrame = 0;

            var texture = Repository.Textures.WeaponsTileSet;
            var weaponIndex = player.SelectedWeapon;

            var dstx = (viewport.Width - WeaponSpriteSize * DstWeaponScale) / 2f;
            var dsty = viewport.Height - WeaponSpriteSize * DstWeaponScale;
            var dstRect = new RectangleF(dstx, dsty, WeaponSpriteSize*DstWeaponScale, WeaponSpriteSize*DstWeaponScale);
            
            var srcx = (1 + weaponAnimationFrame) * (1f + WeaponSpriteSize);
            var srcy = weaponIndex * (1f + WeaponSpriteSize);
            var srcRect = new RectangleF(srcx, srcy, WeaponSpriteSize-1f, WeaponSpriteSize-1f);

            graphics.DrawImage(texture, dstRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}