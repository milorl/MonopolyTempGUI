namespace MonopolyTempGUI
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.labelLogin = new System.Windows.Forms.Label();
            this.labelPassword = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.buttonRegister = new System.Windows.Forms.Button();
            this.actionBox = new System.Windows.Forms.RichTextBox();
            this.walletBox = new System.Windows.Forms.RichTextBox();
            this.chatBox = new System.Windows.Forms.RichTextBox();
            this.buttonMove = new System.Windows.Forms.Button();
            this.positionBox = new System.Windows.Forms.RichTextBox();
            this.buttonBuy = new System.Windows.Forms.Button();
            this.buttonSell = new System.Windows.Forms.Button();
            this.labelChat = new System.Windows.Forms.Label();
            this.labelWallet = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.labelPositions = new System.Windows.Forms.Label();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.buttonResign = new System.Windows.Forms.Button();
            this.sellBox = new System.Windows.Forms.TextBox();
            this.textBoxRoom = new System.Windows.Forms.TextBox();
            this.buttonJoin = new System.Windows.Forms.Button();
            this.labelRoom = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "username:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "roomname:";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(71, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.Visible = false;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(71, 32);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 3;
            this.textBox2.Visible = false;
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Location = new System.Drawing.Point(371, 270);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(383, 20);
            this.textBoxLogin.TabIndex = 4;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Location = new System.Drawing.Point(371, 296);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new System.Drawing.Size(383, 20);
            this.textBoxPassword.TabIndex = 5;
            // 
            // labelLogin
            // 
            this.labelLogin.AutoSize = true;
            this.labelLogin.Location = new System.Drawing.Point(309, 273);
            this.labelLogin.Name = "labelLogin";
            this.labelLogin.Size = new System.Drawing.Size(56, 13);
            this.labelLogin.TabIndex = 6;
            this.labelLogin.Text = "username:";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(309, 299);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(52, 13);
            this.labelPassword.TabIndex = 7;
            this.labelPassword.Text = "password";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(506, 351);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(75, 23);
            this.buttonLogin.TabIndex = 8;
            this.buttonLogin.Text = "Log In";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // buttonRegister
            // 
            this.buttonRegister.Location = new System.Drawing.Point(506, 322);
            this.buttonRegister.Name = "buttonRegister";
            this.buttonRegister.Size = new System.Drawing.Size(75, 23);
            this.buttonRegister.TabIndex = 9;
            this.buttonRegister.Text = "Register";
            this.buttonRegister.UseVisualStyleBackColor = true;
            this.buttonRegister.Click += new System.EventHandler(this.buttonRegister_Click);
            // 
            // actionBox
            // 
            this.actionBox.Enabled = false;
            this.actionBox.Location = new System.Drawing.Point(1080, 212);
            this.actionBox.Name = "actionBox";
            this.actionBox.Size = new System.Drawing.Size(488, 321);
            this.actionBox.TabIndex = 10;
            this.actionBox.Text = "";
            this.actionBox.Visible = false;
            // 
            // walletBox
            // 
            this.walletBox.Enabled = false;
            this.walletBox.Location = new System.Drawing.Point(1080, 92);
            this.walletBox.Name = "walletBox";
            this.walletBox.Size = new System.Drawing.Size(488, 103);
            this.walletBox.TabIndex = 11;
            this.walletBox.Text = "";
            this.walletBox.Visible = false;
            // 
            // chatBox
            // 
            this.chatBox.Enabled = false;
            this.chatBox.Location = new System.Drawing.Point(785, 273);
            this.chatBox.Name = "chatBox";
            this.chatBox.Size = new System.Drawing.Size(289, 170);
            this.chatBox.TabIndex = 12;
            this.chatBox.Text = "";
            this.chatBox.Visible = false;
            // 
            // buttonMove
            // 
            this.buttonMove.Enabled = false;
            this.buttonMove.Location = new System.Drawing.Point(1220, 539);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(75, 23);
            this.buttonMove.TabIndex = 13;
            this.buttonMove.Text = "Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            this.buttonMove.Visible = false;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // positionBox
            // 
            this.positionBox.Enabled = false;
            this.positionBox.Location = new System.Drawing.Point(1574, 270);
            this.positionBox.Name = "positionBox";
            this.positionBox.Size = new System.Drawing.Size(289, 170);
            this.positionBox.TabIndex = 14;
            this.positionBox.Text = "";
            this.positionBox.Visible = false;
            // 
            // buttonBuy
            // 
            this.buttonBuy.Enabled = false;
            this.buttonBuy.Location = new System.Drawing.Point(1301, 539);
            this.buttonBuy.Name = "buttonBuy";
            this.buttonBuy.Size = new System.Drawing.Size(75, 23);
            this.buttonBuy.TabIndex = 15;
            this.buttonBuy.Text = "Buy";
            this.buttonBuy.UseVisualStyleBackColor = true;
            this.buttonBuy.Visible = false;
            this.buttonBuy.Click += new System.EventHandler(this.buttonBuy_Click);
            // 
            // buttonSell
            // 
            this.buttonSell.Enabled = false;
            this.buttonSell.Location = new System.Drawing.Point(1382, 539);
            this.buttonSell.Name = "buttonSell";
            this.buttonSell.Size = new System.Drawing.Size(75, 23);
            this.buttonSell.TabIndex = 16;
            this.buttonSell.Text = "Sell";
            this.buttonSell.UseVisualStyleBackColor = true;
            this.buttonSell.Visible = false;
            // 
            // labelChat
            // 
            this.labelChat.AutoSize = true;
            this.labelChat.Enabled = false;
            this.labelChat.Location = new System.Drawing.Point(782, 257);
            this.labelChat.Name = "labelChat";
            this.labelChat.Size = new System.Drawing.Size(31, 13);
            this.labelChat.TabIndex = 17;
            this.labelChat.Text = "chat:";
            this.labelChat.Visible = false;
            // 
            // labelWallet
            // 
            this.labelWallet.AutoSize = true;
            this.labelWallet.Enabled = false;
            this.labelWallet.Location = new System.Drawing.Point(1077, 76);
            this.labelWallet.Name = "labelWallet";
            this.labelWallet.Size = new System.Drawing.Size(42, 13);
            this.labelWallet.TabIndex = 18;
            this.labelWallet.Text = "wallets:";
            this.labelWallet.Visible = false;
            // 
            // labelLog
            // 
            this.labelLog.AutoSize = true;
            this.labelLog.Enabled = false;
            this.labelLog.Location = new System.Drawing.Point(1077, 198);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(57, 13);
            this.labelLog.TabIndex = 19;
            this.labelLog.Text = "actionLog:";
            this.labelLog.Visible = false;
            // 
            // labelPositions
            // 
            this.labelPositions.AutoSize = true;
            this.labelPositions.Enabled = false;
            this.labelPositions.Location = new System.Drawing.Point(1571, 254);
            this.labelPositions.Name = "labelPositions";
            this.labelPositions.Size = new System.Drawing.Size(51, 13);
            this.labelPositions.TabIndex = 20;
            this.labelPositions.Text = "positions:";
            this.labelPositions.Visible = false;
            // 
            // buttonEnd
            // 
            this.buttonEnd.Enabled = false;
            this.buttonEnd.Location = new System.Drawing.Point(1301, 568);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(75, 23);
            this.buttonEnd.TabIndex = 21;
            this.buttonEnd.Text = "End Turn";
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Visible = false;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // buttonResign
            // 
            this.buttonResign.Enabled = false;
            this.buttonResign.Location = new System.Drawing.Point(1788, 12);
            this.buttonResign.Name = "buttonResign";
            this.buttonResign.Size = new System.Drawing.Size(75, 23);
            this.buttonResign.TabIndex = 22;
            this.buttonResign.Text = "Resign";
            this.buttonResign.UseVisualStyleBackColor = true;
            this.buttonResign.Visible = false;
            // 
            // sellBox
            // 
            this.sellBox.Enabled = false;
            this.sellBox.Location = new System.Drawing.Point(1463, 541);
            this.sellBox.Name = "sellBox";
            this.sellBox.Size = new System.Drawing.Size(100, 20);
            this.sellBox.TabIndex = 23;
            this.sellBox.Visible = false;
            // 
            // textBoxRoom
            // 
            this.textBoxRoom.Location = new System.Drawing.Point(371, 270);
            this.textBoxRoom.Name = "textBoxRoom";
            this.textBoxRoom.Size = new System.Drawing.Size(383, 20);
            this.textBoxRoom.TabIndex = 24;
            this.textBoxRoom.Visible = false;
            // 
            // buttonJoin
            // 
            this.buttonJoin.Location = new System.Drawing.Point(506, 296);
            this.buttonJoin.Name = "buttonJoin";
            this.buttonJoin.Size = new System.Drawing.Size(75, 23);
            this.buttonJoin.TabIndex = 25;
            this.buttonJoin.Text = "Join";
            this.buttonJoin.UseVisualStyleBackColor = true;
            this.buttonJoin.Visible = false;
            this.buttonJoin.Click += new System.EventHandler(this.buttonJoin_Click);
            // 
            // labelRoom
            // 
            this.labelRoom.AutoSize = true;
            this.labelRoom.Location = new System.Drawing.Point(516, 254);
            this.labelRoom.Name = "labelRoom";
            this.labelRoom.Size = new System.Drawing.Size(56, 13);
            this.labelRoom.TabIndex = 26;
            this.labelRoom.Text = "username:";
            this.labelRoom.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1905, 714);
            this.Controls.Add(this.labelRoom);
            this.Controls.Add(this.buttonJoin);
            this.Controls.Add(this.textBoxRoom);
            this.Controls.Add(this.sellBox);
            this.Controls.Add(this.buttonResign);
            this.Controls.Add(this.buttonEnd);
            this.Controls.Add(this.labelPositions);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.labelWallet);
            this.Controls.Add(this.labelChat);
            this.Controls.Add(this.buttonSell);
            this.Controls.Add(this.buttonBuy);
            this.Controls.Add(this.positionBox);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.chatBox);
            this.Controls.Add(this.walletBox);
            this.Controls.Add(this.actionBox);
            this.Controls.Add(this.buttonRegister);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.labelPassword);
            this.Controls.Add(this.labelLogin);
            this.Controls.Add(this.textBoxPassword);
            this.Controls.Add(this.textBoxLogin);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label labelLogin;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.RichTextBox actionBox;
        private System.Windows.Forms.RichTextBox walletBox;
        private System.Windows.Forms.RichTextBox chatBox;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.RichTextBox positionBox;
        private System.Windows.Forms.Button buttonBuy;
        private System.Windows.Forms.Button buttonSell;
        private System.Windows.Forms.Label labelChat;
        private System.Windows.Forms.Label labelWallet;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.Label labelPositions;
        private System.Windows.Forms.Button buttonEnd;
        private System.Windows.Forms.Button buttonResign;
        private System.Windows.Forms.TextBox sellBox;
        private System.Windows.Forms.TextBox textBoxRoom;
        private System.Windows.Forms.Button buttonJoin;
        private System.Windows.Forms.Label labelRoom;
    }
}

