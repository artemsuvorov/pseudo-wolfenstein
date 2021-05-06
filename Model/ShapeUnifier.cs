using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
        internal class ShapeUnifier
        {
            private readonly int width, height;
            private readonly Shape[,] shapes;
            private readonly bool[,] visited;

            public ShapeUnifier(Shape[,] shapes)
            {
                this.shapes = shapes;
                width = shapes.GetLength(0);
                height = shapes.GetLength(1);
                this.visited = new bool[width, height];
            }

            public IReadOnlySet<(char, IReadOnlySet<Shape>)> GetAdjecentShapeIsles()
            {
                var isles = new HashSet<(char, IReadOnlySet<Shape>)>();

                for (var y = 0; y < height; y++)
                {
                    for (var x = 0; x < width; x++)
                    {
                        if (shapes[x, y] is null) continue;
                        var adjecent = GetAllAdjecentShapes(new Vector2(x, y)).ToHashSet();
                        if (adjecent is null || adjecent.Count <= 0) continue;
                        isles.Add((shapes[x, y].Name, adjecent.ToHashSet()));
                    }
                }

                return isles;
            }

            private IEnumerable<Shape> GetAllAdjecentShapes(Vector2 startLocation)
            {
                var queue = new Queue<Vector2>();
                queue.Enqueue(startLocation);

                while (queue.Count != 0)
                {
                    var location = queue.Dequeue();
                    if (IsSkipable(location, startLocation)) continue;
                    MarkVisited(location);

                    yield return shapes[(int)location.X, (int)location.Y];

                    var dv = Vector2.Zero;
                    for (dv.Y = -1; dv.Y <= 1; dv.Y++)
                        for (dv.X = -1; dv.X <= 1; dv.X++)
                            if (dv.X != 0 && dv.Y != 0 || !SceneContains(location + dv)) continue;
                            else queue.Enqueue(location + dv);
                }
            }
                
            private bool IsSkipable(Vector2 location, Vector2 startLocation)
            {
                return SceneIsVisited(location) || !IsShape(location) || 
                    DoShapesDifferAt(location, startLocation);
            }

            private bool IsShape(Vector2 location)
            {
                return shapes[(int)location.X, (int)location.Y] is Shape;
            }

            private bool DoShapesDifferAt(Vector2 v1, Vector2 v2)
            {
                return shapes[(int)v2.X, (int)v2.Y].Name != shapes[(int)v1.X, (int)v1.Y].Name;
            }

            private bool SceneContains(int x, int y)
            {
                return x >= 0 && x < width && y >= 0 && y < height;
            }

            private bool SceneContains(Vector2 location)
            {
                return SceneContains((int)location.X, (int)location.Y);
            }

            private bool SceneIsVisited(Vector2 location)
            {
                return visited[(int)location.X, (int)location.Y];
            }

            private void MarkVisited(Vector2 location)
            {
                visited[(int)location.X, (int)location.Y] = true;
            }
        }
    }
}