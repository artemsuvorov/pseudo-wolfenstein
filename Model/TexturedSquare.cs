using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class TexturedSquare : Square
    {
        public Image Texture { get; private set; }

        public TexturedSquare(Vector2 position, float size, Image texture) : base(position, size)
        {
            Texture = texture;
        }
    }
}