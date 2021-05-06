using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Wall : Polygon
    {
        public float Size { get; set; }

        public Wall(char name, Vector2 position, float size, Image texture, RectangleF srcRect)
            : base(name, texture, srcRect, position,
                new Vector2(position.X+size, position.Y     ),
                new Vector2(position.X+size, position.Y+size),
                new Vector2(position.X,      position.Y+size))
        {
            Size = size;
        }

        public Wall(char name, Vector2 position, float size, Image texture)
            : base(name, texture, position,
                new Vector2(position.X+size, position.Y     ),
                new Vector2(position.X+size, position.Y+size),
                new Vector2(position.X,      position.Y+size))
        {
            Size = size;
        }
    }
}