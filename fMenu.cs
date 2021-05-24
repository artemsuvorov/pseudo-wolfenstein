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
            var src = Scene.SceneBuilder.Level_1;
            var scene = Scene.Builder.FromString(src);
            var gameForm = new GameForm(scene);
            _ = new GamePresenter(scene, gameForm);
            gameForm.Closed += OnGameFormClosed;
            soundPlayer.Stop();
            this.Hide();
            gameForm.ShowDialog();
        }

        private void OnGameFormClosed(object sender, EventArgs e)
        {
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
