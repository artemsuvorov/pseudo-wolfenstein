using PseudoWolfenstein.Core;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class NextLevelVase : Shotable
    {
        public event GameEventHandler Triggered;

        public NextLevelVase(char name, Vector2 position, Image texture)
            : base(name, position, texture, default)
        { }

        protected override void OnShot(object sender, GameEventArgs e)
        {
            Triggered?.Invoke(sender, e);
        }
    }
}