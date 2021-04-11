using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    // todo: make it derive from shape
    public class Player : Shape
    {
        private readonly Input input;

        public const float MoveSpeed = 6.5f;
        public const float RotationSpeed = 0.05f;
        public const float FieldOfView = MathF.PI / 3.0f;

        // in radians
        public float Rotation = 0.0f;

        public Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public Player(Input input)
        {
            this.input = input;
        }

        public void Update()
        {
            MoveRel();
            Rotate();
        }

        // todo: remove this from player class
        public override void Draw(Graphics graphics)
        {
            using var objectFillBrush = new SolidBrush(Settings.GameObjectFillColor);
            using var objectStrokePen = new Pen(Settings.GameObjectStrokeColor, Settings.ObjectStrokeWidth);

            float x = X - Settings.PlayerRadius/2f, y = Y - Settings.PlayerRadius/2f;
            graphics.FillEllipse(objectFillBrush, x, y, Settings.PlayerRadius, Settings.PlayerRadius);
            graphics.DrawEllipse(objectStrokePen, x, y, Settings.PlayerRadius, Settings.PlayerRadius);
        }

        private void MoveAbs()
        {
            if (input.IsKeyDown(Keys.Space))
                Position = Vector2.Zero;

            var dir = input.MotionDirection;
            if (dir == Vector2.Zero) return;

            X += dir.X * MoveSpeed;
            Y -= dir.Y * MoveSpeed;
        }

        private void MoveRel()
        {
            X += input.VerticalAxis * MathF.Cos(Rotation) * MoveSpeed;
            Y += input.VerticalAxis * MathF.Sin(Rotation) * MoveSpeed;
            X += +input.HorizontalAxis * MathF.Sin(Rotation) * MoveSpeed;
            Y += -input.HorizontalAxis * MathF.Cos(Rotation) * MoveSpeed;
        }

        private void Rotate()
        {
            if (input.IsKeyDown(Keys.Q))
                Rotation += RotationSpeed;
            if (input.IsKeyDown(Keys.E))
                Rotation -= RotationSpeed;
        }
    }
}