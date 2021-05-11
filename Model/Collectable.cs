﻿using PseudoWolfenstein.Core;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Collectable : RotatingPane
    {
        public Collectable(char name, Vector2 position, Image texture)
            : base(name, position, texture, default)
        { }

        public void Collide(object sender, GameEventArgs e)
        {
            var dst = (Center - e.Player.Position).Length();
            if (dst > Settings.WorldWallSize * 0.5f) return;
            Collect(e.Player);
            Destroy();
        }

        protected virtual void Collect(Player player) 
        {
            player.Score += 10;
        }
    }

    public class Ammo : Collectable
    {
        public Ammo(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            player.Weaponry.Ammo += 10;
        }
    }
}