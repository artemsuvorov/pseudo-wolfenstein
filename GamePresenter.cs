﻿using PseudoWolfenstein.Core;
using PseudoWolfenstein.Model;
using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    internal class GamePresenter
    {
        private readonly Timer timer;
        private readonly Viewport viewport;
        private readonly IGameForm gameForm;
        //private readonly MinimapForm minimapForm;
        private readonly Player player;
        private readonly Scene scene;
        private readonly Raycast raycast;

        public GamePresenter(Scene scene, IGameForm gameForm)
        {
            timer = new Timer { Interval = 16 };
            timer.Tick += Update;
            timer.Tick += Time.OnGlobalTick;

            this.viewport = gameForm.GetViewport();
            this.scene = scene;
            this.player = scene.Player;
            raycast = new Raycast(scene);

            //minimapForm = new MinimapForm(viewport, scene);
            this.gameForm = gameForm;
            this.gameForm.Load += Start;
        }

        ~GamePresenter()
        {
            timer.Dispose();
        }

        private void Start(object sender, EventArgs e)
        {
            //minimapForm.Show();
            timer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            Time.Update();
            scene.Update();

            gameForm.UserInterface.DebugInfo.Update();
            //minimapForm.Gizmos.Update();

            //minimapForm.Invalidate();
            gameForm.Refresh();
        }
    }
}