using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class RedKey : Collectable
    {
        private int keyAmount = 1;

        public RedKey(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            if (keyAmount <= 0) return;
            player.RedKeyCount += keyAmount;
            keyAmount -= keyAmount;
        }
    }

    public class GreenKey : Collectable
    {
        private int keyAmount = 1;

        public GreenKey(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            if (keyAmount <= 0) return;
            player.GreenKeyCount += keyAmount;
            keyAmount -= keyAmount;
        }
    }

    public class BlueKey : Collectable
    {
        private int keyAmount = 1;

        public BlueKey(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            if (keyAmount <= 0) return;
            player.BlueKeyCount += keyAmount;
            keyAmount -= keyAmount;
        }
    }

    public class OrangeKey : Collectable
    {
        private int keyAmount = 1;

        public OrangeKey(char name, Vector2 position, Image texture)
            : base(name, position, texture)
        { }

        protected override void Collect(Player player)
        {
            base.Collect(player);
            if (keyAmount <= 0) return;
            player.OrangeKeyCount += keyAmount;
            keyAmount -= keyAmount;
        }
    }
}