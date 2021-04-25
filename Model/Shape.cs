using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public abstract class Shape
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Image Texture { get; private set; }
        public RectangleF SpriteRectangle { get; private set; }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Shape() 
            : this(position:default, texture:default, srcRect:default) 
        { }
        public Shape(Vector2 position, Image texture, RectangleF srcRect)
        {
            Position = position;
            Texture = texture;
            SpriteRectangle = srcRect;
        }
        public Shape(Vector2 position)
            : this(position, default, default)
        { }
        public Shape(Vector2 position, Image texture)
            : this(position, texture, new RectangleF(0, 0, texture.Width, texture.Height))
        { }

        public abstract void Draw(Graphics graphics);
    }
}