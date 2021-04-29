using PseudoWolfenstein.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    internal static class ShapeFactory
    {
        private static readonly IReadOnlyDictionary<char, Func<int, int, Shape>> shapeCtorByChar;

        static ShapeFactory()
        {
            shapeCtorByChar = new Dictionary<char, Func<int, int, Shape>> 
            {
                ['S'] = (x, y) => 
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.StoneWall;
                    return new Wall(position, Settings.WorldWallSize, texture);
                },
                ['B'] = (x, y) => 
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.BlueWall;
                    return new Wall(position, Settings.WorldWallSize, texture);
                },
                ['C'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.GreyColmun;
                    return new Pane(position, texture);
                },
                ['F'] = (x, y) =>
                {
                    var position = new Vector2((x+0.5f)*Settings.WorldWallSize, (y+0.5f)*Settings.WorldWallSize);
                    var texture = Repository.Textures.FritzTileSet;
                    var srcRect = new RectangleF(0, 0, 128, 128);
                    return new Enemy(position, texture, srcRect);
                },
                ['P'] = (x, y) =>
                { 
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    return new Player(position);
                },
                ['O'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.Wood_Dark;
                    return new Wall(position, Settings.WorldWallSize, texture);
                }
            };
        }

        public static Shape InstantiateShapeAt(int x, int y, char c)
        {
            if (!IsShape(c))
                throw new ArgumentException($"Invalid character {c}. " +
                    $"There are no shapes associated with that chraracter.", nameof(c));
            return shapeCtorByChar[c](x, y);
        }

        public static Shape InstantiateShapeAtOrDefault(int x, int y, char c)
        {
            if (!IsShape(c))
                return default;
            return shapeCtorByChar[c](x, y);
        }

        private static bool IsShape(char c)
        {
            return shapeCtorByChar.ContainsKey(c);
        }
    }
}