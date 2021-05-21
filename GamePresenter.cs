using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    internal class GamePresenter
    {
        private readonly Timer frameTimer;
        private readonly Timer animationTimer;
        private readonly Viewport viewport;
        private readonly IGameForm gameForm;
        //private readonly MinimapForm minimapForm;
        private readonly LevelCollection levels;

        private Player player;
        private Scene scene;

        public GamePresenter(IGameForm gameForm)
        {
            frameTimer = new Timer { Interval = 30 };
            frameTimer.Tick += FrameUpdate;
            frameTimer.Tick += Time.OnGlobalTick;

            this.viewport = gameForm.GetViewport();
            this.levels = new LevelCollection();
            levels.OnCurrentLevelFinished += OnCurrentLevelFinished;
            this.scene = levels.GetNextLevel();

            this.player = new Player();
            this.player.Initialize(scene);
            this.scene.LoadPlayer(player);

            animationTimer = new Timer { Interval = 50 };
            animationTimer.Tick += this.scene.Animate;

            //minimapForm = new MinimapForm(viewport, scene, player);
            this.gameForm = gameForm;
            this.gameForm.Load += Start;
        }

        ~GamePresenter()
        {
            frameTimer.Dispose();
        }

        private void Start(object sender, EventArgs e)
        {
            //minimapForm.Show();
            Initialize(this.scene);
            frameTimer.Start();
            animationTimer.Start();
        }

        private void FrameUpdate(object sender, EventArgs e)
        {
            Time.Update();
            scene.Update();

            gameForm.DebugInfo.Update();
            //minimapForm.Gizmos.Update();

            //minimapForm.Invalidate();
            gameForm.Refresh();
        }

        private void Initialize(Scene scene)
        {
            animationTimer.Tick -= this.scene.Animate;

            this.scene = scene;
            animationTimer.Tick += this.scene.Animate;

            player.Initialize(this.scene);
            scene.LoadPlayer(player);
            gameForm.LoadScene(scene, player);
        }

        private void OnCurrentLevelFinished(object sender, EventArgs e)
        {
            var nextLevel = levels.GetNextLevel();
            Initialize(nextLevel);
        }
    }
}