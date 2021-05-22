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
        private readonly IGameForm gameForm;
        //private readonly MinimapForm minimapForm;

        private LevelCollection levels;
        private Player player;
        private Scene scene;

        private bool isGameEnded = false;

        public GamePresenter(IGameForm gameForm)
        {
            frameTimer = new Timer { Interval = 30 };
            frameTimer.Tick += FrameUpdate;
            frameTimer.Tick += Time.OnGlobalTick;
            
            animationTimer = new Timer { Interval = 50 };

            //minimapForm = new MinimapForm(viewport, scene, player);
            this.gameForm = gameForm;
            this.gameForm.KeyUp += OnGameFormKeyUp;
            this.gameForm.Load += Start;

            StartNewGame();
        }

        ~GamePresenter()
        {
            frameTimer.Dispose();
        }

        private void Start(object sender, EventArgs e)
        {
            //minimapForm.Show();
            frameTimer.Start();
            animationTimer.Start();
        }

        private void FrameUpdate(object sender, EventArgs e)
        {
            Time.Update();
            scene.Update();

            gameForm.DebugInfo.Update();
            gameForm.Animate();
            //minimapForm.Gizmos.Update();

            //minimapForm.Invalidate();
            gameForm.InvalidateAll();
        }

        private void StartNewGame()
        {
            if (this.levels is object)
            {
                levels.CurrentLevelFinished -= OnCurrentLevelFinished;
                levels.AllLevelsFinished -= OnAllLevelsFinished;
            }
            if (this.player is object)
            {
                this.player.Died -= OnPlayerDied;
            }

            this.levels = new LevelCollection();
            levels.CurrentLevelFinished += OnCurrentLevelFinished;
            levels.AllLevelsFinished += OnAllLevelsFinished;
            this.scene = levels.GetNextLevel();

            this.player = new Player();
            this.player.Died += OnPlayerDied;
            this.player.Initialize(scene);
            this.scene.LoadPlayer(player);

            Initialize(this.scene);
        }

        private void Initialize(Scene scene)
        {
            animationTimer.Tick -= this.scene.Animate;

            this.scene = scene;
            if (scene is null) return;
            
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

        private void OnAllLevelsFinished(object sender, EventArgs e)
        {
            isGameEnded = true;
            gameForm.ShowWinScreen();
        }

        private void OnPlayerDied(object sender, GameEventArgs e)
        {
            isGameEnded = true;
            gameForm.ShowGameOverScreen();
        }

        private void OnGameFormKeyUp(object sender, KeyEventArgs e)
        {
            if (!isGameEnded) return;

            isGameEnded = false;
            gameForm.HideGameEndScreen();
            StartNewGame();
        }
    }
}