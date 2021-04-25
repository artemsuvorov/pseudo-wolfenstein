using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : Pane
    {
        public Enemy(Vector2 position, Image texture, RectangleF srcRect)
            : base(position, texture, srcRect)
        { }
    }
}