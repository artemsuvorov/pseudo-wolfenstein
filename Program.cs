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

            var src = Scene.SceneBuilder.Level_2;
            var scene = Scene.Builder.FromString(src);
            var gameForm = new GameForm(scene);
            _ = new GamePresenter(scene, gameForm);
            Application.Run(gameForm);
        }
    }
}
