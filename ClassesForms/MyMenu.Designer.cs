namespace ClassesForms
{
    partial class MyMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblName = new Label();
            btnExit = new Button();
            btnLoad = new Button();
            btnSave = new Button();
            btnContinue = new Button();
            SuspendLayout();
            // 
            // lblName
            // 
            lblName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblName.AutoSize = true;
            lblName.Font = new Font("Noto Serif", 9F, FontStyle.Bold, GraphicsUnit.Point);
            lblName.Location = new Point(164, 46);
            lblName.Name = "lblName";
            lblName.Size = new Size(130, 33);
            lblName.TabIndex = 1;
            lblName.Text = "Arkanoid";
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnExit.Location = new Point(152, 301);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(150, 46);
            btnExit.TabIndex = 4;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // btnLoad
            // 
            btnLoad.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnLoad.Location = new Point(152, 180);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(150, 46);
            btnLoad.TabIndex = 5;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSave.Location = new Point(152, 239);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(150, 46);
            btnSave.TabIndex = 6;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnContinue
            // 
            btnContinue.Font = new Font("Alef", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnContinue.Location = new Point(152, 117);
            btnContinue.Name = "btnContinue";
            btnContinue.Size = new Size(150, 46);
            btnContinue.TabIndex = 7;
            btnContinue.Text = "Continue";
            btnContinue.UseVisualStyleBackColor = true;
            btnContinue.Click += btnContinue_Click;
            // 
            // MyMenu
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(474, 429);
            Controls.Add(btnContinue);
            Controls.Add(btnSave);
            Controls.Add(btnLoad);
            Controls.Add(btnExit);
            Controls.Add(lblName);
            Name = "MyMenu";
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Button btnExit;
        private Button btnLoad;
        private Button btnSave;
        private Button btnContinue;
    }
}