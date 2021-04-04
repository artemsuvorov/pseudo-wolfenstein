using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    // todo: make it derive from shape and apply singleton pattern
    public static class Player
    {
        public const float MoveSpeed = 6.5f;
        public const float RotationSpeed = 0.05f;
        public const float FieldOfView = MathF.PI / 3.0f;

        private static Vector2 position = new Vector2(0f, 0f);

        // in radians
        public static float Rotation = 0.0f;

        public static Vector2 Position 
        {
            get => position; 
            set => position = value;
        }

        public static float X => position.X;
        public static float Y => position.Y;

        public static Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public static void Update()
        {
            MoveRel();
            Rotate();
        }

        public static void Draw(Graphics graphics)
        {
            using var objectFillBrush = new SolidBrush(Settings.GameObjectFillColor);
            using var objectStrokePen = new Pen(Settings.GameObjectStrokeColor, Settings.ObjectStrokeWidth);

            float x = X - Settings.PlayerRadius/2f, y = Y - Settings.PlayerRadius/2f;
            graphics.FillEllipse(objectFillBrush, x, y, Settings.PlayerRadius, Settings.PlayerRadius);
            graphics.DrawEllipse(objectStrokePen, x, y, Settings.PlayerRadius, Settings.PlayerRadius);
        }

        private static void MoveAbs()
        {
            if (Input.IsKeyDown(Keys.Space))
                position = Vector2.Zero;

            var dir = Input.MotionDirection;
            if (dir == Vector2.Zero) return;

            position.X += dir.X * MoveSpeed;
            position.Y -= dir.Y * MoveSpeed;
        }

        private static void MoveRel()
        {
            position.X += Input.VerticalAxis * MathF.Cos(Rotation) * MoveSpeed;
            position.Y += Input.VerticalAxis * MathF.Sin(Rotation) * MoveSpeed;

            position.X += +Input.HorizontalAxis * MathF.Sin(Rotation) * MoveSpeed;
            position.Y += -Input.HorizontalAxis * MathF.Cos(Rotation) * MoveSpeed;
        }

        private static void Rotate()
        {
            if (Input.IsKeyDown(Keys.Q))
                Rotation += RotationSpeed;
            if (Input.IsKeyDown(Keys.E))
                Rotation -= RotationSpeed;
        }

        private static void FollowCursor()
        {
            var relMousePos = Input.RelMousePosition;
            var cursorPos = new Vector2(relMousePos.X, relMousePos.Y);
            LookAt(cursorPos);
        }

        private static void LookAt(Vector2 target)
        {
            var centerPoint = Camera.ScreenCenterPosition;
            var center = new Vector2(centerPoint.X, centerPoint.Y);
            Rotation = (Vector2.UnitX + center + Player.Position).AngleTo(target);
        }
    }
}