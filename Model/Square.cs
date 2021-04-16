using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Square : Polygon
    {
        public Square(Vector2 position, float size)
            : base(new Vector2[4]
            {
                position,
                new Vector2(position.X+size, position.Y     ),
                new Vector2(position.X+size, position.Y+size),
                new Vector2(position.X,      position.Y+size),
            })
        { }
    }
}