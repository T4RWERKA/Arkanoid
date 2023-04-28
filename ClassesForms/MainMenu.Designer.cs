namespace ClassesForms
{
    partial class MainMenu
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
            lblName = new Label();
            btnStart = new Button();
            btnLoad = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Noto Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblName.Location = new Point(228, 46);
            lblName.Name = "lblName";
            lblName.Size = new Size(130, 33);
            lblName.TabIndex = 0;
            lblName.Text = "Arkanoid";
            // 
            // btnStart
            // 
            btnStart.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnStart.Location = new Point(217, 189);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(150, 46);
            btnStart.TabIndex = 1;
            btnStart.Text = "Start";
            btnStart.UseVisualStyleBackColor = true;
            btnStart.Click += btnStart_Click;
            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoad.Location = new Point(217, 268);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(150, 46);
            btnLoad.TabIndex = 2;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnExit.Location = new Point(217, 344);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 46);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 529);
            Controls.Add(btnExit);
            Controls.Add(btnLoad);
            Controls.Add(btnStart);
            Controls.Add(lblName);
            Name = "MainMenu";
            Text = "Form1";
            KeyDown += MainMenu_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Button btnStart;
        private Button btnLoad;
        private Button btnExit;
    }
}