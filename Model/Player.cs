using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;

namespace PseudoWolfenstein.Model
{
    public class Player : Shape
    {
        public const float MoveSpeed = Settings.PlayerMoveSpeed;
        public const float RotationSpeed = Settings.PlayerRotationSpeed;
        public const float FieldOfView = Settings.PlayerFieldOfView;

        public float Rotation { get; set; }
        public Vector2 Motion { get; private set; }
        public Vector2 MotionDirection => Vector2.UnitX.RotateCounterClockwise(Rotation);

        public int Health { get; private set; } = 100;
        public int Score { get; set; } = 0;
        public Weaponry Weaponry { get; private set; } = new Weaponry();

        public event GameEventHandler Moved;
        public event GameEventHandler Damaged;
        public event GameEventHandler Shot;
        public event GameEventHandler Died;
        public event GameEventHandler Interacting;
        
        private Scene scene;

        public Player() : base(name: default, position: default) 
        {
            Weaponry.Shot += OnWeaponShot;
        }

        public void Initialize(Scene scene)
        {
            this.scene = scene;
            Position = scene.Start;
            Rotation = MathF.PI;
        }

        public void Update()
        {
            SelectWeapon();
            Move();
            Rotate();
            Shoot();
            Interact();
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

        private void Move()
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
                Moved?.Invoke(this, new GameEventArgs(scene, this));
        }

        private void Collide(List<Polygon> obstacles, out int front, out int back)
        {
            const float collisionMagnitude = 0.5f * Settings.WorldWallSize;
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
                Weaponry.BeginShoot();
        }

        private void Interact()
        {
            if (Input.IsKeyDown(Keys.F))
                Interacting?.Invoke(this, new GameEventArgs(scene, this));
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

        internal void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            var newHealth = Health - damage;
            Health = newHealth > 0 ? newHealth : 0;
            Damaged?.Invoke(this, new GameEventArgs(scene, this));
            if (Health == 0) Die();
        }

        private void Die()
        {
            Died?.Invoke(this, new GameEventArgs(scene, this));
        }

        private void OnWeaponShot(object sender, EventArgs e)
        {
            Shot?.Invoke(this, new GameEventArgs(scene, this));
        }
    }
}