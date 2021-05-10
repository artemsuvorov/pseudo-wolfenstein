using PseudoWolfenstein.Core;
using PseudoWolfenstein.View;
using System;
using System.Drawing;

namespace PseudoWolfenstein.Model
{
    public class Weaponry
    {
        public Weapon SelectedWeapon { get; private set; }
        public int Ammo { get; set; } = 5;

        public event EventHandler Shot;

        private bool isAnimating = false;
        private WeaponAnimation weaponAnimation;

        public Weaponry()
        {
            const WeaponType StartWeapon = WeaponType.Knife;
            SelectedWeapon = Weapons.GetWeapon(StartWeapon);
            weaponAnimation = WeaponAnimations.GetAnimation(StartWeapon);
        }

        public void BeginShoot()
        {
            if (Ammo <= 0) return;
            if (isAnimating && weaponAnimation.IsContinuing) return;
            isAnimating = true;
            weaponAnimation.Reset();
        }

        public void Animate()
        {
            if (isAnimating && weaponAnimation.IsContinuing)
            {
                weaponAnimation.NextFrame();
                if (!weaponAnimation.IsFireFrame) return;
                if (SelectedWeapon.Type != WeaponType.Knife)
                    Ammo--;
                Shot?.Invoke(this, EventArgs.Empty);
            }
            else isAnimating = false;
        }

        public void SelectWeapon(WeaponType weaponType)
        {
            if (!IsSelectable(weaponType)) return;

            weaponAnimation?.Reset();
            isAnimating = false;

            SelectedWeapon = Weapons.GetWeapon(weaponType);
            weaponAnimation = WeaponAnimations.GetAnimation(weaponType);
            weaponAnimation.Reset();
        }

        private bool IsSelectable(WeaponType weaponType)
        {
            return !(isAnimating && weaponAnimation.IsContinuing) || weaponType == SelectedWeapon.Type;
        }

        public void DrawWeapon(Viewport viewport, Graphics graphics)
        {
            const float WeaponSpriteSize = 128f, DstWeaponScale = 2.5f;

            var texture = Repository.Textures.WeaponsTileSet;
            var weaponIndex = SelectedWeapon.Id;
            var weaponAnimationFrame = weaponAnimation.Frame;

            var dstx = (viewport.Width - WeaponSpriteSize * DstWeaponScale) / 2f;
            var dsty = viewport.Height - WeaponSpriteSize * DstWeaponScale;
            var dstRect = new RectangleF(dstx, dsty, WeaponSpriteSize * DstWeaponScale, WeaponSpriteSize * DstWeaponScale);

            var srcx = (1 + weaponAnimationFrame) * (1f + WeaponSpriteSize);
            var srcy = weaponIndex * (1f + WeaponSpriteSize);
            var srcRect = new RectangleF(srcx, srcy, WeaponSpriteSize - 1f, WeaponSpriteSize - 1f);

            graphics.DrawImage(texture, dstRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}