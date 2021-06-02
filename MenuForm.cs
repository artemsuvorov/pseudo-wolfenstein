using System;
using System.Media;
using System.Windows.Forms;
using PseudoWolfenstein.Core;
using PseudoWolfenstein.View;

namespace PseudoWolfenstein
{
    public partial class MenuForm : Form
    {
        private readonly SoundPlayer soundPlayer;
        private GameForm gameForm;

        public MenuForm()
        {
            InitializeComponent();
            Icon = Repository.Images.Icon;
            BackgroundImage = Repository.Images.MenuBackground;

            var path = Repository.Music.MenuBackgroundMusicPath;
            soundPlayer = new SoundPlayer(path);
            soundPlayer.PlayLooping();
        }

        private void OnNewGameButtonClick(object sender, EventArgs e)
        {
            gameForm = new GameForm();
            _ = new GamePresenter(gameForm);
            gameForm.Closed += OnGameFormClosed;
            soundPlayer.Stop();
            this.Hide();
            gameForm.ShowDialog();
        }

        private void OnGameFormClosed(object sender, EventArgs e)
        {
            soundPlayer.PlayLooping();
            gameForm.Closed -= OnGameFormClosed;
            this.Show();
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}