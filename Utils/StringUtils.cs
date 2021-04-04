using System;

namespace PseudoWolfenstein.Utils
{
    public static class StringUtils
    {
        // source : https://stackoverflow.com/a/40928366
        // author : https://stackoverflow.com/users/3485361/lokimidgard
        public static int CountLines(string str)
        {
            if (str is null)
                throw new ArgumentNullException("String is null.");

            if (string.IsNullOrWhiteSpace(str))
                return 0;

            int index = -1;
            int count = 0;
            while (-1 != (index = str.IndexOf(Environment.NewLine, index + 1)))
                count++;

            return count + 1;
        }
    }
}