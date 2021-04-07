using PseudoWolfenstein.Core;
using PseudoWolfenstein.Core.Raycasting;
using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    internal class GamePresenter
    {
        private readonly Timer timer;
        private readonly Viewport viewport;
        private readonly Input input;
        private readonly IGameForm gameForm;
        private readonly MinimapForm minimapForm;
        private readonly Player player;
        private readonly GameScene scene;
        private readonly Raycast raycast;

        public GamePresenter(Viewport viewport, Input input, GameScene scene, IGameForm gameForm)
        {
            timer = new Timer { Interval = 16 };
            timer.Tick += Update;
            timer.Tick += Time.OnGlobalTick;

            this.viewport = viewport;
            this.input = input;
            this.scene = scene;
            this.player = scene.Player;
            raycast = new Raycast(scene);

            minimapForm = new MinimapForm(viewport, input, scene);
            this.gameForm = gameForm;
            this.gameForm.Load += Start;
        }

        ~GamePresenter()
        {
            timer.Dispose();
        }

        private void Start(object sender, EventArgs e)
        {
            minimapForm.Show();
            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            player.Update();
            raycast.UpdateRaycastData();

            gameForm.UserInterface.DebugInfo.Update();
            minimapForm.Gizmos.Update();

            minimapForm.Invalidate();
            gameForm.Refresh();
        }
    }
}