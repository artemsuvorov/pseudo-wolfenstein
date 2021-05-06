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
                    return new Wall('S', position, Settings.WorldWallSize, texture);
                },
                ['B'] = (x, y) => 
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.BlueWall;
                    return new Wall('B', position, Settings.WorldWallSize, texture);
                },
                ['C'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.GreyColmun;
                    return new Pane('C', position, texture);
                },
                ['F'] = (x, y) =>
                {
                    var position = new Vector2((x+0.5f)*Settings.WorldWallSize, (y+0.5f)*Settings.WorldWallSize);
                    var texture = Repository.Textures.FritzTileSet;
                    var srcRect = new RectangleF(0, 0, 128, 128);
                    return new Enemy('F', position, texture, srcRect);
                },
                ['P'] = (x, y) =>
                { 
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    return new Player('P', position);
                },
                ['W'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.Wood;
                    return new Wall('W', position, Settings.WorldWallSize, texture);
                },
                ['J'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.Jail;
                    return new Wall('J', position, Settings.WorldWallSize, texture);
                },
                ['R'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.RedWall;
                    return new Wall('R', position, Settings.WorldWallSize, texture);
                },
                ['H'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Heal;
                    return new Pane('H', position, texture);
                },
                ['M'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Meal;
                    return new Pane('M', position, texture);
                },
                ['A'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Ammo;
                    return new Pane('A', position, texture);
                },
                ['O'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Oddments;
                    return new Pane('O', position, texture);
                },
                ['G'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Goods;
                    return new Pane('G', position, texture);
                },
                ['U'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Unlocker;
                    return new Pane('U', position, texture);
                },
                ['N'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.NewLevel;
                    return new Pane('N', position, texture);
                },
                ['X'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.WC;
                    return new Pane('X', position, texture);
                },
                ['I'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.ImageWet;
                    return new Pane('I', position, texture);
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