using System;
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
            public Bitmap StoneWall { get; private set; }
            public Bitmap BlueWall { get; private set; }

            public TextureRepository()
            {
                StoneWall = new Bitmap(GetTexturePath("WALL0.bmp"));
                BlueWall = new Bitmap(GetTexturePath("WALL14.bmp"));
            }

            ~TextureRepository()
            {
                StoneWall.Dispose();
                BlueWall.Dispose();
            }
        }
    }
}