using System.Drawing;
using System.Numerics;
using PseudoWolfenstein.Utils;

namespace PseudoWolfenstein.Model
{
    class Pane : Polygon
    {
        private readonly Vector2 position;

        public Pane(Vector2 position)
            : base(position - new Vector2(Settings.WorldWallSize * 0.5f, 0f),
                   position + new Vector2(Settings.WorldWallSize * 0.5f, 0f))
        {
            this.position = position;
        }
        public Pane(Vector2 position, Bitmap texture)
            : base(texture, 
                   position - new Vector2(Settings.WorldWallSize * 0.5f, 0f),
                   position + new Vector2(Settings.WorldWallSize * 0.5f, 0f))
        {
            this.position = position;
        }

        public void UpdateTransform(Player player)
        {
            LookAt(player.Position);
        }

        private void LookAt(Vector2 target)
        {
            var vertex1 = position - new Vector2(Settings.WorldWallSize * 0.5f, 0f);
            var vertex2 = position + new Vector2(Settings.WorldWallSize * 0.5f, 0f);

            var b1 = (vertex1 + vertex2) / 2f - target;
            var a2 = vertex2 - vertex1;
            var b2 = a2 - Vector2.Dot(a2, b1) / Vector2.Dot(b1, b1) * b1;

            Vertices[0] = position - b2.SafeNormalize() * Settings.WorldWallSize * 0.5f;
            Vertices[1] = position + b2.SafeNormalize() * Settings.WorldWallSize * 0.5f;
        }
    }
}