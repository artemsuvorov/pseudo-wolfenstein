using PseudoWolfenstein.Model;
using System;

namespace PseudoWolfenstein.Core
{

    public delegate void GameEventHandler(object sender, GameEventArgs e);

    public class GameEventArgs : EventArgs
    {
        public Scene Scene { get; }
        public Player Player { get; }

        public GameEventArgs(Scene scene)
        {
            Scene = scene;
            Player = scene.Player;
        }
    }
}