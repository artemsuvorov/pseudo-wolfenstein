using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PseudoWolfenstein.Core
{
    internal static class Repository
    {
        private static TextureRepository textures;

        public static string ProjectDirectoryPath =>
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

        public static TextureRepository Textures => textures ??= new TextureRepository();

        public static string GetTexturePath(string textureName)
        {
            return Path.Combine(ProjectDirectoryPath, "Textures", textureName);
        }
        internal class TextureRepository
        {
            internal class EnemyTextureCollection
            {
                private readonly List<Bitmap> frames = new(16);

                public Bitmap this[int index]
                {
                    get => frames[index];
                }

                public Bitmap IdleFritz { get; private set; }
                public Bitmap ShotFritz { get; private set; }
                public Bitmap FritzDeathFrame1 { get; private set; }
                public Bitmap FritzDeathFrame2 { get; private set; }
                public Bitmap FritzDeathFrame3 { get; private set; }
                public Bitmap FritzDeathFrame4 { get; private set; }
                public Bitmap DeadFritz { get; private set; }

                private readonly Func<string, Func<Bitmap, Color>, Bitmap> TextureLoader;

                public EnemyTextureCollection(Func<string, Func<Bitmap, Color>, Bitmap> textureLoader)
                {
                    TextureLoader = textureLoader;
                    IdleFritz = LoadEnemyTextureFrame("IdleFritz.png");
                    ShotFritz = LoadEnemyTextureFrame("ShotFritz.png");
                    FritzDeathFrame1 = LoadEnemyTextureFrame("FritzDeathFrame1.png");
                    FritzDeathFrame2 = LoadEnemyTextureFrame("FritzDeathFrame2.png");
                    FritzDeathFrame3 = LoadEnemyTextureFrame("FritzDeathFrame3.png");
                    FritzDeathFrame4 = LoadEnemyTextureFrame("FritzDeathFrame4.png");
                    DeadFritz = LoadEnemyTextureFrame("DeadFritz.png");
                }

                private Bitmap LoadEnemyTextureFrame(string textureName)
                {
                    var texture = TextureLoader(textureName, texture => texture.GetPixel(0, 0));
                    frames.Add(texture);
                    return texture;
                }

            }

            private const int TextureRepoCapactity = 256;

            public EnemyTextureCollection EnemyFrames { get; private set; }

            public Bitmap WeaponsTileSet { get; private set; }
            public Bitmap StoneWall { get; private set; }
            public Bitmap BlueWall { get; private set; }
            public Bitmap RedWall { get; private set; }
            public Bitmap WoodWall { get; private set; }
            public Bitmap BlueJail { get; private set; }
            public Bitmap Door { get; private set; }
            public Bitmap GreyColumn { get; private set; }
            public Bitmap WC { get; private set; }
            public Bitmap Puddle { get; private set; }
            public Bitmap Bones { get; private set; }
            public Bitmap Ammo { get; private set; }
            public Bitmap Heal { get; private set; }
            public Bitmap SmallTable { get; private set; }
            public Bitmap ScoreItemCross { get; private set; }
            public Bitmap NextLevelVase { get; private set; }
            public Bitmap RedKey { get; set; }
            public Bitmap RedDoor { get; set; }
            public Bitmap OrangeDoor { get; set; }
            public Bitmap OrangeKey { get; set; }
            public Bitmap BlueDoor { get; set; }
            public Bitmap GreenDoor { get; set; }
            public Bitmap BlueKey { get; set; }
            public Bitmap GreenKey { get; set; }
            public Bitmap LockedDoor { get; set; }
            public Bitmap BigTable { get; set; }
            public Image RedJail { get; set; }
            public Bitmap Knight { get; set; }
            public Bitmap TextBlueWall { get; set; }
            public Bitmap TextStoneWall { get; set; }
            public Bitmap PotPlant { get; set; }
            public Bitmap BadGuy { get; set; }
            public Bitmap Barrel { get; set; }
            public Bitmap BloodyBones { get; set; }
            public Bitmap ScoreItemChest { get; set; }
            public Bitmap ScoreItemCrown { get; set; }
            public Bitmap BrickWall { get; set; }
            public Bitmap VaseCracks { get; set; }
            public Bitmap BrokenVase { get; set; }
            public Bitmap Left { get; set; }
            public Bitmap Right { get; set; }
            public Bitmap Tree { get; set; }
            public Bitmap SendWall { get; set; }
            public Bitmap GoldWall { get; set; }
            public Bitmap Flowey { get; set; }

            private readonly HashSet<Bitmap> textures = new(TextureRepoCapactity);

            public TextureRepository()
            {
                EnemyFrames = new EnemyTextureCollection(LoadTexture);

                //walls
                StoneWall = LoadTexture("StoneWall.bmp");
                BlueWall = LoadTexture("BlueWall.bmp");
                WoodWall = LoadTexture("WoodWall.bmp");
                RedWall = LoadTexture("RedWall.gif");
                BlueJail = LoadTexture("BlueJail.bmp");
                RedJail = LoadTexture("RedJail.GIF");
                TextBlueWall = LoadTexture("TextBlueWall.bmp");
                TextStoneWall = LoadTexture("TextStoneWall.bmp");
                BrickWall = LoadTexture("BrickWall.bmp");
                BadGuy = LoadTexture("Illustration.bmp");
                Left = LoadTexture("Left.bmp");
                Right = LoadTexture("Right.bmp");
                GoldWall = LoadTexture("GoldWall.bmp");
                SendWall = LoadTexture("SendWall.bmp");

                //decorations
                GreyColumn = LoadTexture("Column.bmp", texture => texture.GetPixel(0,0));
                Puddle = LoadTexture("Puddle.bmp", texture => texture.GetPixel(0, 0));
                SmallTable = LoadTexture("SmallTable.bmp", texture => texture.GetPixel(0, 0));
                BigTable = LoadTexture("BigTable.bmp", texture => texture.GetPixel(0, 0));
                Bones = LoadTexture("Bones.bmp", texture => texture.GetPixel(0, 0));
                WC = LoadTexture("WC.bmp", texture => texture.GetPixel(0, 0));
                Knight = LoadTexture("Knight.bmp", texture => texture.GetPixel(0, 0));
                PotPlant = LoadTexture("PotPlant.gif", texture => texture.GetPixel(0, 0));
                Barrel = LoadTexture("Barrel.bmp", texture => texture.GetPixel(0, 0));
                BloodyBones = LoadTexture("Bloody Bones.bmp", texture => texture.GetPixel(0, 0));
                Tree = LoadTexture("Tree.bmp", texture => texture.GetPixel(0, 0));

                //collectable items
                Ammo = LoadTexture("Ammo.bmp", texture => texture.GetPixel(0, 0));
                Heal = LoadTexture("Heal.bmp", texture => texture.GetPixel(0, 0));
                ScoreItemCross = LoadTexture("Cross.bmp", texture => texture.GetPixel(0, 0));
                ScoreItemChest = LoadTexture("Chest.bmp", texture => texture.GetPixel(0, 0));
                ScoreItemCrown = LoadTexture("Crown.bmp", texture => texture.GetPixel(0, 0));

                //doors and keys
                RedKey = LoadTexture("RedKey.bmp", texture => texture.GetPixel(0, 0));
                RedDoor = LoadTexture("RedDoor.bmp");
                GreenKey = LoadTexture("GreenKey.bmp", texture => texture.GetPixel(0, 0));
                GreenDoor = LoadTexture("GreenDoor.bmp");
                BlueKey = LoadTexture("BlueKey.bmp", texture => texture.GetPixel(0, 0));
                BlueDoor = LoadTexture("BlueDoor.bmp");
                OrangeKey = LoadTexture("OrangeKey.bmp", texture => texture.GetPixel(0, 0));
                OrangeDoor = LoadTexture("OrangeDoor.bmp");

                Door = LoadTexture("Door.bmp");
                LockedDoor = LoadTexture("Door.bmp");

                //new level items
                NextLevelVase = LoadTexture("Vase.bmp", texture => texture.GetPixel(0, 0));
                VaseCracks = LoadTexture("VaseCracks.bmp", texture => texture.GetPixel(0, 0));
                BrokenVase = LoadTexture("BrokenVase.bmp", texture => texture.GetPixel(0, 0));

                //Secrets
                Flowey = LoadTexture("Flowey.bmp", texture => texture.GetPixel(0, 0));

                //Guns
                WeaponsTileSet = LoadTexture("Weapons.png", texture => texture.GetPixel(0, 0));
                Knife = LoadTexture("Knife.png", texture => texture.GetPixel(0, 0));
                Pistol = LoadTexture("Pistol.png", texture => texture.GetPixel(0, 0));
                Rifle = LoadTexture("Rifle.png", texture => texture.GetPixel(0, 0));
                MachineGun = LoadTexture("MachineGun.png", texture => texture.GetPixel(0, 0));
                Flamethrower = LoadTexture("Flamethrower.png", texture => texture.GetPixel(0, 0));
                Bazooka = LoadTexture("Bazooka.png", texture => texture.GetPixel(0, 0));
            }

            public Bitmap Bazooka { get; set; }

            public Bitmap Rifle { get; set; }

            public Bitmap Flamethrower { get; set; }

            public Bitmap MachineGun { get; set; }

            public Bitmap Pistol { get; set; }

            public Bitmap Knife { get; set; }

            ~TextureRepository()
            {
                foreach (var texture in textures)
                    texture.Dispose();
            }

            private Bitmap LoadTexture(string textureName)
            {
                var filepath = GetTexturePath(textureName);
                var texture = new Bitmap(GetTexturePath(filepath));
                textures.Add(texture);
                return texture;
            }

            private Bitmap LoadTexture(string textureName, Func<Bitmap, Color> transparentPicker)
            {
                var filepath = GetTexturePath(textureName);
                var texture = new Bitmap(GetTexturePath(filepath));
                texture.MakeTransparent(transparentPicker(texture));
                textures.Add(texture);
                return texture;
            }
        }
    }
}