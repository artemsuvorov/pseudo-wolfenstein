using PseudoWolfenstein.Core;
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
            var input = new Input(viewport);
            var player = new Player(input);
            var scene = Scene.Default(player);
            gameForm.Initialize(input, player, scene);
            _ = new GamePresenter(viewport, input, scene, gameForm);
            Application.Run(gameForm);
        }
    }
}
