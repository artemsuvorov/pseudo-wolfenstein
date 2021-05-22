using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Pane : Polygon
    {
        public Vector2 Center { get; protected set; }

        public Pane(char name, Vector2 position, Image texture)
            : base(name, texture,
                   position - new Vector2(Settings.WorldWallSize * 0.5f, 0f),
                   position + new Vector2(Settings.WorldWallSize * 0.5f, 0f))
        {
            Center = position;
        }
        public Pane(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, texture, srcRect,
                   position - new Vector2(Settings.WorldWallSize * 0.5f, 0f),
                   position + new Vector2(Settings.WorldWallSize * 0.5f, 0f))
        {
            Center = position;
        }
        public Pane(char name, Vector2 position)
            : this(name, position, texture: default, srcRect: default)
        { }
    }
}