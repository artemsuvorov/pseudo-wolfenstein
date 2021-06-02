using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace PseudoWolfenstein.Core
{
    internal static class Repository
    {
        private static TextureRepository textures;
        private static MusicRepository music;
        private static ImageRepository images;

        public static string ProjectDirectoryPath =>
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

        public static TextureRepository Textures => textures ??= new TextureRepository();
        public static ImageRepository Images => images ??= new ImageRepository();
        public static MusicRepository Music => music ??= new MusicRepository();

        private static string GetTexturePath(string textureName)
        {
            return GetFilePath(directory: "Textures", textureName);
        }

        private static string GetMusicPath(string fileName)
        {
            return GetFilePath(directory: "Music", fileName);
        }

        private static string GetFilePath(string directory, string fileName)
        {
            return Path.Combine(ProjectDirectoryPath, directory, fileName);
        }

        internal class MusicRepository
        {
            public string MenuBackgroundMusicPath { get; private set; }

            public MusicRepository()
            {
                MenuBackgroundMusicPath = GetMusicPath("menu-background.wav");
            }
        }

        internal class ImageRepository
        {
            public Bitmap MenuBackground { get; private set; }
            public Icon Icon { get; private set; }

            public ImageRepository()
            {
                MenuBackground = new Bitmap(GetFilePath("Assets", "background.png"));
                Icon = new Icon(GetFilePath("Assets", "icon.ico"));
            }
        }

        internal class TextureRepository
        {
            internal class EnemyTextureCollection
            {
                private readonly List<Bitmap> frames = new(32);

                public Bitmap this[int index]
                {
                    get => frames[index];
                }

                public Bitmap IdleFritz1 { get; private set; }
                public Bitmap ShotFritz { get; private set; }
                public Bitmap FritzDeathFrame1 { get; private set; }
                public Bitmap FritzDeathFrame2 { get; private set; }
                public Bitmap FritzDeathFrame3 { get; private set; }
                public Bitmap FritzDeathFrame4 { get; private set; }
                public Bitmap FritzFireFrame1 { get; private set; }
                public Bitmap FritzFireFrame2 { get; private set; }
                public Bitmap FritzFireFrame3 { get; private set; }
                public Bitmap DeadFritz { get; private set; }
                public Bitmap IdleFritz2 { get; private set; }
                public Bitmap IdleFritz3 { get; private set; }
                public Bitmap IdleFritz4 { get; private set; }
                public Bitmap IdleFritz5 { get; private set; }
                public Bitmap IdleFritz6 { get; private set; }
                public Bitmap IdleFritz7 { get; private set; }
                public Bitmap IdleFritz8 { get; private set; }
                public Bitmap FritzWalk1 { get; private set; }
                public Bitmap FritzWalk2 { get; private set; }
                public Bitmap FritzWalk3 { get; private set; }
                public Bitmap FritzWalk4 { get; private set; }

                private readonly Func<string, Func<Bitmap, Color>, Bitmap> TextureLoader;

                public EnemyTextureCollection(Func<string, Func<Bitmap, Color>, Bitmap> textureLoader)
                {
                    TextureLoader = textureLoader;
                    IdleFritz1 = LoadEnemyTextureFrame("IdleFritz1.png");
                    ShotFritz = LoadEnemyTextureFrame("ShotFritz.png");
                    FritzDeathFrame1 = LoadEnemyTextureFrame("FritzDeathFrame1.png");
                    FritzDeathFrame2 = LoadEnemyTextureFrame("FritzDeathFrame2.png");
                    FritzDeathFrame3 = LoadEnemyTextureFrame("FritzDeathFrame3.png");
                    FritzDeathFrame4 = LoadEnemyTextureFrame("FritzDeathFrame4.png");
                    DeadFritz = LoadEnemyTextureFrame("DeadFritz.png");
                    FritzFireFrame1 = LoadEnemyTextureFrame("FritzFireFrame1.png");
                    FritzFireFrame2 = LoadEnemyTextureFrame("FritzFireFrame2.png");
                    FritzFireFrame3 = LoadEnemyTextureFrame("FritzFireFrame3.png");
                    IdleFritz2 = LoadEnemyTextureFrame("IdleFritz2.png");
                    IdleFritz3 = LoadEnemyTextureFrame("IdleFritz3.png");
                    IdleFritz4 = LoadEnemyTextureFrame("IdleFritz4.png");
                    IdleFritz5 = LoadEnemyTextureFrame("IdleFritz5.png");
                    IdleFritz6 = LoadEnemyTextureFrame("IdleFritz6.png");
                    IdleFritz7 = LoadEnemyTextureFrame("IdleFritz7.png");
                    IdleFritz8 = LoadEnemyTextureFrame("IdleFritz8.png");
                    FritzWalk1 = LoadEnemyTextureFrame("FritzWalk1.png");
                    FritzWalk2 = LoadEnemyTextureFrame("FritzWalk2.png");
                    FritzWalk3 = LoadEnemyTextureFrame("FritzWalk3.png");
                    FritzWalk4 = LoadEnemyTextureFrame("FritzWalk4.png");
                }

                private Bitmap LoadEnemyTextureFrame(string textureName)
                {
                    var texture = TextureLoader(textureName, texture => texture.GetPixel(0, 0));
                    frames.Add(texture);
                    return texture;
                }
            }

            internal class VaseTextureCollection
            {
                private readonly List<Bitmap> frames = new(4);

                public Bitmap this[int index]
                {
                    get => frames[index];
                }

                public Bitmap Vase { get; private set; }
                public Bitmap CrackedVase { get; private set; }
                public Bitmap BrokenVase { get; private set; }

                private readonly Func<string, Func<Bitmap, Color>, Bitmap> TextureLoader;

                public VaseTextureCollection(Func<string, Func<Bitmap, Color>, Bitmap> textureLoader)
                {
                    TextureLoader = textureLoader;
                    Vase = LoadVaseTextureFrame("Vase.bmp");
                    CrackedVase = LoadVaseTextureFrame("VaseCracked.bmp");
                    BrokenVase = LoadVaseTextureFrame("VaseBroken.bmp");
                }

                private Bitmap LoadVaseTextureFrame(string textureName)
                {
                    var texture = TextureLoader(textureName, texture => texture.GetPixel(0, 0));
                    frames.Add(texture);
                    return texture;
                }
            }

            private const int TextureRepoCapactity = 256;

            public EnemyTextureCollection EnemyFrames { get; private set; }
            public VaseTextureCollection VaseFrames { get; private set; }

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
            public Bitmap HealPack { get; private set; }
            public Bitmap SmallTable { get; private set; }
            public Bitmap ScoreItemCross { get; private set; }
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
            public Bitmap Left { get; set; }
            public Bitmap Right { get; set; }
            public Bitmap Tree { get; set; }
            public Bitmap SendWall { get; set; }
            public Bitmap GoldWall { get; set; }
            public Bitmap Flowey { get; set; }
            public Bitmap Knife { get; set; }
            public Bitmap Pistol { get; set; }
            public Bitmap MachineGun { get; set; }
            public Bitmap ChainGun { get; set; }
            public Bitmap FlameThrower { get; set; }
            public Bitmap RocketLauncher { get; set; }
            public Bitmap HUD { get; set; }

            private readonly HashSet<Bitmap> textures = new(TextureRepoCapactity);

            public TextureRepository()
            {
                EnemyFrames = new EnemyTextureCollection(LoadTexture);
                VaseFrames = new VaseTextureCollection(LoadTexture);

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
                HealPack = LoadTexture("Heal.bmp", texture => texture.GetPixel(0, 0));
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

                //Secrets
                Flowey = LoadTexture("Flowey.bmp", texture => texture.GetPixel(0, 0));

                //Guns
                WeaponsTileSet = LoadTexture("Weapons.png", texture => texture.GetPixel(0, 0));
                Knife = LoadTexture("Knife.png", texture => texture.GetPixel(0, 0));
                Pistol = LoadTexture("Pistol.png", texture => texture.GetPixel(0, 0));
                MachineGun = LoadTexture("MachineGun.png", texture => texture.GetPixel(0, 0));
                ChainGun = LoadTexture("ChainGun.png", texture => texture.GetPixel(0, 0));
                FlameThrower = LoadTexture("FlameThrower.png", texture => texture.GetPixel(0, 0));
                RocketLauncher = LoadTexture("RocketLauncher.png", texture => texture.GetPixel(0, 0));

                HUD = LoadTexture("hud.png");
            }

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