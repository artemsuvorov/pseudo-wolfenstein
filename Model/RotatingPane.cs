using System.Drawing;
using System.Numerics;
using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;

namespace PseudoWolfenstein.Model
{
    public class RotatingPane : Pane
    {
        public RotatingPane(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }
        public RotatingPane(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

        public void UpdateTransform(object sender, GameEventArgs e)
        {
            LookAt(e.Player.Position);
        }

        private void LookAt(Vector2 target)
        {
            var vertex1 = Center - new Vector2(Settings.WorldWallSize * 0.5f, 0f);
            var vertex2 = Center + new Vector2(Settings.WorldWallSize * 0.5f, 0f);

            var b1 = (vertex1 + vertex2) / 2f - target;
            var a2 = vertex2 - vertex1;
            var b2 = a2 - Vector2.Dot(a2, b1) / Vector2.Dot(b1, b1) * b1;

            Vertices[0] = Center - b2.SafeNormalize() * Settings.WorldWallSize * 0.5f;
            Vertices[1] = Center + b2.SafeNormalize() * Settings.WorldWallSize * 0.5f;
        }
    }
}