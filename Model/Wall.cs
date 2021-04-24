using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Wall : Polygon
    {
        public float Size { get; set; }

        public Wall(Vector2 position, float size)
            : base(new Vector2[4]
            {
                position,
                new Vector2(position.X+size, position.Y     ),
                new Vector2(position.X+size, position.Y+size),
                new Vector2(position.X,      position.Y+size),
            })
        {
            Size = size;
        }


        public Wall(Vector2 position, float size, Bitmap texture)
            : this(position, size)
        {
            Texture = texture;
        }
    }
}