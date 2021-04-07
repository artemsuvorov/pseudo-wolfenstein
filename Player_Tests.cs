using System;

namespace PseudoWolfenstein.Tests
{
    internal class Player_Tests
    {
        private readonly static string playerPosition = Player.Position.ToString();

        public void TestPlayer()
        {
            Console.WriteLine($"Testing player! Player position: {playerPosition}!");
        }
    }
}