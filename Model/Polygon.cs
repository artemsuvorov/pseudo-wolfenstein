using System;
using System.Drawing;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Polygon : Shape
    {
        public Vector2[] Vertices { get; protected set; }

        public event EventHandler<Polygon> Destroying;

        public Polygon(char name, Image texture, RectangleF srcRect, params Vector2[] vertices)
            : base(name, position: vertices[0], texture, srcRect)
        {
            Vertices = vertices;
        }
        public Polygon(char name, Image texture, params Vector2[] vertices)
            : base(name, position: vertices[0], texture)
        {
            Vertices = vertices;
        }
        public Polygon(char name, params Vector2[] vertices)
            : this(name, texture:default, srcRect:default, vertices)
        { }

        public void Destroy()
        {
            Destroying?.Invoke(this, this);
        }

        public override void Draw(Graphics graphics)
        {
            using var objectFillBrush = new SolidBrush(Settings.GameObjectFillColor);
            using var objectStrokePen = new Pen(Settings.GameObjectStrokeColor, Settings.ObjectStrokeWidth);

            var points = Vertices.Select(vec => new PointF(vec.X, vec.Y)).ToArray();
            graphics.DrawPolygon(objectStrokePen, points);
            graphics.FillPolygon(objectFillBrush, points);
        }
    }
}