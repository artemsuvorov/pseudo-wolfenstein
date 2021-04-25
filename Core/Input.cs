using PseudoWolfenstein.Utils;
using PseudoWolfenstein.View;
using System;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PseudoWolfenstein.Core
{
    public static class Input
    {
        [Flags]
        private enum KeyStates
        {
            None = 0,
            Down = 1,
            Toggled = 2
        }

        //private readonly Viewport viewport;

        //public Point MousePosition => Cursor.Position;
        //public Point RelMousePosition => viewport.PointToClient(Cursor.Position);
        //public MouseButtons MouseButtons => Control.MouseButtons;

        public static int VerticalAxis =>
            Convert.ToInt32(IsKeyDown(Keys.W)) - Convert.ToInt32(IsKeyDown(Keys.S));
        public static int HorizontalAxis =>
            Convert.ToInt32(IsKeyDown(Keys.D)) - Convert.ToInt32(IsKeyDown(Keys.A));

        public static Vector2 MotionDirection =>
            new Vector2(x: HorizontalAxis, y: VerticalAxis).SafeNormalize();

        //public Input(Viewport viewport)
        //{
        //    this.viewport = viewport;
        //}

        // source : https://stackoverflow.com/a/9356006
        // author : https://stackoverflow.com/users/264822/parsley72

        public static bool IsKeyDown(Keys key)
        {
            return KeyStates.Down == (GetKeyState(key) & KeyStates.Down);
        }
        public static bool IsKeyToggled(Keys key)
        {
            return KeyStates.Toggled == (GetKeyState(key) & KeyStates.Toggled);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetKeyState(int keyCode);

        private static KeyStates GetKeyState(Keys key)
        {
            KeyStates state = KeyStates.None;

            short retVal = GetKeyState((int)key);

            // If the high-order bit is 1, the key is down otherwise, it is up.
            if ((retVal & 0x8000) == 0x8000)
                state |= KeyStates.Down;

            // If the low-order bit is 1, the key is toggled.
            if ((retVal & 1) == 1)
                state |= KeyStates.Toggled;

            return state;
        }
    }
}