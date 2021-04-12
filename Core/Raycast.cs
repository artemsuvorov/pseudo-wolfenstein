using PseudoWolfenstein.Utils;
using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public class RaycastData
    {
        public Vector2[] CrossingPoints { get; private set; }
        public float[] CrossingPointsDistances { get; private set; }

        public RaycastData(Vector2[] crossingPoints, float[] crossingPointsDistances)
        {
            CrossingPoints = crossingPoints;
            CrossingPointsDistances = crossingPointsDistances;
        }
    }

    public class Raycast
    {
        private readonly Scene scene;
        private readonly Player player;

        private readonly Vector2[] crossingPoints = new Vector2[Settings.RaycastRayCount];
        private readonly float[] crossingPointsDistances = new float[Settings.RaycastRayCount];

        public Raycast(Scene scene)
        {
            this.scene = scene;
            this.player = this.scene.Player;
        }

        public RaycastData GetCrossingPointsAndDistances()
        {
            for (var index = 0; index < Settings.RaycastRayCount; index++)
            {
                var angle = index * Settings.RaycastRayDensity;
                var ray = new Ray(player.Position, angle.ToRadians() - player.Rotation - Player.FieldOfView/2f);
                var crossingPoint = GetMinDistanceCrossingPoint(ray, scene.Obstacles, out float minDistance);
                crossingPoints[index] = crossingPoint;
                crossingPointsDistances[index] = minDistance;
            }

            return new RaycastData(crossingPoints, crossingPointsDistances);
        }

        public Vector2 GetMinDistanceCrossingPoint(Ray ray, IEnumerable<Polygon> polygons, out float minDistance)
        {
            var minDistanceCrossingPoint = default(Vector2);
            minDistance = float.MaxValue;

            foreach (Polygon polygon in polygons)
            {
                for (var index = 1; index < polygon.Vertices.Length + 1; index++)
                {
                    Vector2 vertex1 = polygon.Vertices[index-1], 
                        vertex2 = polygon.Vertices[index % polygon.Vertices.Length];

                    var isCrossing = ray.IsCrossing(vertex1, vertex2, out Vector2 crossingPoint);
                    var distance = (crossingPoint-player.Position).Length();

                    if (isCrossing && distance < minDistance)
                    {
                        minDistanceCrossingPoint = crossingPoint;
                        minDistance = distance;
                    }
                }
            }

            return minDistanceCrossingPoint;
        }
    }
}