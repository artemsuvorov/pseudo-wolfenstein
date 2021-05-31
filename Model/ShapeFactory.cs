using PseudoWolfenstein.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    internal class ShapeFactory
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
                    var texture = Repository.Textures.GreyColumn;
                    return new RotatingPane('C', position, texture);
                },
                ['F'] = (x, y) =>
                {
                    var position = new Vector2((x+0.5f)*Settings.WorldWallSize, (y+0.5f)*Settings.WorldWallSize);
                    var texture = Repository.Textures.EnemyFrames.IdleFritz1;
                    var srcRect = new RectangleF(0, 0, 128, 128);
                    return new Enemy('F', position, texture, srcRect);
                },
                ['P'] = (x, y) =>
                { 
                    var position = new Vector2(x * Settings.WorldWallSize + Settings.WorldWallSize / 2f,
                         y * Settings.WorldWallSize + Settings.WorldWallSize / 2f);
                    return new PlaceHolder('P', position);
                },
                ['W'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.WoodWall;
                    return new Wall('W', position, Settings.WorldWallSize, texture);
                },
                ['J'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.BlueJail;
                    return new Wall('J', position, Settings.WorldWallSize, texture);
                },
                ['i'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.RedJail;
                    return new Wall('i', position, Settings.WorldWallSize, texture);
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
                    var texture = Repository.Textures.HealPack;
                    return new HealPack('H', position, texture);
                },
                ['M'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.SmallTable;
                    return new RotatingPane('M', position, texture);
                },
                ['A'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Ammo;
                    return new AmmoPack('A', position, texture);
                },
                ['O'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Bones;
                    return new RotatingPane('O', position, texture);
                },
                ['G'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.ScoreItemCross;
                    return new CrossItem('G', position, texture);
                },
                ['N'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.VaseFrames[0];
                    return new NextLevelVase('N', position, texture);
                },
                ['X'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.WC;
                    return new RotatingPane('X', position, texture);
                },
                ['I'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Puddle;
                    return new RotatingPane('I', position, texture);
                },
                ['D'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Door;
                    return new Door('D', position, texture);
                },
                ['g'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.GreenDoor;
                    return new GreenDoor('g', position, texture);
                },
                ['o'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.OrangeDoor;
                    return new OrangeDoor('o', position, texture);
                },
                ['b'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.BlueDoor;
                    return new BlueDoor('b', position, texture);
                },
                ['r'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.RedDoor;
                    return new RedDoor('r', position, texture);
                },
                ['q'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.GreenKey;
                    return new GreenKey('q', position, texture);
                },
                ['w'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.OrangeKey;
                    return new OrangeKey('w', position, texture);
                },
                ['e'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.BlueKey;
                    return new BlueKey('e', position, texture);
                },
                ['t'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.RedKey;
                    return new RedKey('t', position, texture);
                },
                ['p'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.LockedDoor;
                    return new LockedDoor('p', position, texture);
                },
                ['a'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.BigTable;
                    return new RotatingPane('a', position, texture);
                },
                ['s'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Knight;
                    return new RotatingPane('s', position, texture);
                },
                ['x'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.PotPlant;
                    return new RotatingPane('x', position, texture);
                },
                ['k'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.GoldWall;
                    return new Wall('k', position, Settings.WorldWallSize, texture);
                },
                ['K'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.SendWall;
                    return new Wall('K', position, Settings.WorldWallSize, texture);
                },
                ['l'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Tree;
                    return new RotatingPane('l', position, texture);
                },
                ['L'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.Left;
                    return new Wall('L', position, Settings.WorldWallSize, texture);
                },
                ['f'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.Right;
                    return new Wall('f', position, Settings.WorldWallSize, texture);
                },
                ['Q'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.BrickWall;
                    return new Wall('Q', position, Settings.WorldWallSize, texture);
                },
                ['T'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.TextBlueWall;
                    return new Wall('T', position, Settings.WorldWallSize, texture);
                },
                ['j'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.TextStoneWall;
                    return new Wall('j', position, Settings.WorldWallSize, texture);
                },
                ['E'] = (x, y) =>
                {
                    var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                    var texture = Repository.Textures.BadGuy;
                    return new Wall('E', position, Settings.WorldWallSize, texture);
                },
                ['d'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.ScoreItemChest;
                    return new ChestItem('d', position, texture);
                },
                ['m'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.ScoreItemCrown;
                    return new CrownItem('m', position, texture);
                },
                ['n'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.BloodyBones;
                    return new RotatingPane('n', position, texture);
                },
                ['h'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Barrel;
                    return new RotatingPane('h', position, texture);
                },
                ['1'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Flowey;
                    return new RotatingPane('1', position, texture);
                },
                ['5'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Knife;
                    return new Knife('5', position, texture);
                },
                ['6'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.Pistol;
                    return new Pistol('6', position, texture);
                },
                ['7'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.MachineGun;
                    return new MachineGun('7', position, texture);
                },
                ['8'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.ChainGun;
                    return new ChainGun('8', position, texture);
                },
                ['9'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.FlameThrower;
                    return new FlameThrower('9', position, texture);
                },
                ['4'] = (x, y) =>
                {
                    var position = new Vector2((x + 0.5f) * Settings.WorldWallSize, (y + 0.5f) * Settings.WorldWallSize);
                    var texture = Repository.Textures.RocketLauncher;
                    return new RocketLauncher('4', position, texture);
                },
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
            if (!IsShape(c)) return default;
            return shapeCtorByChar[c](x, y);
        }

        private static bool IsShape(char c)
        {
            return shapeCtorByChar.ContainsKey(c);
        }
    }
}