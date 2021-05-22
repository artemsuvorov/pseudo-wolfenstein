using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using System;
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
            DrawHUD(viewport, graphics);
            graphics.InterpolationMode = InterpolationMode.Bilinear;
            if (isDamageAnimationEnabled)
                DrawDamageEffect(viewport, graphics);
        }

        private void DrawHUD(Viewport viewport, Graphics graphics)
        {
            var hudTexture = Repository.Textures.HUD;
            var dstHUDScale = 2.5f;

            var dstx = (viewport.Width - hudTexture.Width * dstHUDScale) / 2f;
            var dsty = viewport.Height - hudTexture.Height * dstHUDScale;
            var dstRect = new RectangleF(dstx, dsty, hudTexture.Width*dstHUDScale+2f, hudTexture.Height*dstHUDScale+2f);

            graphics.DrawImage(hudTexture, dstRect);
            DrawString(player.Score.ToString(), 0f, viewport, graphics);
            DrawString(player.Health.ToString(), 285f, viewport, graphics);
            DrawString(player.Weaponry.Ammo.ToString(), 470f, viewport, graphics);
        }

        private void DrawString(string value, float offset, Viewport viewport, Graphics graphics)
        {
            using var brush = new SolidBrush(Color.FromArgb(216, 216, 252));
            using var font = new Font("Consolas", 32f, FontStyle.Bold);
            var format = new StringFormat
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Center
            };
            var dstRect = new RectangleF(offset, viewport.Height - 50f, 140f*2.5f, 40f);
            graphics.DrawString(value, font, brush, dstRect, format);
        }

        private void DrawDamageEffect(Viewport viewport, Graphics graphics)
        {
            using var damageScreenBrush = new SolidBrush(Color.FromArgb(100, 224, 41, 0));
            graphics.FillRectangle(damageScreenBrush, 0, 0, viewport.Width, viewport.Height-100f);
        }

        private void BeginDamageEffectAnimation()
        {
            isDamageAnimationEnabled = true;
        }
    }
}