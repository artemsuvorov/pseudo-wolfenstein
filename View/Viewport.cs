using System.Drawing;

namespace PseudoWolfenstein.View
{
    public interface IViewport
    {
        Rectangle ClientRectangle { get; }
        Point PointToClient(Point point);
    }

    public class Viewport : IViewport
    {
        public const int DefaultWidth = 800, DefaultHeight = 600;

        private readonly IViewport display;

        public Rectangle ClientRectangle => GetClientRectangleCentered();

        public Point Center => new Point(ClientRectangle.Width / 2, ClientRectangle.Height / 2);

        public bool IsEmpty => ClientRectangle.IsEmpty;

        public int Width => ClientRectangle.Width;
        public int Height => ClientRectangle.Height;

        public Size Size => ClientRectangle.Size;
        public Point Location => ClientRectangle.Location;

        public int Top => ClientRectangle.Top;
        public int Right => ClientRectangle.Right;
        public int Left => ClientRectangle.Left;
        public int Bottom => ClientRectangle.Bottom;

        public int X => ClientRectangle.X;
        public int Y => ClientRectangle.Y;

        public Viewport(IGameForm gameForm)
        {
            display = gameForm;
        }

        public Point PointToClient(Point point)
        {
            return display.PointToClient(point);
        }

        public Rectangle GetClientRectangleCentered()
        {
            const int Width = DefaultWidth, Height = DefaultHeight;
            var x = (display.ClientRectangle.Width - Width) / 2;
            var y = (display.ClientRectangle.Height - Height) / 2;
            return new Rectangle(x, y, Width, Height);
        }
    }
}