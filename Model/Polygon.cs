using System.Drawing;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Polygon : Shape
    {
        public Vector2[] Vertices { get; protected set; }

        public Polygon(params Vector2[] vertices)
            : base(position: vertices[0])
        {
            Vertices = vertices;
        }

        public Polygon(Bitmap texture, params Vector2[] vertices)
            : this(vertices)
        {
            Texture = texture;
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