using PseudoWolfenstein.Core;
using System.Drawing;
using System.Numerics;
namespace PseudoWolfenstein.Model
{
    public class Door : Pane
    {
        public Door(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        public void Open(object sender, GameEventArgs e)
        {
            var dst = (Center - e.Player.Position).Length();
            if (dst > Settings.WorldWallSize) return;
            Destroy();
        }

        internal void Rotate()
        {
            Vertices = new Vector2[]
            {
                Center - new Vector2(0f, Settings.WorldWallSize * 0.5f),
                Center + new Vector2(0f, Settings.WorldWallSize * 0.5f)
            };
        }
    }
}