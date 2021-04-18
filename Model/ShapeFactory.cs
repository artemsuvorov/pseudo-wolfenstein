﻿using System;
using System.Collections.Generic;
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
                    {
                        'S', (x, y) =>  {
                            var position = new Vector2(x * Settings.WorldWallSize, y * Settings.WorldWallSize);
                            return new Square(position, Settings.WorldWallSize);
                        }
                    },
                    {
                        'P', (x, y) => new Player(x * Settings.WorldWallSize, y * Settings.WorldWallSize)
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