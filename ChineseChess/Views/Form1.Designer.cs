namespace ChineseChess
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.StartOfflineGameBtn = new System.Windows.Forms.Button();
            this.ConnectToServerBtn = new System.Windows.Forms.Button();
            this.StartStopServerBtn = new System.Windows.Forms.Button();
            this.MessagesTextBox = new System.Windows.Forms.TextBox();
            this.URL = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.NickName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SendChatBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(530, 590);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.PictureBox1_Click);
            // 
            // StartOfflineGameBtn
            // 
            this.StartOfflineGameBtn.Location = new System.Drawing.Point(34, 46);
            this.StartOfflineGameBtn.Name = "StartOfflineGameBtn";
            this.StartOfflineGameBtn.Size = new System.Drawing.Size(77, 40);
            this.StartOfflineGameBtn.TabIndex = 1;
            this.StartOfflineGameBtn.Text = "開始遊戲";
            this.StartOfflineGameBtn.UseVisualStyleBackColor = true;
            this.StartOfflineGameBtn.Click += new System.EventHandler(this.StartOfflineGameBtn_Click);
            // 
            // ConnectToServerBtn
            // 
            this.ConnectToServerBtn.Location = new System.Drawing.Point(145, 81);
            this.ConnectToServerBtn.Name = "ConnectToServerBtn";
            this.ConnectToServerBtn.Size = new System.Drawing.Size(56, 22);
            this.ConnectToServerBtn.TabIndex = 2;
            this.ConnectToServerBtn.Text = "Connect";
            this.ConnectToServerBtn.UseVisualStyleBackColor = true;
            this.ConnectToServerBtn.Click += new System.EventHandler(this.ConnectToServerBtn_Click);
            // 
            // StartStopServerBtn
            // 
            this.StartStopServerBtn.Location = new System.Drawing.Point(6, 52);
            this.StartStopServerBtn.Name = "StartStopServerBtn";
            this.StartStopServerBtn.Size = new System.Drawing.Size(195, 23);
            this.StartStopServerBtn.TabIndex = 3;
            this.StartStopServerBtn.Text = "Start Server";
            this.StartStopServerBtn.UseVisualStyleBackColor = true;
            this.StartStopServerBtn.Click += new System.EventHandler(this.StartStopServer_Click);
            // 
            // MessagesTextBox
            // 
            this.MessagesTextBox.Font = new System.Drawing.Font("新細明體", 12F);
            this.MessagesTextBox.Location = new System.Drawing.Point(536, 132);
            this.MessagesTextBox.Multiline = true;
            this.MessagesTextBox.Name = "MessagesTextBox";
            this.MessagesTextBox.ReadOnly = true;
            this.MessagesTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MessagesTextBox.Size = new System.Drawing.Size(368, 430);
            this.MessagesTextBox.TabIndex = 4;
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(39, 81);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(100, 22);
            this.URL.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.NickName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.StartStopServerBtn);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.ConnectToServerBtn);
            this.groupBox1.Controls.Add(this.URL);
            this.groupBox1.Location = new System.Drawing.Point(696, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 114);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "連線遊戲";
            // 
            // NickName
            // 
            this.NickName.Location = new System.Drawing.Point(94, 21);
            this.NickName.Name = "NickName";
            this.NickName.Size = new System.Drawing.Size(100, 22);
            this.NickName.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 11F);
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "遊戲暱稱：";
            // 
            // ChatInput
            // 
            this.ChatInput.Location = new System.Drawing.Point(536, 568);
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(307, 22);
            this.ChatInput.TabIndex = 8;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.StartOfflineGameBtn);
            this.groupBox2.Location = new System.Drawing.Point(536, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 113);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "單機遊戲";
            // 
            // SendChatBtn
            // 
            this.SendChatBtn.Location = new System.Drawing.Point(849, 568);
            this.SendChatBtn.Name = "SendChatBtn";
            this.SendChatBtn.Size = new System.Drawing.Size(54, 21);
            this.SendChatBtn.TabIndex = 10;
            this.SendChatBtn.Text = "發送";
            this.SendChatBtn.UseVisualStyleBackColor = true;
            this.SendChatBtn.Click += new System.EventHandler(this.SendChatBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 593);
            this.Controls.Add(this.SendChatBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ChatInput);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.MessagesTextBox);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "XX首家線上象棋上線啦";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button SendChatBtn;
        public System.Windows.Forms.Button StartOfflineGameBtn;
        public System.Windows.Forms.Button ConnectToServerBtn;
        public System.Windows.Forms.Button StartStopServerBtn;
        public System.Windows.Forms.TextBox URL;
        public System.Windows.Forms.TextBox NickName;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.TextBox MessagesTextBox;
    }
}

