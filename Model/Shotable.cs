using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public abstract class Shotable : RotatingPane
    {
        public Shotable(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

        public void OnPlayerShot(object sender, GameEventArgs e)
        {
            var shootDistance = e.Player.Weaponry.SelectedWeapon.Distance;
            var hitEnd = e.Player.Position + Vector2.UnitX.RotateClockwise(-e.Player.Rotation) * shootDistance;
            var wallHit = e.Scene.GetMinDistanceWallCross
                (e.Player.Position, hitEnd, out _, out var wallHitDst);
            var enemyHit = MathF2D.AreSegmentsCrossing
                (e.Player.Position, hitEnd, Vertices[0], Vertices[1], out var enemyHitLocation);
            var enemyHitDst = (enemyHitLocation - e.Player.Position).Length();
            if ((!wallHit && enemyHit) || (wallHit && wallHitDst > enemyHitDst))
                OnShot(sender, e);
        }

        protected abstract void OnShot(object sender, GameEventArgs e);
    }
}