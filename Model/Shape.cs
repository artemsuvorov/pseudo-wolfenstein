using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public abstract class Shape
    {
        public float X { get; set; }
        public float Y { get; set; }

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
            : this(x:default, y:default) 
        { }
        public Shape(float x, float y)
        {
            X = x; Y = y;
        }
        public Shape(Vector2 position)
        {
            Position = position;
        }

        public abstract void Draw(Graphics graphics);
    }
}