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
            public Bitmap GreyColmun { get; private set; }

            private readonly IList<Bitmap> textures = new List<Bitmap>(TextureRepoCapactity);

            public TextureRepository()
            {
                StoneWall = LoadTexture("WALL0.bmp");
                BlueWall = LoadTexture("WALL14.bmp");
                GreyColmun = LoadTexture("GreyColumn.bmp");
            }

            private Bitmap LoadTexture(string textureName)
            {
                var filepath = GetTexturePath(textureName);
                var texture = new Bitmap(GetTexturePath(filepath));
                textures.Add(texture);
                return texture;
            }

            ~TextureRepository()
            {
                StoneWall.Dispose();
                BlueWall.Dispose();
                GreyColmun.Dispose();
            }
        }
    }
}