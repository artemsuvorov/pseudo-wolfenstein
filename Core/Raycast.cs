using PseudoWolfenstein.Model;
using PseudoWolfenstein.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public enum SideDirection
    {
        None,
        Horizontal,
        Vertical,
    }

    public class RaycastData
    {
        public int Length { get; private set; }

        public RaycastDataEntry this[int index]
        {
            get => entries[index];
        }

        private readonly RaycastDataEntry[] entries;

        public RaycastData(int length, Vector2[] crossingPoints, float[] distances, 
            Polygon[] crossedObstacles, SideDirection[] crossedSides, Ray[] rays)
        {
            Length = length;

            var entries = new RaycastDataEntry[Length];
            for (var i = 0; i < Length; i++)
            {
                entries[i] = new RaycastDataEntry(crossingPoints[i],
                    distances[i], crossedObstacles[i], crossedSides[i], rays[i]);
            }

            this.entries = entries;
        }

        public class RaycastDataEntry
        {
            public Vector2 CrossingPoint { get; private set; }
            public float Distance { get; private set; }
            public Polygon CrossedObstacle { get; private set; }
            public SideDirection CrossedSide { get; private set; }
            public Ray Ray { get; private set; }

            public RaycastDataEntry(Vector2 crossingPoint, float distance, 
                Polygon crossedObstacle, SideDirection crossedSide, Ray ray)
            {
                CrossingPoint = crossingPoint;
                Distance = distance;
                CrossedObstacle = crossedObstacle;
                CrossedSide = crossedSide;
                Ray = ray;
            }
        }
    }

    public class Raycast
    {
        private readonly Scene scene;
        private readonly Player player;

        private readonly Vector2[] crossingPoints = new Vector2[Settings.RaycastRayCount];
        private readonly float[] distances = new float[Settings.RaycastRayCount];
        private readonly Polygon[] crossedObstacles = new Polygon[Settings.RaycastRayCount];
        private readonly SideDirection[] crossedSides = new SideDirection[Settings.RaycastRayCount];
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
                    out float minDistance, out Polygon crossedObstacle, out SideDirection crossedSide);
                crossingPoints[index] = crossingPoint;
                distances[index] = minDistance;
                crossedObstacles[index] = crossedObstacle;
                crossedSides[index] = crossedSide;
                rays[index] = ray;
            }

            return new RaycastData(length, crossingPoints, distances, crossedObstacles, crossedSides, rays);
        }

        public Vector2 GetMinDistanceCrossingPoint(Ray ray, IEnumerable<Polygon> polygons, 
            out float minDistance, out Polygon crossedObstacle, out SideDirection crossedSide)
        {
            Vector2 minDistanceCrossingPoint = default;
            minDistance = float.MaxValue;
            crossedObstacle = default;
            crossedSide = default;

            foreach (Polygon polygon in polygons)
            {
                for (var index = 1; index < polygon.Vertices.Length+1; index++)
                {
                    Vector2 vertex1 = polygon.Vertices[index-1], 
                            vertex2 = polygon.Vertices[index % polygon.Vertices.Length],
                            edge = vertex2 - vertex1;

                    var sideDir = MathF.Abs(Vector2.Dot(edge, Vector2.UnitX)) < MathF.Abs(Vector2.Dot(edge, Vector2.UnitY)) ?
                        SideDirection.Vertical : SideDirection.Horizontal;

                    var isCrossing = ray.IsCrossing(vertex1, vertex2, out Vector2 crossingPoint);
                    var distance = (crossingPoint-player.Position).Length();

                    if (isCrossing && distance < minDistance)
                    {
                        minDistanceCrossingPoint = crossingPoint;
                        minDistance = distance;
                        crossedObstacle = polygon;
                        crossedSide = sideDir;
                    }
                }
            }

            return minDistanceCrossingPoint;
        }
    }
}