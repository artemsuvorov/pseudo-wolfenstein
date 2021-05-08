using PseudoWolfenstein.Core;
using PseudoWolfenstein.View;
using System.Drawing;

namespace PseudoWolfenstein.Model
{
    public enum Weapon
    {
        Knife,
        Pistol,
        MachineGun,
        Chaingun,
        FlameThrower,
        RocketLauncher
    }

    public class Weaponry
    {
        public Weapon SelectedWeapon { get; private set; } = Weapon.Knife;

        private bool isAnimating = false;
        private WeaponAnimation weaponAnimation;

        public Weaponry()
        {
            SelectWeapon(Weapon.Knife);
        }

        public void Shoot()
        {
            isAnimating = true;
            if (weaponAnimation.IsContinuing) return;
            weaponAnimation.Reset();
        }

        public void Animate()
        {
            if (isAnimating && weaponAnimation.IsContinuing)
                weaponAnimation.NextFrame();
            else
                isAnimating = false;
        }

        public void SelectWeapon(Weapon weapon)
        {
            SelectedWeapon = weapon;
            weaponAnimation = WeaponAnimations.GetAnimation(weapon);
            weaponAnimation.Reset();
        }

        public void DrawWeapon(Viewport viewport, Graphics graphics)
        {
            const float WeaponSpriteSize = 128f, DstWeaponScale = 2.5f;

            var texture = Repository.Textures.WeaponsTileSet;
            var weaponIndex = (int)SelectedWeapon;
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