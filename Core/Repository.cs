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
            private const int TextureRepoCapactity = 256;

            public Bitmap StoneWall { get; private set; }
            public Bitmap BlueWall { get; private set; }

            public Bitmap Wood_Dark { get; private set; }
            public Bitmap GreyColmun { get; private set; }

            public Bitmap WeaponsTileSet { get; private set; }

            public Bitmap FritzTileSet { get; private set; }

            private readonly ISet<Bitmap> textures = new HashSet<Bitmap>(TextureRepoCapactity);

            public TextureRepository()
            {
                StoneWall = LoadTexture("WALL0.bmp");
                BlueWall = LoadTexture("WALL14.bmp");
                Wood_Dark = LoadTexture("WALL23.bmp");
                GreyColmun = LoadTexture("GreyColumn.bmp", texture => texture.GetPixel(0,0));
                WeaponsTileSet = LoadTexture("weapons.png", texture => texture.GetPixel(0, 0));
                FritzTileSet = LoadTexture("fritz.png", texture => texture.GetPixel(0, 0));
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

            ~TextureRepository()
            {
                foreach (var texture in textures)
                    texture.Dispose();
            }
        }
    }
}