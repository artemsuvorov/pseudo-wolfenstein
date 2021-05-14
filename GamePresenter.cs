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
        private readonly MinimapForm minimapForm;
        private readonly Player player;
        private readonly Scene scene;

        public GamePresenter(Scene scene, IGameForm gameForm)
        {
            frameTimer = new Timer { Interval = 16 };
            frameTimer.Tick += FrameUpdate;
            frameTimer.Tick += Time.OnGlobalTick;

            this.viewport = gameForm.GetViewport();
            this.scene = scene;
            this.player = this.scene.Player;
            this.player.Initialize(scene);

            animationTimer = new Timer { Interval = 50 };
            animationTimer.Tick += this.scene.Animate;

            minimapForm = new MinimapForm(viewport, scene);
            this.gameForm = gameForm;
            this.gameForm.Load += Start;
        }

        ~GamePresenter()
        {
            frameTimer.Dispose();
        }

        private void Start(object sender, EventArgs e)
        {
            minimapForm.Show();
            frameTimer.Start();
            animationTimer.Start();
        }

        private void FrameUpdate(object sender, EventArgs e)
        {
            Time.Update();
            scene.Update();

            gameForm.DebugInfo.Update();
            //minimapForm.Gizmos.Update();

            minimapForm.Invalidate();
            gameForm.Refresh();
        }
    }
}