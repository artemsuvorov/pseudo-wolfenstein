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

        public const float MoveSpeed = 0.14f;
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
            Move();
            Rotate();
        }

        private void Move()
        {
            if (input.IsKeyDown(Keys.W) || input.IsKeyDown(Keys.Up))
            {
                X += 1 * MathF.Cos(Rotation) * MoveSpeed;
                Y += 1 * MathF.Sin(Rotation) * MoveSpeed;
            }
            else if (input.IsKeyDown(Keys.S) || input.IsKeyDown(Keys.Down))
            {
                X += -1 * MathF.Cos(Rotation) * MoveSpeed;
                Y += -1 * MathF.Sin(Rotation) * MoveSpeed;
            }
        }

        private void Rotate()
        {
            if (input.IsKeyDown(Keys.A) || input.IsKeyDown(Keys.Left))
                Rotation += RotationSpeed;
            if (input.IsKeyDown(Keys.D) || input.IsKeyDown(Keys.Right))
                Rotation -= RotationSpeed;
        }

        // todo: remove this from player class
        public override void Draw(Graphics graphics)
        {
            using var objectFillBrush = new SolidBrush(Settings.GameObjectFillColor);
            using var objectStrokePen = new Pen(Settings.GameObjectStrokeColor, Settings.ObjectStrokeWidth);

            float x = X - Settings.PlayerRadius / 2f, y = Y - Settings.PlayerRadius / 2f;
            graphics.FillEllipse(objectFillBrush, x, y, Settings.PlayerRadius, Settings.PlayerRadius);
            graphics.DrawEllipse(objectStrokePen, x, y, Settings.PlayerRadius, Settings.PlayerRadius);
        }
    }
}