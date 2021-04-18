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

        // in radians
        public float Rotation = 0.0f;

        public Vector2 Motion { get; private set; }
        public Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public Player(float x, float y) : base(x, y) { }
        public Player(Vector2 position) : base(position) { }

        public void Update(Scene scene)
        {
            Move(scene);
            Rotate();
        }

        private void Collide(IEnumerable<Shape> obstacles, out int front, out int back)
        {
            const float collisionMagnitude = 2f;
            (front, back) = (1, 1);

            foreach (Polygon polygon in obstacles)
            {
                for (var index = 1; index < polygon.Vertices.Length + 1; index++)
                {
                    Vector2 vertex1 = polygon.Vertices[index-1], vertex2 = polygon.Vertices[index % polygon.Vertices.Length];
                    Vector2 dirFront = Position + MotionDirection * collisionMagnitude, dirBack = Position - MotionDirection * collisionMagnitude;
                    var isCrossingFront = MathF2D.AreSegmentsCrossing(Position, dirFront, vertex1, vertex2, out _);
                    var isCrossingBack = MathF2D.AreSegmentsCrossing(Position, dirBack, vertex1, vertex2, out _);
                    if (isCrossingFront) front = 0;
                    if (isCrossingBack) back = 0;
                }
            }
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