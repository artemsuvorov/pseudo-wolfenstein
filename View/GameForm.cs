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

    public interface IGameForm : IInputClient, IViewport
    {
        event EventHandler Load;

        UserInterface UserInterface { get; }
        Viewport Viewport { get; }

        void Refresh();
        //void Update();
        //void Invalidate();
    }

    public partial class GameForm : Form, IGameForm, IInputClient, IViewport
    {
        public UserInterface UserInterface { get; private set; }
        public Viewport Viewport { get; private set; }

        private readonly Camera camera;

        //private static GameForm instance;
        //public static GameForm Instance
        //{
        //    get
        //    {
        //        if (instance is null)
        //            instance = new GameForm();
        //        return instance;
        //    }
        //}

        public GameForm()
        {
            InitializeComponent();

            Viewport = new Viewport(this);
            camera = new Camera(Viewport);

            Paint += Redraw;
        }

        public void Initialize(Input input, Player player)
        {
            UserInterface = new UserInterface(input, player);
            Controls.Add(UserInterface);
        }

        private void Redraw(object sender, PaintEventArgs e)
        {
            using var backgroundBrush = new SolidBrush(Settings.BackgroundColor);
            e.Graphics.SmoothingMode = Settings.GraphicsSmoothingMode;

            e.Graphics.FillRectangle(backgroundBrush, e.ClipRectangle);
            camera.DrawView(e.Graphics);
        }
    }
}