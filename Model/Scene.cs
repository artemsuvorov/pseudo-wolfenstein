using PseudoWolfenstein.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
        public string Name { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Vector2 Start { get; private set; }

        public List<Polygon> Obstacles => obstacles;
        public List<Wall> Walls => walls;
        public List<Pane> Panes => panes;

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        public event GameEventHandler Finished;
        
        private static SceneBuilder builder;
        private Player player;

        private readonly List<Polygon> obstacles;
        private readonly List<Wall> walls;
        private readonly List<Pane> panes;
        private readonly List<Door> doors;
        private readonly List<Enemy> enemies;
        private readonly List<Shotable> shotables;
        private readonly List<RotatingPane> rotatingPanes;
        private readonly List<Collectable> collectables;

        private Scene(string name, Vector2 playerPosition, List<Wall> walls, List<Pane> panes, int width, int height)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.Start = playerPosition;

            this.walls = walls;
            this.panes = panes;
            this.doors = panes.OfType<Door>().ToList();
            this.enemies = panes.OfType<Enemy>().ToList();
            this.obstacles = walls.Cast<Polygon>().Concat(Panes.Cast<Polygon>()).ToList();
            this.shotables = panes.OfType<Shotable>().ToList();
            this.rotatingPanes = panes.OfType<RotatingPane>().ToList();
            this.collectables = panes.OfType<Collectable>().ToList();

            foreach (var obstacle in Obstacles)
                obstacle.Destroying += Destroy;
            foreach (var vase in panes.OfType<NextLevelVase>().ToList())
                vase.Triggered += OnNextLevelVaseShot;
        }

        public void LoadPlayer(Player player)
        {
            this.player = player;

            foreach (var door in doors)
                this.player.Interacting += door.Open;
            foreach (var shotable in shotables)
                this.player.Shot += shotable.OnPlayerShot;
            foreach (var pane in rotatingPanes)
                this.player.Moved += pane.UpdateTransform;
            foreach (var collectable in collectables)
                this.player.Moved += collectable.Collide;
        }

        private void OnNextLevelVaseShot(object sender, GameEventArgs e)
        {
            foreach (var door in doors)
                this.player.Interacting -= door.Open;
            foreach (var shotable in shotables)
                this.player.Shot -= shotable.OnPlayerShot;
            foreach (var pane in rotatingPanes)
                this.player.Moved -= pane.UpdateTransform;
            foreach (var collectable in collectables)
                this.player.Moved -= collectable.Collide;
            Finished?.Invoke(sender, e);
        }

        private void Destroy(object sender, Polygon shape)
        {
            obstacles.Remove(shape);
            if (shape is Wall wall)
                walls.Remove(wall);
            else if (shape is Pane pane)
                panes.Remove(pane);
            if (shape is Door door)
                doors.Remove(door);
            else if (shape is Collectable collectable)
                if (player is object)
                this.player.Moved -= collectable.Collide;
            else if (shape is Enemy enemy)
                enemies.Remove(enemy);
        }

        public void Update()
        {
            player?.Update();
        }

        public void Animate(object sender, EventArgs e)
        {
            player?.Animate();
            foreach (var enemy in enemies)
                enemy.Animate();
        }

        public bool GetMinDistanceWallCross(Vector2 v1, Vector2 v2, out Vector2 location)
        {
            return GetMinDistanceWallCross(v1, v2, out location, out _);
        }

        public bool GetMinDistanceWallCross(Vector2 v1, Vector2 v2, out Vector2 location, out float minDistance)
        {
            if (player is null)
                throw new InvalidOperationException("player cannot be null.");

            var crossFound = false;
            minDistance = float.MaxValue;
            location = default(Vector2);

            foreach (var wall in walls)
            {
                for (var index = 1; index < wall.Vertices.Length + 1; index++)
                {
                    Vector2 vertex1 = wall.Vertices[index - 1],
                            vertex2 = wall.Vertices[index % wall.Vertices.Length];

                    var isCrossing = MathF2D.AreSegmentsCrossing(v1, v2, vertex1, vertex2, out var cross);
                    if (!isCrossing) continue;

                    var distance = (cross - this.player.Position).Length();
                    if (distance < minDistance)
                    {
                        location = cross;
                        minDistance = distance;
                        crossFound = true;
                    }
                }
            }

            return crossFound;
        }

        //private List<Vector2> GetWallCrosses(Vector2 v1, Vector2 v2)
        //{
        //    var crosses = new List<Vector2>();

        //    foreach (var wall in walls)
        //    {
        //        for (var index = 1; index < wall.Vertices.Length + 1; index++)
        //        {
        //            Vector2 vertex1 = wall.Vertices[index - 1], 
        //                vertex2 = wall.Vertices[index % wall.Vertices.Length];
        //            if (MathF2D.AreSegmentsCrossing(v1, v2, vertex1, vertex2, out var cross))
        //                crosses.Add(cross);
        //        }
        //    }

        //    return crosses;
        //}
    }
}