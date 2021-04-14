using System;
using System.Collections.Generic;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public class Scene
    {
        private const string DefaultScene =
            "######\r\n" +
            "#    #\r\n" +
            "#    #\r\n" +
            "#    #\r\n" +
            "#    #\r\n" +
            "#    #\r\n" +
            "######\r\n";

        private const float wallSize = 2f;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player Player { get; private set; }
        public IEnumerable<Polygon> Obstacles { get; private set; }

        public static Scene Default(Player player)
        {
            return Scene.FromString(DefaultScene, player);
        }

        public static Scene FromString(string source, Player player)
        {
            var lines = source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            var height = lines.Length;
            var width = lines[0].Length;

            var obstacles = ParseObstacles(lines, width, height);
            var scene = new Scene(player, obstacles, width, height);
            PlacePlayerInCenter(player, width, height);
            return scene;
        }

        private Scene(Player player, IEnumerable<Polygon> obstacles, int width, int height)
        {
            Player = player;
            Obstacles = obstacles;
            Width = width;
            Height = height;
        }

        private static IEnumerable<Polygon> ParseObstacles(string[] lines, int width, int height)
        {
            for (var y = 0; y < height; y++)
                for (var x = 0; x < width; x++)
                {
                    if (lines[y][x] != '#') continue;
                    var position = new Vector2(x * wallSize, y * wallSize);
                    yield return new Square(position, wallSize);
                }
        }

        private static void PlacePlayerInCenter(Player player, int width, int height)
        {
            player.X = width / 2.0f * wallSize;
            player.Y = height / 2.0f * wallSize;
        }
    }
}