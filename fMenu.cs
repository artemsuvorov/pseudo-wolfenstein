using System;
using System.Media;
using System.Windows.Forms;
using PseudoWolfenstein.Model;
using PseudoWolfenstein.View;

namespace PseudoWolfenstein
{
    public partial class fMenu : Form
    {
        private readonly SoundPlayer soundPlayer;
        private GameForm gameForm;

        private fMenu()
        {
            InitializeComponent();
        }

        public fMenu(SoundPlayer soundPlayer) : this()
        {
            this.soundPlayer = soundPlayer;
        }

        private void button1_Click(object sender, EventArgs e)
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

        private void fMenu_Load(object sender, EventArgs e)
        {
        }

        private void ButtonExitClick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
