using PseudoWolfenstein.View;
using System;
using System.Drawing;

namespace PseudoWolfenstein.Core
{
    public interface IViewport
    {
        Rectangle ClientRectangle { get; }
        Point PointToClient(Point point);
    }

    public class Viewport : IViewport
    {
        private readonly IViewport display;

        public Rectangle ClientRectangle => display.ClientRectangle;

        public Point Center => new Point(ClientRectangle.Width / 2, ClientRectangle.Height / 2);

        public bool IsEmpty => ClientRectangle.IsEmpty;

        public float Width => ClientRectangle.Width;
        public float Height => ClientRectangle.Height;

        public Size Size => ClientRectangle.Size;
        public Point Location => ClientRectangle.Location;

        public float Top => ClientRectangle.Top;
        public float Right => ClientRectangle.Right;
        public float Left => ClientRectangle.Left;
        public float Bottom => ClientRectangle.Bottom;

        public float X => ClientRectangle.X;
        public float Y => ClientRectangle.Y;

        public Viewport(IGameForm gameForm)
        {
            display = gameForm;
        }

        public Point PointToClient(Point point)
        {
            return display.PointToClient(point);
        }
    }
}