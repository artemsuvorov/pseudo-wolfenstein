using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PseudoWolfenstein.Model
{
    // todo: make it derive from shape
    public class Player : Shape
    {
        public const float MoveSpeed = Settings.PlayerMoveSpeed;
        public const float RotationSpeed = Settings.PlayerRotationSpeed;
        public const float FieldOfView = Settings.PlayerFieldOfView;

        // in radians
        public float Rotation = 0.0f;

        public Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public Player(float x, float y) : base(x, y) { }
        public Player(Vector2 position) : base(position) { }

        public void Update()
        {
            Move();
            Rotate();
        }

        private void Move()
        {
            if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
            {
                X += 1 * MathF.Cos(Rotation) * MoveSpeed * TimeF.DeltaTime;
                Y += 1 * MathF.Sin(Rotation) * MoveSpeed * TimeF.DeltaTime;
            }
            else if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
            {
                X += -1 * MathF.Cos(Rotation) * MoveSpeed * TimeF.DeltaTime;
                Y += -1 * MathF.Sin(Rotation) * MoveSpeed * TimeF.DeltaTime;
            }
        }

        private void Rotate()
        {
            if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
            {
                var a = RotationSpeed * TimeF.DeltaTime;
                Rotation += RotationSpeed * TimeF.DeltaTime;
            }
            if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
            {
                var a = RotationSpeed * TimeF.DeltaTime;
                Rotation -= RotationSpeed * TimeF.DeltaTime;
            }
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