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
        public List<Enemy> Enemies => enemies;

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        public event GameEventHandler Finished;
        
        private static SceneBuilder builder;
        private Player player;

        private readonly Shape[,] shapes;
        private List<Polygon> obstacles => walls.Cast<Polygon>().Concat(Panes).ToList();
        private List<Wall> walls => shapes.Cast<Shape>().Where(shape => shape is Wall).Cast<Wall>().ToList();
        private List<Pane> panes => shapes.Cast<Shape>().Where(shape => shape is Pane).Cast<Pane>().ToList();
        private List<Door> doors => panes.OfType<Door>().ToList();
        private List<Enemy> enemies => panes.OfType<Enemy>().ToList();
        private List<Shotable> shotables => panes.OfType<Shotable>().ToList();
        private List<RotatingPane> rotatingPanes => panes.OfType<RotatingPane>().ToList();
        private List<Collectable> collectables => panes.OfType<Collectable>().ToList();

        internal Shape this[Vector2 locationIndex]
        {
            get => this[(int)locationIndex.X, (int)locationIndex.Y];
            private set => this[(int)locationIndex.X, (int)locationIndex.Y] = value;
        }

        internal Shape this[int x, int y]
        {
            get => shapes[x, y];
            private set => shapes[x, y] = value;
        }

        private Scene(string name, Vector2 playerPosition, Shape[,] shapes, int width, int height)
        {
            this.Name = name;
            this.Width = width;
            this.Height = height;
            this.shapes = shapes;
            this.Start = playerPosition;
            //this.shapes = shapes;
            //this.walls = walls;
            //this.panes = panes;
            //this.doors = panes.OfType<Door>().ToList();
            //this.enemies = panes.OfType<Enemy>().ToList();
            //this.obstacles = walls.Cast<Polygon>().Concat(Panes.Cast<Polygon>()).ToList();
            //this.shotables = panes.OfType<Shotable>().ToList();
            //this.rotatingPanes = panes.OfType<RotatingPane>().ToList();
            //this.collectables = panes.OfType<Collectable>().ToList();

            foreach (var obstacle in obstacles)
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
            {
                pane.LookAt(this.player.Position);
                this.player.Moved += pane.UpdateTransform;
            }
            foreach (var collectable in collectables)
                this.player.Moved += collectable.Collide;
        }

        private void OnNextLevelVaseShot(object sender, EventArgs e)
        {
            foreach (var door in doors)
                this.player.Interacting -= door.Open;
            foreach (var shotable in shotables)
                this.player.Shot -= shotable.OnPlayerShot;
            foreach (var pane in rotatingPanes)
                this.player.Moved -= pane.UpdateTransform;
            foreach (var collectable in collectables)
                this.player.Moved -= collectable.Collide;
            Finished?.Invoke(this, new GameEventArgs(this, player));
        }

        private void Destroy(object sender, Polygon shape)
        {
            var index = ToIndexCoords(shape.Position);
            this[index] = default;

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
            foreach (var enemy in enemies)
                enemy.Update(this, player);
        }

        public void Animate(object sender, EventArgs e)
        {
            if (player is null) return;

            player.Animate();
            foreach (var enemy in obstacles.OfType<IAnimatable>().ToList())
                enemy.Animate();
        }

        public bool Contains(Vector2 locationIndex)
        {
            return 
                locationIndex.X >= 0 && locationIndex.X < Width && 
                locationIndex.Y >= 0 && locationIndex.Y < Height;
        }

        public static Vector2 ToIndexCoords(Vector2 location)
        {
            var v = location / Settings.WorldWallSize;
            return new Vector2(MathF.Floor(v.X), MathF.Floor(v.Y));
        }

        public static Vector2 ToSceneCoords(Vector2 locationIndex)
        {
            return locationIndex * Settings.WorldWallSize;
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

            foreach (var wall in obstacles.Where(x => !(x is Shotable) && !(x is Collectable)).ToList())
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