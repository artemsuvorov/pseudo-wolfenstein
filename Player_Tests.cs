using System;
using System.Numerics;

namespace PseudoWolfenstein.Tests
{
    internal static class Player_Tests
    {
        private static readonly Vector2 playerPosition = Player.Position;

        public static void TestPlayer()
        {
            Console.WriteLine($"Player test! Player position : {playerPosition}");
        }
    }
}