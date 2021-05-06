using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : Pane
    {
        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }
    }
}