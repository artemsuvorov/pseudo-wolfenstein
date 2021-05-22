using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PseudoWolfenstein.View
{
    public class UserInterface
    {
        private const int DamageAnimationLength = 5;

        private readonly Player player;

        private int damageAnimationIndex = 0;
        private bool isDamageAnimationEnabled = false;

        public UserInterface(Player player)
        {
            this.player = player;
            player.Damaged += OnPlayerDamaged;
        }

        private void OnPlayerDamaged(object sender, GameEventArgs e)
        {
            BeginDamageEffectAnimation();
        }

        public void Animate()
        {
            if (isDamageAnimationEnabled && damageAnimationIndex < DamageAnimationLength)
                damageAnimationIndex++;
            else
            {
                isDamageAnimationEnabled = false;
                damageAnimationIndex = 0;
            }
        }

        public void DrawUI(Viewport viewport, Graphics graphics)
        {
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            player.Weaponry.DrawWeapon(viewport, graphics);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            if (isDamageAnimationEnabled)
                DrawDamageEffect(viewport, graphics);
        }

        private void BeginDamageEffectAnimation()
        {
            isDamageAnimationEnabled = true;
        }

        private void DrawDamageEffect(Viewport viewport, Graphics graphics)
        {
            using var damageScreenBrush = new SolidBrush(Color.FromArgb(100, 224, 41, 0));
            graphics.FillRectangle(damageScreenBrush, 0, 0, viewport.Width, viewport.Height);
        }
    }
}