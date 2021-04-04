using System.Drawing;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public abstract class Shape
    {
        private Vector2 position = new Vector2(0f, 0f);

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public float X => position.X;
        public float Y => position.Y;

        public Shape(Vector2 position)
        {
            Position = position;
        }

        public abstract void Draw(Graphics graphics);
    }

    public class Polygon : Shape
    {
        private readonly PointF[] points;
        
        public Vector2[] Vertices { get; private set; }

        public Polygon(params Vector2[] vertices)
            : base(position: vertices[0])
        {
            Vertices = vertices;
            points = vertices.Select(vec => new PointF(vec.X, vec.Y)).ToArray();
        }

        public override void Draw(Graphics graphics)
        {
            using var objectFillBrush = new SolidBrush(Settings.GameObjectFillColor);
            using var objectStrokePen = new Pen(Settings.GameObjectStrokeColor, Settings.ObjectStrokeWidth);

            graphics.DrawPolygon(objectStrokePen, points);
            graphics.FillPolygon(objectFillBrush, points);
        }
    }

    public class Square : Polygon
    {
        public Square(Vector2 position, float size)
            : base(new Vector2[4]
            {
                position,
                new Vector2(position.X+size, position.Y     ),
                new Vector2(position.X+size, position.Y+size),
                new Vector2(position.X,      position.Y+size),
            })
        { }
    }
}