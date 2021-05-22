using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class PlaceHolder : Shape
    {
        public PlaceHolder(char name, Vector2 position)
            : base(name, position, texture: default, srcRect: default)
        { }

        public override void Draw(Graphics graphics) { }
    }
}