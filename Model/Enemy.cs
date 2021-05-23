using PseudoWolfenstein.Core;
using PseudoWolfenstein.Utils;
using System;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Enemy : Shotable, IAnimatable
    {
        public int Health { get; private set; } = 3;
        //public float Rotation { get; private set; } = 0.0f;

        private Scene scene;
        private Player player;

        private const bool AiIsEnabled = true;
        private const float VisibilityRange = 12f * Settings.WorldWallSize;
        private const int DamageAmount = 15;
        private const float MoveSpeed = Settings.PlayerMoveSpeed * 2f;

        private readonly EnemyAi ai;
        private Vector2? nextPosition;

        private readonly EnemyAnimations animations;
        private Animation currentAnimation;

        private bool sawPlayer = false;
        private bool seeingPlayer = false;

        private bool isFiring = false;
        private bool isWalking = false;
        private bool isShot = false;
        private bool isDead = false;

        public Enemy(char name, Vector2 position, Image texture, RectangleF srcRect)
            : base(name, position, texture, srcRect)
        {
            animations = new EnemyAnimations();
            ai = new EnemyAi(this);
        }

        public void Initialize(Scene scene, Player player)
        {
            this.scene = scene;
            this.player = player;
        }

        public void Update()
        {
            Look();
            Act();
            //Move();
        }

        public void Animate()
        {
            ChangeAnimation();

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

        private void ChangeAnimation()
        {
            if (isShot && !isDead)
                currentAnimation = animations.ShotAnimation;
            else if (isWalking && !isDead)
                currentAnimation = animations.WalkAnimation;
            else if (isFiring && !isDead)
                currentAnimation = animations.FireAnimation;
            else if (isDead)
                currentAnimation = animations.DeadAnimation;
            else
                currentAnimation = animations.IdleAnimation1;
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

        private void Act()
        {
            if (!AiIsEnabled)
                return;

            if (seeingPlayer && !isShot)
                Fire();
            else if (sawPlayer)
                MoveToPlayer();
            else if (isShot)
                animations.FireAnimation.Reset();
        }

        private void Fire()
        {
            StopWalkingAnimation();
            BeginFireAnimation();
            if (currentAnimation.Frame != animations.FireAnimationFrame) return;

            var dst = (player.Position - Center).Length();
            var dmg = MathF.Min(Settings.WorldWallSize * 30f * (1f / dst), DamageAmount);
            player.ApplyDamage((int)dmg);
        }

        private void MoveToPlayer()
        {
            if (seeingPlayer || isShot || isDead)
                return;

            if (ai.HasPath && ai.PathEnd != player.Position)
                ai.Reset(scene, player.Position);

            if (!nextPosition.HasValue || IsCloseTo(nextPosition.Value))
                this.nextPosition = ai.TryGetNextStep(scene, player.Position, out var nextPosition) ? nextPosition : null;

            if (!nextPosition.HasValue) return;

            isWalking = true;
            var step = (nextPosition.Value - Center).SafeNormalize();
            Center += step;
            LookAt(player.Position);
        }

        private bool IsCloseTo(Vector2 target)
        {
            if (!nextPosition.HasValue) return false;
            var dstVector = Center - target;
            return dstVector.Length() < 1f;
        }

        //private void Move()
        //{
        //    if (AiIsEnabled) return;

        //    if (Input.IsKeyDown(System.Windows.Forms.Keys.J))
        //        Rotation += Player.RotationSpeed * TimeF.DeltaTime;
        //    else if (Input.IsKeyDown(System.Windows.Forms.Keys.L))
        //        Rotation -= Player.RotationSpeed * TimeF.DeltaTime;

        //    float dx = 0.0f, dy = 0.0f;
        //    if (Input.IsKeyDown(System.Windows.Forms.Keys.I))
        //    {
        //        dx = 1.0f * MathF.Cos(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
        //        dy = 1.0f * MathF.Sin(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
        //    }
        //    else if (Input.IsKeyDown(System.Windows.Forms.Keys.K))
        //    {
        //        dx = -1.0f * MathF.Cos(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
        //        dy = -1.0f * MathF.Sin(Rotation) * Player.MoveSpeed * TimeF.DeltaTime;
        //    }
        //    Center += new Vector2(dx, dy);
        //    LookAt(player.Position);
        //}

        private void Look()
        {
            var playerIsVisible = IsPlayerVisible();
            if (playerIsVisible)
            {
                sawPlayer = true;
                //Rotation = Vector2.UnitX.AngleTo(player.Position);
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

        private bool IsPlayerVisible()
        {
            var direction = Center.Lengthen(5, direction: Center - player.Position);
            var wallHit = scene.GetMinDistanceWallCross
                   (player.Position, direction, out _, out var wallHitDst);
            var enemyHit = MathF2D.AreSegmentsCrossing
                (player.Position, direction, Vertices[0], Vertices[1], out var enemyHitLocation);
            var enemyHitDst = (enemyHitLocation - player.Position).Length();

            return ((!wallHit && enemyHit) || (wallHit && wallHitDst > enemyHitDst)) &&
                (enemyHit && enemyHitDst <= VisibilityRange);
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
            player.Score += 50;
        }
    }
}