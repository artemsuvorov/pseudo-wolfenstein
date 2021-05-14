using PseudoWolfenstein.Core;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
        private static SceneBuilder builder;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player Player { get; private set; }

        public List<Polygon> Obstacles => obstacles;
        public List<Wall> Walls => walls;
        public List<Pane> Panes => panes;

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        private readonly List<Polygon> obstacles;
        private readonly List<Wall> walls;
        private readonly List<Pane> panes;
        private readonly List<Door> doors;
        private readonly List<Enemy> enemies;

        private Scene(Player player, List<Wall> walls, List<Pane> panes, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Player = player;
            this.walls = walls;
            this.panes = panes;
            this.doors = Panes.OfType<Door>().ToList();
            this.enemies = Panes.OfType<Enemy>().ToList();
            this.obstacles = Walls.Cast<Polygon>().Concat(Panes.Cast<Polygon>()).ToList();

            foreach (var obstacle in Obstacles)
                obstacle.Destroying += Destroy;

            foreach (var door in doors)
                player.Interacting += door.Open;

            foreach (var enemy in enemies)
                player.Shot += enemy.OnPlayerShot;

            foreach (var pane in Panes.OfType<RotatingPane>().ToList())
                player.Moved += pane.UpdateTransform;

            foreach (var collectable in Panes.OfType<Collectable>().ToList())
                player.Moved += collectable.Collide;
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
                Player.Moved -= collectable.Collide;
            else if (shape is Enemy enemy)
                enemies.Remove(enemy);
        }

        public void Update()
        {
            Player.Update();
        }

        public void Animate(object sender, System.EventArgs e)
        {
            Player.Animate();
            foreach (var enemy in enemies)
                enemy.Animate();
        }

        public bool GetMinDistanceWallCross(Vector2 v1, Vector2 v2, out Vector2 location)
        {
            return GetMinDistanceWallCross(v1, v2, out location, out _);
        }

        public bool GetMinDistanceWallCross(Vector2 v1, Vector2 v2, out Vector2 location, out float minDistance)
        {
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

                    var distance = (cross - Player.Position).Length();
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