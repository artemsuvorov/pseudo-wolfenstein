using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public class Scene
    {
        public Player Player { get; private set; }
        public IEnumerable<Polygon> Obstacles { get; private set; }

        private readonly Polygon square1;
        private readonly Polygon square2;

        private readonly Polygon wall1;
        private readonly Polygon wall2;
        private readonly Polygon wall3;
        private readonly Polygon wall4;

        public Scene(Player player)
        {
            Player = player;

            square1 = new Square(new Vector2(+250f, +100f), size: 120f);
            square2 = new Square(new Vector2(-450f, -250f), size: 120f);

            wall1 = new Polygon(new Vector2(-580f, -360f), new Vector2(+530f, -360f), new Vector2(+530f, -310f), new Vector2(-580f, -310f));
            wall2 = new Polygon(new Vector2(+530f, -360f), new Vector2(+580f, -360f), new Vector2(+580f, +310f), new Vector2(+530f, +310f));
            wall3 = new Polygon(new Vector2(-530f, +310f), new Vector2(+580f, +310f), new Vector2(+580f, +360f), new Vector2(-530f, +360f));
            wall4 = new Polygon(new Vector2(-580f, -310f), new Vector2(-530f, -310f), new Vector2(-530f, +360f), new Vector2(-580f, +360f));

            Obstacles = new Polygon[]
            {
                wall1, wall2, wall3, wall4,
                square1, square2
            };
        }
    }
}