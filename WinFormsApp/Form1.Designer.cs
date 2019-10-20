namespace WinFormsApp
{
    partial class BlackJack
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
        [System.Obsolete]
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlackJack));
            this.speler5 = new System.Windows.Forms.TextBox();
            this.speler4 = new System.Windows.Forms.TextBox();
            this.speler3 = new System.Windows.Forms.TextBox();
            this.speler2 = new System.Windows.Forms.TextBox();
            this.speler1 = new System.Windows.Forms.TextBox();
            this.dealer = new System.Windows.Forms.TextBox();
            this.adodcArray1 = new Microsoft.VisualBasic.Compatibility.VB6.ADODCArray(this.components);
            this.adodcArray2 = new Microsoft.VisualBasic.Compatibility.VB6.ADODCArray(this.components);
            this.jImageButton1 = new JImageButton.JImageButton();
            this.jImageButton2 = new JImageButton.JImageButton();
            this.Hand1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.Start = new System.Windows.Forms.Button();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.adodcArray1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.adodcArray2)).BeginInit();
            this.SuspendLayout();
            // 
            // speler5
            // 
            this.speler5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.speler5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speler5.ForeColor = System.Drawing.SystemColors.Info;
            this.speler5.Location = new System.Drawing.Point(852, 463);
            this.speler5.Name = "speler5";
            this.speler5.Size = new System.Drawing.Size(128, 29);
            this.speler5.TabIndex = 0;
            this.speler5.Text = "Geen Speler";
            this.speler5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // speler4
            // 
            this.speler4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.speler4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speler4.ForeColor = System.Drawing.SystemColors.Info;
            this.speler4.Location = new System.Drawing.Point(658, 526);
            this.speler4.Name = "speler4";
            this.speler4.Size = new System.Drawing.Size(128, 29);
            this.speler4.TabIndex = 6;
            this.speler4.Text = "Geen Speler";
            this.speler4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // speler3
            // 
            this.speler3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.speler3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speler3.ForeColor = System.Drawing.SystemColors.Info;
            this.speler3.Location = new System.Drawing.Point(447, 559);
            this.speler3.Name = "speler3";
            this.speler3.Size = new System.Drawing.Size(128, 29);
            this.speler3.TabIndex = 7;
            this.speler3.Text = "Geen Speler";
            this.speler3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // speler2
            // 
            this.speler2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.speler2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speler2.ForeColor = System.Drawing.SystemColors.Info;
            this.speler2.Location = new System.Drawing.Point(219, 526);
            this.speler2.Name = "speler2";
            this.speler2.Size = new System.Drawing.Size(128, 29);
            this.speler2.TabIndex = 8;
            this.speler2.Text = "Geen Speler";
            this.speler2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // speler1
            // 
            this.speler1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.speler1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speler1.ForeColor = System.Drawing.SystemColors.Info;
            this.speler1.Location = new System.Drawing.Point(13, 463);
            this.speler1.Name = "speler1";
            this.speler1.Size = new System.Drawing.Size(128, 29);
            this.speler1.TabIndex = 9;
            this.speler1.Text = "Geen Speler";
            this.speler1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dealer
            // 
            this.dealer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this.dealer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dealer.ForeColor = System.Drawing.SystemColors.Info;
            this.dealer.Location = new System.Drawing.Point(361, 86);
            this.dealer.Name = "dealer";
            this.dealer.Size = new System.Drawing.Size(309, 29);
            this.dealer.TabIndex = 10;
            this.dealer.Text = "Dealer";
            this.dealer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // jImageButton1
            // 
            this.jImageButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jImageButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.jImageButton1.CausesValidation = false;
            this.jImageButton1.Cursor = System.Windows.Forms.Cursors.Default;
            this.jImageButton1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("jImageButton1.ErrorImage")));
            this.jImageButton1.Image = ((System.Drawing.Image)(resources.GetObject("jImageButton1.Image")));
            this.jImageButton1.ImageHover = null;
            this.jImageButton1.InitialImage = null;
            this.jImageButton1.Location = new System.Drawing.Point(0, 2);
            this.jImageButton1.Name = "jImageButton1";
            this.jImageButton1.Size = new System.Drawing.Size(88, 77);
            this.jImageButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.jImageButton1.TabIndex = 11;
            this.jImageButton1.Zoom = 4;
            // 
            // jImageButton2
            // 
            this.jImageButton2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.jImageButton2.BackColor = System.Drawing.SystemColors.Highlight;
            this.jImageButton2.CausesValidation = false;
            this.jImageButton2.Cursor = System.Windows.Forms.Cursors.Default;
            this.jImageButton2.ErrorImage = ((System.Drawing.Image)(resources.GetObject("jImageButton2.ErrorImage")));
            this.jImageButton2.Image = ((System.Drawing.Image)(resources.GetObject("jImageButton2.Image")));
            this.jImageButton2.ImageHover = null;
            this.jImageButton2.InitialImage = null;
            this.jImageButton2.Location = new System.Drawing.Point(477, 12);
            this.jImageButton2.Name = "jImageButton2";
            this.jImageButton2.Size = new System.Drawing.Size(74, 77);
            this.jImageButton2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.jImageButton2.TabIndex = 12;
            this.jImageButton2.Zoom = 4;
            // 
            // Hand1
            // 
            this.Hand1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.Hand1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Hand1.Location = new System.Drawing.Point(13, 343);
            this.Hand1.Name = "Hand1";
            this.Hand1.Size = new System.Drawing.Size(128, 114);
            this.Hand1.TabIndex = 13;
            this.Hand1.Text = "Hoi";
            // 
            // richTextBox2
            // 
            this.richTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.richTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox2.Location = new System.Drawing.Point(219, 415);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(128, 105);
            this.richTextBox2.TabIndex = 14;
            this.richTextBox2.Text = "Hoi";
            // 
            // richTextBox3
            // 
            this.richTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.richTextBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox3.Location = new System.Drawing.Point(447, 449);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(130, 104);
            this.richTextBox3.TabIndex = 15;
            this.richTextBox3.Text = "Hoi";
            // 
            // richTextBox4
            // 
            this.richTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.richTextBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox4.Location = new System.Drawing.Point(658, 415);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(128, 105);
            this.richTextBox4.TabIndex = 16;
            this.richTextBox4.Text = "Hoi";
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.Color.Gray;
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(412, 206);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(199, 92);
            this.Start.TabIndex = 19;
            this.Start.Text = "Start !!";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // richTextBox5
            // 
            this.richTextBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.richTextBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox5.Location = new System.Drawing.Point(852, 343);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(128, 114);
            this.richTextBox5.TabIndex = 20;
            this.richTextBox5.Text = "Hoi";
            // 
            // richTextBox6
            // 
            this.richTextBox6.Location = new System.Drawing.Point(449, 121);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.Size = new System.Drawing.Size(128, 55);
            this.richTextBox6.TabIndex = 18;
            this.richTextBox6.Text = "Hoi";
            this.richTextBox6.Visible = false;
            // 
            // BlackJack
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(3)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(1003, 638);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.Hand1);
            this.Controls.Add(this.jImageButton2);
            this.Controls.Add(this.jImageButton1);
            this.Controls.Add(this.dealer);
            this.Controls.Add(this.speler1);
            this.Controls.Add(this.speler2);
            this.Controls.Add(this.speler3);
            this.Controls.Add(this.speler4);
            this.Controls.Add(this.speler5);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.MaximizeBox = false;
            this.Name = "BlackJack";
            this.ShowIcon = false;
            this.Text = "BlackJack";
            this.Load += new System.EventHandler(this.BlackJack_Load);
            ((System.ComponentModel.ISupportInitialize)(this.adodcArray1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.adodcArray2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox speler5;
        private System.Windows.Forms.TextBox speler4;
        private System.Windows.Forms.TextBox speler3;
        private System.Windows.Forms.TextBox speler2;
        private System.Windows.Forms.TextBox speler1;
        private System.Windows.Forms.TextBox dealer;
        [System.Obsolete]
        private Microsoft.VisualBasic.Compatibility.VB6.ADODCArray adodcArray1;
        [System.Obsolete]
        private Microsoft.VisualBasic.Compatibility.VB6.ADODCArray adodcArray2;
        private JImageButton.JImageButton jImageButton1;
        private JImageButton.JImageButton jImageButton2;
        private System.Windows.Forms.RichTextBox Hand1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.RichTextBox richTextBox6;
    }
}

