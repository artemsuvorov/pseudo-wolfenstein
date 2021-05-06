using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
        internal class PolygonMerger
        {
            private readonly int width, height;
            private readonly Shape[,] shapes;
            private readonly Vector2?[,] vertices;

            public PolygonMerger(IEnumerable<Shape> shapes)
            {
                this.shapes = ConvertToShapeArrayOrDefault(shapes);
                if (this.shapes is null) return;
                this.width = this.shapes.GetLength(0) + 1;
                this.height = this.shapes.GetLength(1) + 1;
                this.vertices = GetVertices(this.shapes);
            }

            public static Vector2[] GetShapesCommonBorderOrDefault(IEnumerable<Shape> shapes)
            {
                return new PolygonMerger(shapes).GetShapesCommonBorderOrDefault();
            }

            public Vector2[] GetShapesCommonBorderOrDefault()
            {
                if (shapes is null) return default;

                var maxPathLength = int.MinValue;
                Vector2[] maxPath = default;

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        var startPosition = new Vector2(x, y);
                        var path = DepthSearch(startPosition).ToArray();
                        if (path.Length > maxPathLength)
                        {
                            maxPathLength = path.Length;
                            maxPath = path;
                        }
                    }
                }

                return maxPath;
            }

            private IEnumerable<Vector2> DepthSearch(Vector2 startPosition)
            {
                var stack = new Stack<Vector2>();
                var visited = new HashSet<Vector2>();
                stack.Push(startPosition);

                while (stack.Count != 0)
                {
                    var position = stack.Pop();
                    if (!IsVertex(position) || visited.Contains(position)) continue;
                    visited.Add(position);

                    yield return vertices[(int)position.X, (int)position.Y].Value;

                    var dv = Vector2.Zero;
                    for (dv.Y = -1; dv.Y <= 1; dv.Y++)
                        for (dv.X = -1; dv.X <= 1; dv.X++)
                            if (dv.X != 0 && dv.Y != 0 || !VertexExists(position + dv)) continue;
                            else stack.Push(position + dv);
                }
            }

            private Shape[,] ConvertToShapeArrayOrDefault(IEnumerable<Shape> shapes)
            {
                if (!shapes.OfType<Polygon>().Any()) 
                    return default;

                var width = (int)(shapes.Max(shape => shape.X) / Settings.WorldWallSize + 1);
                var height = (int)(shapes.Max(shape => shape.Y) / Settings.WorldWallSize + 1);
                var result = new Shape[width, height];

                foreach (var shape in shapes)
                {
                    var x = (int)(shape.Position.X / Settings.WorldWallSize);
                    var y = (int)(shape.Position.Y / Settings.WorldWallSize);
                    result[x, y] = shape;
                }

                return result;
            }

            private Vector2?[,] GetVertices(Shape[,] shapes)
            {
                Vector2?[,] vertices = new Vector2?[width, height];

                for (var y = 0; y < height; y++)
                    for (var x = 0; x < width; x++)
                        vertices[x, y] = GetVertexOrDefault(x, y);

                return vertices;
            }

            private Vector2? GetVertexOrDefault(int x, int y)
            {
                if (IsVertexOfPolygon(x, y))
                    return new Vector2(x, y) * Settings.WorldWallSize;
                else
                    return default;
            }

            private bool IsVertexOfPolygon(int x, int y)
            {
                return (ShapeExists(x - 1, y - 1) && shapes[x - 1, y - 1] is Polygon) ||
                    (ShapeExists(x - 1, y) && shapes[x - 1, y] is Polygon) ||
                    (ShapeExists(x, y - 1) && shapes[x, y - 1] is Polygon) ||
                    (ShapeExists(x, y) && shapes[x, y] is Polygon);
            }

            private bool IsVertex(Vector2 position)
            {
                return vertices[(int)position.X, (int)position.Y] is Vector2;
            }

            private bool ShapeExists(int x, int y)
            {
                return 
                    x >= 0 && x < shapes.GetLength(0) && 
                    y >= 0 && y < shapes.GetLength(1);
            }

            private bool VertexExists(Vector2 position)
            {
                return
                    position.X >= 0 && position.X < width &&
                    position.Y >= 0 && position.Y < height;
            }
        }
    }
}