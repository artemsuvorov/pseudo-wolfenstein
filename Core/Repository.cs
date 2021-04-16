using System;
using System.IO;

namespace PseudoWolfenstein.Core
{
    internal static class Repository
    {
        public static string ProjectDirectoryPath =>
            Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\"));

        public static string GetTexturePath(string textureName)
        {
            return Path.Combine(ProjectDirectoryPath, "Textures", textureName);
        }
    }
}