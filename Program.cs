using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;
using System.Media;

namespace PseudoWolfenstein
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        
        private static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
         
            var soundPlayer = new SoundPlayer(@"D:\pseudo-wolfenstein\wolf_menu.wav");
            soundPlayer.PlayLooping();
            var fMenu = new fMenu(soundPlayer);
            Application.Run(fMenu);
        }

    }

}