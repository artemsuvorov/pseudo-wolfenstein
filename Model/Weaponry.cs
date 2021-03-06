using PseudoWolfenstein.Core;
using PseudoWolfenstein.View;
using System;
using System.Drawing;

namespace PseudoWolfenstein.Model
{
    public class Weaponry
    {
        private const WeaponType StartWeapon = WeaponType.Pistol;

        public Weapon SelectedWeapon { get; private set; }
        public int Ammo { get; set; } = 7;

        public event EventHandler Shot;

        private readonly Weapons weapons;
        private readonly WeaponAnimations weaponAnimations;

        private bool isAnimating = false;
        private WeaponFireAnimation weaponAnimation;

        public Weaponry()
        {
            weapons = new Weapons();
            weaponAnimations = new WeaponAnimations();

            if (!weapons.TryGetAvailableWeapon(StartWeapon, out var weapon))
                throw new InvalidOperationException("There is no starting weapon in the weaponry.");

            SelectedWeapon = weapon;
            weaponAnimation = weaponAnimations.GetAnimation(StartWeapon);
        }

        public void BeginShoot()
        {
            if (SelectedWeapon.Type != WeaponType.Knife && Ammo <= 0) return;
            if (isAnimating && weaponAnimation.IsContinuing) return;
            isAnimating = true;
            weaponAnimation.Reset();
        }

        public void Animate()
        {
            //if (Ammo == 0)
            //    SelectWeapon(WeaponType.Knife);

            if (isAnimating && weaponAnimation.IsContinuing)
            {
                weaponAnimation.NextFrame();
                if (!weaponAnimation.IsFireFrame) return;
                if (SelectedWeapon.Type != WeaponType.Knife)
                    Ammo--;
                Shot?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                weaponAnimation.Reset();
                isAnimating = false;
            }
        }

        public void SelectWeapon(WeaponType weaponType)
        {
            if (!IsSelectable(weaponType)) return;

            weaponAnimation?.Reset();
            isAnimating = false;

            if (!weapons.TryGetAvailableWeapon(weaponType, out var weapon)) return;
            SelectedWeapon = weapon;
            weaponAnimation = weaponAnimations.GetAnimation(weaponType);
            weaponAnimation.Reset();
        }

        public void Equip(WeaponType type)
        {
            if (weapons.AddAvailableWeapon(type))
                SelectWeapon(type);
            else if (type != WeaponType.Knife)
                Ammo += 15;
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
            var dsty = viewport.Height - WeaponSpriteSize * DstWeaponScale - 100;
            var dstRect = new RectangleF(dstx, dsty, WeaponSpriteSize * DstWeaponScale, WeaponSpriteSize * DstWeaponScale);

            var srcx = (1 + weaponAnimationFrame) * (1f + WeaponSpriteSize);
            var srcy = weaponIndex * (1f + WeaponSpriteSize);
            var srcRect = new RectangleF(srcx, srcy, WeaponSpriteSize - 1f, WeaponSpriteSize - 1f);

            graphics.DrawImage(texture, dstRect, srcRect, GraphicsUnit.Pixel);
        }
    }
}