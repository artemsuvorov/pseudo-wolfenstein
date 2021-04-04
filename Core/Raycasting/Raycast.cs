using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Core.Raycasting
{
    // todo: make non-static ? 
    internal static class RaycastData
    {
        public static readonly List<Vector2> CrossingPoints = new List<Vector2>();
        public static readonly List<float> CrossingPointsDistances = new List<float>();

        public static readonly List<Ray> Rays = new List<Ray>();
    }

    public static class Raycast
    {
        public static void UpdateRaycastData()
        {
            RaycastData.CrossingPoints.Clear();
            RaycastData.CrossingPointsDistances.Clear();
            RaycastData.Rays.Clear();

            var fov = MathF2D.ToDegrees(Player.FieldOfView);

            for (var angle = 0.0f; angle <= fov; angle += Settings.RaycastRayDencity)
            {
                var ray = new Ray(Player.Position, MathF2D.ToRadians(angle) - Player.Rotation - Player.FieldOfView/2f);
                RaycastData.Rays.Add(ray);

                var crossingPoint = GetMinDistanceCrossingPoint(ray, GameScene.Polygons);
                RaycastData.CrossingPoints.Add(crossingPoint);
            }
        }

        public static Vector2 GetMinDistanceCrossingPoint(Ray ray, Polygon[] polygons)
        {
            var minDistanceCrossingPoint = default(Vector2);
            var minDistance = float.MaxValue;

            foreach (Polygon polygon in polygons)
            {
                for (var index = 1; index < polygon.Vertices.Length + 1; index++)
                {
                    Vector2 vertex1 = polygon.Vertices[index-1], vertex2 = polygon.Vertices[index % polygon.Vertices.Length];

                    var isCrossing = ray.IsCrossing(vertex1, vertex2, out Vector2 crossingPoint);
                    var distance = (crossingPoint-Player.Position).Length();

                    if (isCrossing && distance < minDistance)
                    {
                        minDistanceCrossingPoint = crossingPoint;
                        minDistance = distance;
                    }
                }
            }

            RaycastData.CrossingPointsDistances.Add(minDistance);
            return minDistanceCrossingPoint;
        }
    }
}