namespace Desktop_App
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            panel1 = new Panel();
            pbxCloseWindow = new PictureBox();
            pbxLogo = new PictureBox();
            btnBooks = new Button();
            panelChildForm = new Panel();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbxCloseWindow).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.ControlDark;
            panel1.Controls.Add(pbxCloseWindow);
            panel1.Controls.Add(pbxLogo);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // pbxCloseWindow
            // 
            pbxCloseWindow.BackColor = SystemColors.ControlDark;
            resources.ApplyResources(pbxCloseWindow, "pbxCloseWindow");
            pbxCloseWindow.Name = "pbxCloseWindow";
            pbxCloseWindow.TabStop = false;
            pbxCloseWindow.Click += pbxCloseWindow_Click;
            // 
            // pbxLogo
            // 
            pbxLogo.Image = Properties.Resources.KajanReview_Logo_Cropped;
            resources.ApplyResources(pbxLogo, "pbxLogo");
            pbxLogo.Name = "pbxLogo";
            pbxLogo.TabStop = false;
            // 
            // btnBooks
            // 
            resources.ApplyResources(btnBooks, "btnBooks");
            btnBooks.BackColor = SystemColors.ControlDark;
            btnBooks.FlatAppearance.BorderSize = 0;
            btnBooks.Name = "btnBooks";
            btnBooks.UseVisualStyleBackColor = false;
            btnBooks.Click += btnBooks_Click;
            // 
            // panelChildForm
            // 
            resources.ApplyResources(panelChildForm, "panelChildForm");
            panelChildForm.Name = "panelChildForm";
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            ControlBox = false;
            Controls.Add(panelChildForm);
            Controls.Add(btnBooks);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "MainForm";
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbxCloseWindow).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbxLogo).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pbxLogo;
        private PictureBox pbxCloseWindow;
        private Panel panel2;
        private Button btnBooks;
        private Panel panelChildForm;
    }
}
