using System.Collections.Generic;
using System.Linq;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
        private static SceneBuilder builder;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player Player { get; private set; }

        public IReadOnlyCollection<Polygon> Obstacles => obstacles;
        public IReadOnlyCollection<Wall> Walls => walls;
        public IReadOnlyCollection<Pane> Panes => panes;

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        private readonly List<Polygon> obstacles;
        private readonly List<Wall> walls;
        private readonly List<Pane> panes;
        private readonly List<Door> doors;

        private Scene(Player player, List<Wall> walls, List<Pane> panes, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Player = player;
            this.walls = walls;
            this.panes = panes;
            this.doors = Panes.OfType<Door>().ToList();
            this.obstacles = Walls.Cast<Polygon>().Concat(Panes.Cast<Polygon>()).ToList();

            foreach (var obstacle in Obstacles)
                obstacle.Destroying += Destroy;

            foreach (var door in Panes.OfType<Door>())
                player.DoorOpening += door.Open;

            foreach (var pane in Panes.OfType<RotatingPane>())
                player.Moved += pane.UpdateTransform;

            foreach (var collectable in Panes.OfType<Collectable>())
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
        }

        public void Update()
        {
            Player.Update(this);
        }

        private void RotateDoors()
        {

        }
    }
}