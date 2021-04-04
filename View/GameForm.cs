using PseudoWolfenstein.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PseudoWolfenstein.View
{
    public interface IInputClient
    {
        event KeyEventHandler KeyDown;
        event KeyEventHandler KeyUp;

        event MouseEventHandler MouseMove;
        event MouseEventHandler MouseDown;
        event MouseEventHandler MouseUp;
    }

    public interface IGameForm : IInputClient
    {
        //event EventHandler Load;

        //UserInterface UserInterface { get; }

        //void Update();
        //void Invalidate();
    }

    public partial class GameForm : Form, IGameForm, IInputClient
    {
        public static UserInterface UserInterface 
        { 
            get => userInterface;
            private set => userInterface = value;
        }

        private static GameForm instance;
        public static GameForm Instance
        {
            get
            {
                if (instance is null)
                    instance = new GameForm();
                return instance;
            }
        }

        private GameForm()
        {
            InitializeComponent();
            Paint += Redraw;
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            using var backgroundBrush = new SolidBrush(Settings.BackgroundColor);
            e.Graphics.SmoothingMode = Settings.GraphicsSmoothingMode;

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);

            Camera.DrawView(e.Graphics);
        }
    }
}