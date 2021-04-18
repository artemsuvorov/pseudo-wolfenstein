using PseudoWolfenstein.Model;
using PseudoWolfenstein.Utils;
using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public enum Side
    {
        Horizontal,
        Vertical,
    }

    public class RaycastData
    {
        public int Length { get; private set; }

        public Vector2[] CrossingPoints { get; private set; }
        public float[] CrossingPointsDistances { get; private set; }
        public Polygon[] CrossedObstacles { get; private set; }
        public Side[] CrossedSides { get; private set; }
        public Ray[] Rays { get; private set; }

        public RaycastData(int length, Vector2[] crossingPoints, float[] crossingPointsDistances, 
            Polygon[] crossedObstacles, Side[] crossedSides, Ray[] rays)
        {
            Length = length;
            CrossingPoints = crossingPoints;
            CrossingPointsDistances = crossingPointsDistances;
            CrossedObstacles = crossedObstacles;
            CrossedSides = crossedSides;
            Rays = rays;
        }
    }

    public class Raycast
    {
        private readonly Scene scene;
        private readonly Player player;

        private readonly Vector2[] crossingPoints = new Vector2[Settings.RaycastRayCount];
        private readonly float[] crossingPointsDistances = new float[Settings.RaycastRayCount];
        private readonly Polygon[] crossedObstacles = new Polygon[Settings.RaycastRayCount];
        private readonly Side[] crossedSides = new Side[Settings.RaycastRayCount];
        private readonly Ray[] rays = new Ray[Settings.RaycastRayCount];

        public Raycast(Scene scene)
        {
            this.scene = scene;
            this.player = this.scene.Player;
        }

        public RaycastData GetCrossingPointsAndDistances()
        {
            var length = Settings.RaycastRayCount;
            for (var index = 0; index < length; index++)
            {
                var angle = index * Settings.RaycastRayDensity;
                var ray = new Ray(player.Position, angle.ToRadians() - player.Rotation - Player.FieldOfView/2f);
                var crossingPoint = GetMinDistanceCrossingPoint(ray, scene.Obstacles, 
                    out float minDistance, out Polygon crossedObstacle, out Side crossedSide);
                crossingPoints[index] = crossingPoint;
                crossingPointsDistances[index] = minDistance;
                crossedObstacles[index] = crossedObstacle;
                crossedSides[index] = crossedSide;
                rays[index] = ray;
            }

            return new RaycastData(length, crossingPoints, 
                crossingPointsDistances, crossedObstacles, crossedSides, rays);
        }

        public Vector2 GetMinDistanceCrossingPoint(Ray ray, IEnumerable<Polygon> polygons, 
            out float minDistance, out Polygon crossedObstacle, out Side crossedSide)
        {
            Vector2 minDistanceCrossingPoint = default;
            minDistance = float.MaxValue;
            crossedObstacle = default;
            crossedSide = default;

            var side = Side.Vertical;
            foreach (Polygon polygon in polygons)
            {
                for (var index = 1; index < polygon.Vertices.Length + 1; index++)
                {
                    Vector2 vertex1 = polygon.Vertices[index-1], 
                        vertex2 = polygon.Vertices[index % polygon.Vertices.Length];
                    side = side == Side.Vertical ? Side.Horizontal : Side.Vertical;

                    var isCrossing = ray.IsCrossing(vertex1, vertex2, out Vector2 crossingPoint);
                    var distance = (crossingPoint-player.Position).Length();

                    if (isCrossing && distance < minDistance)
                    {
                        minDistanceCrossingPoint = crossingPoint;
                        minDistance = distance;
                        crossedObstacle = polygon;
                        crossedSide = side;
                    }
                }
            }

            return minDistanceCrossingPoint;
        }
    }
}