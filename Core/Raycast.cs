using PseudoWolfenstein.Model;
using PseudoWolfenstein.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public enum SideDirection
    {
        None,
        Horizontal,
        Vertical,
    }

    public class Cross
    {
        public Vector2 Location { get; private set; }
        public float Distance { get; private set; }
        public Polygon CrossedObstacle { get; private set; }
        public SideDirection CrossedSide { get; private set; }

        public Cross(Vector2 location, float distance, Polygon crossedObstacle, SideDirection crossedSide)
        {
            Location = location;
            Distance = distance;
            CrossedObstacle = crossedObstacle;
            CrossedSide = crossedSide;
        }
    }

    public class RaycastDataEntry : IReadOnlyCollection<Cross>
    {
        public int Count => crossData.Length;
        public Ray Ray { get; private set; }

        public Cross this[int index]
        {
            get => crossData[index];
        }

        private readonly Cross[] crossData;

        public RaycastDataEntry(Ray ray, Cross[] crossData)
        {
            Ray = ray;
            this.crossData = crossData;
        }

        public IEnumerator<Cross> GetEnumerator()
        {
            return ((IEnumerable<Cross>)crossData).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class RaycastData : IReadOnlyCollection<RaycastDataEntry>
    {
        public int Count => entries.Length;

        public RaycastDataEntry this[int index]
        {
            get => entries[index];
        }

        private readonly RaycastDataEntry[] entries;

        public RaycastData(RaycastDataEntry[] entries)
        {
            this.entries = entries;
        }

        public IEnumerator<RaycastDataEntry> GetEnumerator()
        {
            return ((IEnumerable<RaycastDataEntry>)entries).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class Raycast
    {
        private readonly Scene scene;
        private readonly Player player;

        public Raycast(Scene scene)
        {
            this.scene = scene;
            this.player = this.scene.Player;
        }

        public RaycastData GetCrossingPointsAndDistances()
        {
            var length = Settings.RaycastRayCount;
            var entries = new RaycastDataEntry[length];

            for (var index = 0; index < length; index++)
            {
                var angle = index * Settings.RaycastRayDensity;
                var ray = new Ray(player.Position, angle.ToRadians() - player.Rotation - Player.FieldOfView / 2f);
                entries[index] = GetCrossingPoints(ray, scene.Obstacles);
            }

            return new RaycastData(entries);
        }

        public RaycastDataEntry GetCrossingPoints(Ray ray, IEnumerable<Polygon> polygons)
        {
            const int MaxEntryCapacity = 12;
            var crossData = new List<Cross>(MaxEntryCapacity);

            foreach (Polygon polygon in polygons)
            {
                for (var index = 1; index < polygon.Vertices.Length+1; index++)
                {
                    //if (crossDataIndex >= MaxEntryCapacity) break;

                    Vector2 vertex1 = polygon.Vertices[index-1],
                            vertex2 = polygon.Vertices[index % polygon.Vertices.Length],
                            edge = vertex2 - vertex1;

                    var sideDir = MathF.Abs(Vector2.Dot(edge, Vector2.UnitX)) < MathF.Abs(Vector2.Dot(edge, Vector2.UnitY)) ?
                        SideDirection.Vertical : SideDirection.Horizontal;

                    var isCrossing = ray.IsCrossing(vertex1, vertex2, out Vector2 crossLocation);
                    if (!isCrossing) continue;
                    
                    var distance = (crossLocation - player.Position).Length();
                    crossData.Add(new Cross(crossLocation, distance, polygon, sideDir));
                }
            }

            crossData.Sort((cross, other) => other.Distance.CompareTo(cross.Distance));
            return new RaycastDataEntry(ray, crossData.TakeLast(Settings.DrawLayers).ToArray());
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