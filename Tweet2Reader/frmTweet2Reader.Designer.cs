namespace Twitter2Reader
{
    partial class frmTweet2Reader
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
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.button2 = new System.Windows.Forms.Button();
            this.chkTweets = new System.Windows.Forms.CheckBox();
            this.chkFavorites = new System.Windows.Forms.CheckBox();
            this.chkRetweets = new System.Windows.Forms.CheckBox();
            this.chkLinks = new System.Windows.Forms.CheckBox();
            this.chkNoLinks = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(139, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(173, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Authenticate with twitter";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Google Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Google Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(139, 74);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 20);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(139, 101);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(173, 20);
            this.textBox2.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(45, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Interval (Minutes)";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(139, 200);
            this.trackBar1.Maximum = 180;
            this.trackBar1.Minimum = 5;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(173, 45);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.Value = 15;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(182, 251);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkTweets
            // 
            this.chkTweets.AutoSize = true;
            this.chkTweets.Checked = true;
            this.chkTweets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTweets.Location = new System.Drawing.Point(43, 140);
            this.chkTweets.Name = "chkTweets";
            this.chkTweets.Size = new System.Drawing.Size(61, 17);
            this.chkTweets.TabIndex = 8;
            this.chkTweets.Text = "Tweets";
            this.chkTweets.UseVisualStyleBackColor = true;
            // 
            // chkFavorites
            // 
            this.chkFavorites.AutoSize = true;
            this.chkFavorites.Checked = true;
            this.chkFavorites.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFavorites.Location = new System.Drawing.Point(139, 140);
            this.chkFavorites.Name = "chkFavorites";
            this.chkFavorites.Size = new System.Drawing.Size(69, 17);
            this.chkFavorites.TabIndex = 9;
            this.chkFavorites.Text = "Favorites";
            this.chkFavorites.UseVisualStyleBackColor = true;
            // 
            // chkRetweets
            // 
            this.chkRetweets.AutoSize = true;
            this.chkRetweets.Checked = true;
            this.chkRetweets.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRetweets.Location = new System.Drawing.Point(241, 140);
            this.chkRetweets.Name = "chkRetweets";
            this.chkRetweets.Size = new System.Drawing.Size(71, 17);
            this.chkRetweets.TabIndex = 10;
            this.chkRetweets.Text = "Retweets";
            this.chkRetweets.UseVisualStyleBackColor = true;
            // 
            // chkLinks
            // 
            this.chkLinks.AutoSize = true;
            this.chkLinks.Checked = true;
            this.chkLinks.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkLinks.Location = new System.Drawing.Point(43, 171);
            this.chkLinks.Name = "chkLinks";
            this.chkLinks.Size = new System.Drawing.Size(107, 17);
            this.chkLinks.TabIndex = 11;
            this.chkLinks.Text = "Tweets with links";
            this.chkLinks.UseVisualStyleBackColor = true;
            // 
            // chkNoLinks
            // 
            this.chkNoLinks.AutoSize = true;
            this.chkNoLinks.Location = new System.Drawing.Point(182, 171);
            this.chkNoLinks.Name = "chkNoLinks";
            this.chkNoLinks.Size = new System.Drawing.Size(122, 17);
            this.chkNoLinks.TabIndex = 12;
            this.chkNoLinks.Text = "Tweets without links";
            this.chkNoLinks.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(342, 13);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(338, 316);
            this.listBox1.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 13);
            this.label4.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(68, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 13);
            this.label5.TabIndex = 15;
            // 
            // frmTwitter2Reader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 344);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.chkNoLinks);
            this.Controls.Add(this.chkLinks);
            this.Controls.Add(this.chkRetweets);
            this.Controls.Add(this.chkFavorites);
            this.Controls.Add(this.chkTweets);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "frmTwitter2Reader";
            this.Text = "Twitter2Reader";
            this.Load += new System.EventHandler(this.frmTwitter2Reader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkTweets;
        private System.Windows.Forms.CheckBox chkFavorites;
        private System.Windows.Forms.CheckBox chkRetweets;
        private System.Windows.Forms.CheckBox chkLinks;
        private System.Windows.Forms.CheckBox chkNoLinks;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}

