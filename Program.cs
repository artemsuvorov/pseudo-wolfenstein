using PseudoWolfenstein.Model;
using PseudoWolfenstein.View;
using System;
using System.Windows.Forms;
using System.Media;

namespace PseudoWolfenstein
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void StopMusic(SoundPlayer x)
        {
            x.Stop();
        }
        [STAThread]
        
        static void Main()
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