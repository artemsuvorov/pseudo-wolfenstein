﻿using PseudoWolfenstein.Core;
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

        public float Rotation { get; set; } = MathF.PI;
        public Vector2 Motion { get; private set; }
        public Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public int Health { get; private set; } = 100;
        public int Score { get; set; } = 0;
        public Weaponry Weaponry { get; private set; } = new Weaponry();

        public event EventHandler<Player> Moved;
        public event EventHandler<Player> Shot;
        public event EventHandler<Player> DoorOpening;

        public Player(char name, Vector2 position) : base(name, position) { }

        public void Update(Scene scene)
        {
            SelectWeapon();
            Move(scene);
            Rotate();
            Shoot();
            OpenDoor();
        }

        public void Animate()
        {
            Weaponry.Animate();
        }

        public void Heal(int amount)
        {
            Health = (int)MathF.Min(MathF.Max(0, Health + amount), 100);
        }

        private void SelectWeapon()
        {
            if (Input.IsKeyDown(Keys.D1)) Weaponry.SelectWeapon(WeaponType.Knife);
            if (Input.IsKeyDown(Keys.D2)) Weaponry.SelectWeapon(WeaponType.Pistol);
            if (Input.IsKeyDown(Keys.D3)) Weaponry.SelectWeapon(WeaponType.MachineGun);
            if (Input.IsKeyDown(Keys.D4)) Weaponry.SelectWeapon(WeaponType.Chaingun);
            if (Input.IsKeyDown(Keys.D5)) Weaponry.SelectWeapon(WeaponType.FlameThrower);
            if (Input.IsKeyDown(Keys.D6)) Weaponry.SelectWeapon(WeaponType.RocketLauncher);
        }

        private void Move(Scene scene)
        {
            Collide(scene.Obstacles, out int front, out int back);
            var dx = 0f;
            var dy = 0f;

            if (Input.IsKeyDown(Keys.W) || Input.IsKeyDown(Keys.Up))
            {
                dx = 1.0f * MathF.Cos(Rotation) * MoveSpeed * TimeF.DeltaTime * front;
                dy = 1.0f * MathF.Sin(Rotation) * MoveSpeed * TimeF.DeltaTime * front;
            }
            else if (Input.IsKeyDown(Keys.S) || Input.IsKeyDown(Keys.Down))
            {
                dx = -1.0f * MathF.Cos(Rotation) * MoveSpeed * TimeF.DeltaTime * back;
                dy = -1.0f * MathF.Sin(Rotation) * MoveSpeed * TimeF.DeltaTime * back;
            }

            Position += new Vector2(dx, dy);

            if (dx.IsNotEqual(0f) || dy.IsNotEqual(0f))
                Moved?.Invoke(this, this);
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

        private void Shoot()
        {
            if (Input.IsKeyDown(Keys.Space))
            {
                Weaponry.Shoot();
                Shot?.Invoke(this, this);
            }
        }

        private void OpenDoor()
        {
            if (Input.IsKeyDown(Keys.F))
                DoorOpening?.Invoke(this, this);
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