namespace PseudoWolfenstein.View
{
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            userInterface = new PseudoWolfenstein.View.UserInterface();
            this.SuspendLayout();
            // 
            // button1
            // 
            userInterface.Name = "userInterface";
            userInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            userInterface.TabIndex = 0;
            // 
            // GameForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(userInterface);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Consolas", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "GameForm";
            this.Text = "PseudoWolfenstein";
            this.ResumeLayout(false);
        }

        private static PseudoWolfenstein.View.UserInterface userInterface;
    }
}