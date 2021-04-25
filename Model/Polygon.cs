using System.Drawing;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Polygon : Shape
    {
        public Vector2[] Vertices { get; protected set; }

        public Polygon(Image texture, RectangleF srcRect, params Vector2[] vertices)
            : base(position: vertices[0], texture, srcRect)
        {
            Vertices = vertices;
        }
        public Polygon(Image texture, params Vector2[] vertices)
            : base(position: vertices[0], texture)
        {
            Vertices = vertices;
        }
        public Polygon(params Vector2[] vertices)
            : this(texture:default, srcRect:default, vertices)
        { }

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