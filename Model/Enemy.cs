using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    internal static class EnemyAnimations
    {
        public readonly static Animation IdleAnimation1 = new(new[] { 0 }) { Looped = true };
        public readonly static Animation IdleAnimation2 = new(new[] { 10 }) { Looped = true };
        public readonly static Animation IdleAnimation3 = new(new[] { 11 }) { Looped = true };
        public readonly static Animation IdleAnimation4 = new(new[] { 12 }) { Looped = true };
        public readonly static Animation IdleAnimation5 = new(new[] { 13 }) { Looped = true };
        public readonly static Animation IdleAnimation6 = new(new[] { 14 }) { Looped = true };
        public readonly static Animation IdleAnimation7 = new(new[] { 15 }) { Looped = true };
        public readonly static Animation IdleAnimation8 = new(new[] { 16 }) { Looped = true };

        public readonly static Animation ShotAnimation = new(new[] { 1, 1, 1, 1 }) { Looped = true };
        public readonly static Animation FireAnimation = new(new[] { 7, 7, 8, 8, 9, 9, 9, 8, 7 }) { Looped = true };
        public readonly static Animation WalkAnimation = new(new[] { 17, 17, 18, 18, 19, 19, 20, 20 }) { Looped = true };
        public readonly static Animation DeadAnimation = new(new[] { 2, 2, 3, 3, 4, 4, 5, 5, 6 });

        public static Animation GetIdleAnimation(float angle)
        {
            angle = MathF.Abs(angle) % (2f * MathF.PI);
            if (angle >= -1f * MathF.PI / 8f && angle < 1f * MathF.PI / 8f) return IdleAnimation1;
            else if (angle >= 1f * MathF.PI / 8f && angle < 3f * MathF.PI / 8f) return IdleAnimation2;
            else if (angle >= 3f * MathF.PI / 8f && angle < 5f * MathF.PI / 8f) return IdleAnimation3;
            else if (angle >= 5f * MathF.PI / 8f && angle < 7f * MathF.PI / 8f) return IdleAnimation4;
            else if (angle >= 7f * MathF.PI / 8f && angle < 9f * MathF.PI / 8f) return IdleAnimation5;
            else if (angle >= 9f * MathF.PI / 8f && angle < 11f * MathF.PI / 8f) return IdleAnimation6;
            else if (angle >= 11f * MathF.PI / 8f && angle < 13f * MathF.PI / 8f) return IdleAnimation7;
            else return IdleAnimation8;
        }
    }

    public class Enemy : Shotable
    {
        public int Health { get; private set; } = 3;
        public float Rotation { get; private set; } = 0.0f;

        private const bool AiIsEnabled = true;
        private const float VisibilityRange = 7f * Settings.WorldWallSize;
        private const int DamageAmount = 20;
        private const float MoveSpeed = Settings.PlayerMoveSpeed / 3f;

        private Animation currentAnimation;
        private bool sawPlayer = false;
        private bool seeingPlayer = false;

        private bool isFiring = false;
        private bool isWalking = false;
        private bool isShot = false;
        private bool isDead = false;

        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        { }

        public void Update(Scene scene, Player player)
        {
            Look(scene, player);
            Act(player);
            Move(player);
        }

        public void Animate(object sender, GameEventArgs e)
        {
            ChangeAnimation(e.Player);

            if (currentAnimation.IsContinuing)
            {
                var frame = currentAnimation.NextFrame();
                Texture = Repository.Textures.EnemyFrames[frame];
            }
            if (!currentAnimation.IsContinuing)
            {
                if (currentAnimation.Looped)
                    currentAnimation.Reset();
                isShot = false;
                isFiring = false;
            }
        }

        private void ChangeAnimation(Player player)
        {
            if (isShot && !isDead)
                currentAnimation = EnemyAnimations.ShotAnimation;
            else if (isWalking && !isDead)
                currentAnimation = EnemyAnimations.WalkAnimation;
            else if (isFiring && !isDead)
                currentAnimation = EnemyAnimations.FireAnimation;
            else if (isDead)
                currentAnimation = EnemyAnimations.DeadAnimation;
            else
                currentAnimation = EnemyAnimations.IdleAnimation1;
            //{
            //    // todo: it doesn't work properly !!!

            //    var v = new Vector2(Center.X - player.X, Center.Y - player.Y);
            //    var e = Vector2.UnitX.RotateCounterClockwise(Rotation);
            //    var q = new Vector2(e.X, e.Y);
            //    //var a = v.AngleTo(q);
            //    //var c = MathF.Acos(Vector2.Dot(v, q) / (v.Length() * q.Length()));
            //    //var d = q.AngleTo(v);
            //    var angle = MathF.Acos(Vector2.Dot(v, q) / (v.Length() * q.Length())) - MathF.PI;
            //    //angle = -q.AngleTo(v);

            //    //var m = Center - player.Position;
            //    //var direction = Vector2.UnitX.RotateCounterClockwise(rotation);
            //    //angle = m.AngleTo(direction);
            //    currentAnimation = EnemyAnimations.GetIdleAnimation(angle);
            //    //currentAnimation = EnemyAnimations.GetIdleAnimation(angle);
            //}
        }

        private void Act(Player player)
        {
            if (!AiIsEnabled) 
                return;

            if (seeingPlayer && !isShot)
                Fire(player);
            else if (sawPlayer)
                MoveToPlayer(player);
            else if (isShot)
                EnemyAnimations.FireAnimation.Reset();
        }

        private void Fire(Player player)
        {
            StopWalkingAnimation();
            BeginFireAnimation();
        }

        private void MoveToPlayer(Player player)
        {
            if (seeingPlayer || isShot || isDead) return;
            isWalking = true;

            // todo: make enemy not going through walls
            // https://stackoverflow.com/questions/5303538/algorithm-to-find-the-shortest-path-with-obstacles
            var target = player.Position;
            var step = (target - Center).SafeNormalize() * MoveSpeed * TimeF.DeltaTime;
            Center += step;
            LookAt(player.Position);
        }

        private void Move(Player player)
        {
            if (AiIsEnabled) return;

            if (Input.IsKeyDown(System.Windows.Forms.Keys.J))
                Rotation += Player.RotationSpeed * TimeF.DeltaTime;
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.L))
                Rotation -= Player.RotationSpeed * TimeF.DeltaTime;

            float dx = 0.0f, dy = 0.0f;
            if (Input.IsKeyDown(System.Windows.Forms.Keys.I))
            {
                dx = 1.0f * MathF.Cos(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
                dy = 1.0f * MathF.Sin(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
            }
            else if (Input.IsKeyDown(System.Windows.Forms.Keys.K))
            {
                dx = -1.0f * MathF.Cos(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
                dy = -1.0f * MathF.Sin(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
            }
            Center += new Vector2(dx, dy);
            LookAt(player.Position);
        }

        private void Look(Scene scene, Player player)
        {
            var playerIsVisible = IsPlayerVisible(scene, player);
            if (playerIsVisible)
            {
                sawPlayer = true;
                Rotation = Vector2.UnitX.AngleTo(player.Position);
            }
            seeingPlayer = playerIsVisible;
        }

        protected override void OnShot(object sender, GameEventArgs e)
        {
            BeginShotAnimation();
            ApplyDamage(e.Player.Weaponry.SelectedWeapon.DamageAmount);
        }

        private void StopWalkingAnimation()
        {
            isWalking = false;
        }

        private void BeginFireAnimation()
        {
            isFiring = true;
        }

        private void BeginShotAnimation()
        {
            isShot = true;
        }

        private bool IsPlayerVisible(Scene scene, Player player)
        {
            var direction = Center.Lengthen(5, direction: Center - player.Position);
            var wallHit = scene.GetMinDistanceWallCross
                   (player.Position, direction, out _, out var wallHitDst);
            var enemyHit = MathF2D.AreSegmentsCrossing
                (player.Position, direction, Vertices[0], Vertices[1], out var enemyHitLocation);
            var enemyHitDst = (enemyHitLocation - player.Position).Length();

            // todo: add visibility range constraint
            if (((!wallHit && enemyHit) || (wallHit && wallHitDst > enemyHitDst)) && 
                (enemyHit && enemyHitDst <= VisibilityRange))
                return true;
            else
                return false;
        }

        private void ApplyDamage(int damage)
        {
            if (damage <= 0) return;
            var newHealth = Health - damage;
            Health = newHealth > 0 ? newHealth : 0;
            if (Health == 0) Die();
        }

        private void Die()
        {
            isDead = true;
        }
    }
}