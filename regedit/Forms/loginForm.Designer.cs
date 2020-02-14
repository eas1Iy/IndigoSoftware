namespace IndigoSoftware.Forms
{
    partial class loginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(loginForm));
            this.nextButt = new MaterialSkin.Controls.MaterialFlatButton();
            this.vkLogo = new System.Windows.Forms.PictureBox();
            this.siteLogo = new System.Windows.Forms.PictureBox();
            this.oznRadBut = new MaterialSkin.Controls.MaterialRadioButton();
            this.infLlb1 = new MaterialSkin.Controls.MaterialLabel();
            this.versionLbl = new MaterialSkin.Controls.MaterialLabel();
            ((System.ComponentModel.ISupportInitialize)(this.vkLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // nextButt
            // 
            this.nextButt.AutoSize = true;
            this.nextButt.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.nextButt.Depth = 0;
            this.nextButt.Icon = null;
            this.nextButt.Location = new System.Drawing.Point(118, 243);
            this.nextButt.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.nextButt.MouseState = MaterialSkin.MouseState.HOVER;
            this.nextButt.Name = "nextButt";
            this.nextButt.Primary = false;
            this.nextButt.Size = new System.Drawing.Size(139, 36);
            this.nextButt.TabIndex = 0;
            this.nextButt.Text = "   Продолжить   ";
            this.nextButt.UseVisualStyleBackColor = true;
            this.nextButt.Click += new System.EventHandler(this.materialFlatButton1_Click);
            // 
            // vkLogo
            // 
            this.vkLogo.BackColor = System.Drawing.Color.Transparent;
            this.vkLogo.Image = ((System.Drawing.Image)(resources.GetObject("vkLogo.Image")));
            this.vkLogo.Location = new System.Drawing.Point(12, 288);
            this.vkLogo.Name = "vkLogo";
            this.vkLogo.Size = new System.Drawing.Size(114, 92);
            this.vkLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.vkLogo.TabIndex = 1;
            this.vkLogo.TabStop = false;
            this.vkLogo.Click += new System.EventHandler(this.pictureBox1_Click);
            this.vkLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // siteLogo
            // 
            this.siteLogo.BackColor = System.Drawing.Color.Transparent;
            this.siteLogo.Image = ((System.Drawing.Image)(resources.GetObject("siteLogo.Image")));
            this.siteLogo.Location = new System.Drawing.Point(244, 288);
            this.siteLogo.Name = "siteLogo";
            this.siteLogo.Size = new System.Drawing.Size(114, 92);
            this.siteLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.siteLogo.TabIndex = 2;
            this.siteLogo.TabStop = false;
            this.siteLogo.Click += new System.EventHandler(this.pictureBox2_Click);
            this.siteLogo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseMove);
            // 
            // oznRadBut
            // 
            this.oznRadBut.AutoSize = true;
            this.oznRadBut.Depth = 0;
            this.oznRadBut.Font = new System.Drawing.Font("Roboto", 10F);
            this.oznRadBut.Location = new System.Drawing.Point(118, 207);
            this.oznRadBut.Margin = new System.Windows.Forms.Padding(0);
            this.oznRadBut.MouseLocation = new System.Drawing.Point(-1, -1);
            this.oznRadBut.MouseState = MaterialSkin.MouseState.HOVER;
            this.oznRadBut.Name = "oznRadBut";
            this.oznRadBut.Ripple = true;
            this.oznRadBut.Size = new System.Drawing.Size(140, 30);
            this.oznRadBut.TabIndex = 3;
            this.oznRadBut.Text = "я ознакомился    ";
            this.oznRadBut.UseVisualStyleBackColor = true;
            // 
            // infLlb1
            // 
            this.infLlb1.Depth = 0;
            this.infLlb1.Font = new System.Drawing.Font("Roboto", 11F);
            this.infLlb1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.infLlb1.Location = new System.Drawing.Point(6, 72);
            this.infLlb1.MouseState = MaterialSkin.MouseState.HOVER;
            this.infLlb1.Name = "infLlb1";
            this.infLlb1.Size = new System.Drawing.Size(358, 131);
            this.infLlb1.TabIndex = 4;
            this.infLlb1.Text = "Внимание! После продолжения, вы автоматически соглашаетесь с правилами использова" +
    "ния, описанными на нашем сайте! \r\n\r\nКнопка для перехода на сайт находится снизу." +
    "";
            // 
            // versionLbl
            // 
            this.versionLbl.Depth = 0;
            this.versionLbl.Font = new System.Drawing.Font("Roboto", 11F);
            this.versionLbl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.versionLbl.Location = new System.Drawing.Point(132, 288);
            this.versionLbl.MouseState = MaterialSkin.MouseState.HOVER;
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(106, 31);
            this.versionLbl.TabIndex = 5;
            this.versionLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 392);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.infLlb1);
            this.Controls.Add(this.oznRadBut);
            this.Controls.Add(this.siteLogo);
            this.Controls.Add(this.vkLogo);
            this.Controls.Add(this.nextButt);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "loginForm";
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "                          IndigoSoftware";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.loginForm_FormClosed);
            this.Load += new System.EventHandler(this.loginForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vkLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.siteLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialFlatButton nextButt;
        private System.Windows.Forms.PictureBox vkLogo;
        private System.Windows.Forms.PictureBox siteLogo;
        private MaterialSkin.Controls.MaterialRadioButton oznRadBut;
        private MaterialSkin.Controls.MaterialLabel infLlb1;
        private MaterialSkin.Controls.MaterialLabel versionLbl;
    }
}