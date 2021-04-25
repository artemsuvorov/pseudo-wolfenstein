using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Collections.Generic;
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

        public float Rotation = 0.0f;

        public Vector2 Motion { get; private set; }
        public Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public int SelectedWeapon = 0;

        public Player(float x, float y) : base(x, y) { }
        public Player(Vector2 position) : base(position) { }

        public void Update(Scene scene)
        {
            SelectWeapon();
            Move(scene);
            Rotate();
        }

        private void SelectWeapon()
        {
            if (Input.IsKeyDown(Keys.D1))
                SelectedWeapon = 0;
            if (Input.IsKeyDown(Keys.D2))
                SelectedWeapon = 1;
            if (Input.IsKeyDown(Keys.D3))
                SelectedWeapon = 2;
            if (Input.IsKeyDown(Keys.D4))
                SelectedWeapon = 3;
            if (Input.IsKeyDown(Keys.D5))
                SelectedWeapon = 4;
            if (Input.IsKeyDown(Keys.D6))
                SelectedWeapon = 5;
        }

        private void Move(Scene scene)
        {
            Collide(scene.Obstacles, out int front, out int back);

            if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
            {
                X += 1.0f * MathF.Cos(Rotation) * MoveSpeed * TimeF.DeltaTime * front;
                Y += 1.0f * MathF.Sin(Rotation) * MoveSpeed * TimeF.DeltaTime * front;
            }
            else if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
            {
                X += -1.0f * MathF.Cos(Rotation) * MoveSpeed * TimeF.DeltaTime * back;
                Y += -1.0f * MathF.Sin(Rotation) * MoveSpeed * TimeF.DeltaTime * back;
            }
        }

        private void Collide(IEnumerable<Shape> obstacles, out int front, out int back)
        {
            const float collisionMagnitude = 0.5f * Settings.WorldWallSize;
            (front, back) = (1, 1);

            foreach (Polygon polygon in obstacles)
            {
                for (var index = 1; index < polygon.Vertices.Length + 1; index++)
                {
                    Vector2 vertex1 = polygon.Vertices[index - 1], vertex2 = polygon.Vertices[index % polygon.Vertices.Length];
                    Vector2 dirFront = Position + MotionDirection * collisionMagnitude, dirBack = Position - MotionDirection * collisionMagnitude;
                    var isCrossingFront = MathF2D.AreSegmentsCrossing(Position, dirFront, vertex1, vertex2, out _);
                    var isCrossingBack = MathF2D.AreSegmentsCrossing(Position, dirBack, vertex1, vertex2, out _);
                    if (isCrossingFront) front = 0;
                    if (isCrossingBack) back = 0;
                }
            }
        }

        private void Rotate()
        {
            if (Input.IsKeyDown(Keys.A) || Input.IsKeyDown(Keys.Left))
                Rotation += RotationSpeed * TimeF.DeltaTime;
            if (Input.IsKeyDown(Keys.D) || Input.IsKeyDown(Keys.Right))
                Rotation -= RotationSpeed * TimeF.DeltaTime;
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