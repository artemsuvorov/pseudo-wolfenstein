using PseudoWolfenstein.Model;
using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;

namespace PseudoWolfenstein
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var gameForm = new GameForm();
            var viewport = gameForm.Viewport;
            var scene = Scene.Builder.SingleBlockScene;
            gameForm.Initialize(scene.Player, scene);
            _ = new GamePresenter(viewport, scene, gameForm);
            Application.Run(gameForm);
        }
    }
}
