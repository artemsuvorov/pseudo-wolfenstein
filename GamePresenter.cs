using PseudoWolfenstein.Core;
using PseudoWolfenstein.Core.Raycasting;
using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    internal class GamePresenter
    {
        private static Timer timer;
        private static GameForm gameForm;
        private static MinimapForm minimapForm;

        public GamePresenter()
        {
            timer = new Timer { Interval = 16 };
            timer.Tick += Update;
            timer.Tick += Time.OnGlobalTick;

            minimapForm = MinimapForm.Instance;
            gameForm = GameForm.Instance;
            gameForm.Load += Start;
        }

        ~GamePresenter()
        {
            timer.Dispose();
            gameForm.Dispose();
            minimapForm.Dispose();
        }

        private void Start(object sender, EventArgs e)
        {
            minimapForm.Show();
            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            Player.Update();
            Raycast.UpdateRaycastData();

            DebugInfo.Update();
            Gizmos.Update();

            minimapForm.Invalidate();
            gameForm.Refresh();
        }
    }
}