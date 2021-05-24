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

        public virtual void Open(object sender, GameEventArgs e)
        {
            if (IsCloseTo(e.Player.Position))
                Destroy();
        }

        protected bool IsCloseTo(Vector2 position)
        {
            var dst = (Center - position).Length();
            return dst <= Settings.WorldWallSize;
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

    public class RedDoor : Door
    {
        public RedDoor(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        public override void Open(object sender, GameEventArgs e)
        {
            if (e.Player.RedKeyCount <= 0 || !IsCloseTo(e.Player.Position))
                return;
            e.Player.RedKeyCount--;
            Destroy();
        }
    }

    public class GreenDoor : Door
    {
        public GreenDoor(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        public override void Open(object sender, GameEventArgs e)
        {
            if (e.Player.GreenKeyCount <= 0 || !IsCloseTo(e.Player.Position))
                return;
            e.Player.GreenKeyCount--;
            Destroy();
        }
    }

    public class BlueDoor : Door
    {
        public BlueDoor(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        public override void Open(object sender, GameEventArgs e) 
        {
            if (e.Player.BlueKeyCount <= 0 || !IsCloseTo(e.Player.Position))
                return;
            e.Player.BlueKeyCount--;
            Destroy();
        }
    }

    public class OrangeDoor : Door
    {
        public OrangeDoor(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        public override void Open(object sender, GameEventArgs e)
        {
            if (e.Player.OrangeKeyCount <= 0 || !IsCloseTo(e.Player.Position))
                return;
            e.Player.OrangeKeyCount--;
            Destroy();
        }
    }

    public class LockedDoor : Door
    {
        public LockedDoor(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        public override void Open(object sender, GameEventArgs e) { }
    }
}