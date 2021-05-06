using System;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public abstract class Shape : IEquatable<Shape>
    {
        public char Name { get; set; }
        public float X { get; set; }
        public float Y { get; set; }

        public Image Texture { get; set; }
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

        public Shape(char name) 
            : this(name, position:default, texture:default, srcRect:default) 
        { }
        public Shape(char name, Vector2 position, Image texture, RectangleF srcRect)
        {
            Name = name;
            Position = position;
            Texture = texture;
            SpriteRectangle = srcRect;
        }
        public Shape(char name, Vector2 position)
            : this(name, position, default, default)
        { }
        public Shape(char name, Vector2 position, Image texture)
            : this(name, position, texture, new RectangleF(0, 0, texture.Width, texture.Height))
        { }

        public abstract void Draw(Graphics graphics);

        public override bool Equals(object obj)
        {
            return Equals(obj as Shape);
        }

        public bool Equals(Shape other)
        {
            return other != null &&
                   Name == other.Name &&
                   X == other.X &&
                   Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, X, Y);
        }
    }
}