
using System;

namespace RedisMessenger
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lbUsersOnline = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btnFindChat = new System.Windows.Forms.Button();
            this.ChatsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.MessagesContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SystemTrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.PopUpMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCreateChat = new System.Windows.Forms.Button();
            this.pbAvatar = new System.Windows.Forms.PictureBox();
            this.lbNick = new System.Windows.Forms.Label();
            this.pbSettings = new System.Windows.Forms.PictureBox();
            this.PopUpMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUsersOnline
            // 
            this.lbUsersOnline.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUsersOnline.Location = new System.Drawing.Point(592, 67);
            this.lbUsersOnline.Name = "lbUsersOnline";
            this.lbUsersOnline.Size = new System.Drawing.Size(187, 424);
            this.lbUsersOnline.TabIndex = 19;
            this.lbUsersOnline.Text = "Пользователей онлайн: 1";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(484, 464);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(90, 27);
            this.btnSend.TabIndex = 16;
            this.btnSend.Text = "Отправить";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // tbMessage
            // 
            this.tbMessage.Location = new System.Drawing.Point(298, 467);
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(180, 23);
            this.tbMessage.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(252, 470);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "Текст:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 15);
            this.label2.TabIndex = 12;
            this.label2.Text = "Сообщения:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Чат:";
            // 
            // tbSearch
            // 
            this.tbSearch.Location = new System.Drawing.Point(47, 7);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(120, 23);
            this.tbSearch.TabIndex = 21;
            // 
            // btnFindChat
            // 
            this.btnFindChat.Location = new System.Drawing.Point(173, 5);
            this.btnFindChat.Name = "btnFindChat";
            this.btnFindChat.Size = new System.Drawing.Size(60, 25);
            this.btnFindChat.TabIndex = 22;
            this.btnFindChat.Text = "Найти";
            this.btnFindChat.UseVisualStyleBackColor = true;
            this.btnFindChat.Click += new System.EventHandler(this.btnFindChat_Click);
            // 
            // ChatsContainer
            // 
            this.ChatsContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChatsContainer.Location = new System.Drawing.Point(12, 67);
            this.ChatsContainer.Name = "ChatsContainer";
            this.ChatsContainer.Size = new System.Drawing.Size(221, 423);
            this.ChatsContainer.TabIndex = 23;
            // 
            // MessagesContainer
            // 
            this.MessagesContainer.AutoScroll = true;
            this.MessagesContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MessagesContainer.Location = new System.Drawing.Point(252, 35);
            this.MessagesContainer.Name = "MessagesContainer";
            this.MessagesContainer.Size = new System.Drawing.Size(322, 423);
            this.MessagesContainer.TabIndex = 24;
            // 
            // SystemTrayIcon
            // 
            this.SystemTrayIcon.ContextMenuStrip = this.PopUpMenu;
            this.SystemTrayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("SystemTrayIcon.Icon")));
            this.SystemTrayIcon.Text = "Redis Messenger";
            this.SystemTrayIcon.Visible = true;
            this.SystemTrayIcon.DoubleClick += new System.EventHandler(this.SystemTrayIcon_DoubleClick);
            // 
            // PopUpMenu
            // 
            this.PopUpMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SettingsMenuItem,
            this.ExitMenuItem});
            this.PopUpMenu.Name = "PopUpMenu";
            this.PopUpMenu.Size = new System.Drawing.Size(135, 48);
            // 
            // SettingsMenuItem
            // 
            this.SettingsMenuItem.Name = "SettingsMenuItem";
            this.SettingsMenuItem.Size = new System.Drawing.Size(134, 22);
            this.SettingsMenuItem.Text = "Настройки";
            this.SettingsMenuItem.Click += new System.EventHandler(this.SettingsMenuItem_Click);
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(134, 22);
            this.ExitMenuItem.Text = "Выход";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // btnCreateChat
            // 
            this.btnCreateChat.Location = new System.Drawing.Point(12, 36);
            this.btnCreateChat.Name = "btnCreateChat";
            this.btnCreateChat.Size = new System.Drawing.Size(221, 25);
            this.btnCreateChat.TabIndex = 25;
            this.btnCreateChat.Text = "Создать новый чат";
            this.btnCreateChat.UseVisualStyleBackColor = true;
            this.btnCreateChat.Click += new System.EventHandler(this.btnCreateChat_Click);
            // 
            // pbAvatar
            // 
            this.pbAvatar.Location = new System.Drawing.Point(592, 7);
            this.pbAvatar.Name = "pbAvatar";
            this.pbAvatar.Size = new System.Drawing.Size(50, 50);
            this.pbAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAvatar.TabIndex = 26;
            this.pbAvatar.TabStop = false;
            // 
            // lbNick
            // 
            this.lbNick.Location = new System.Drawing.Point(648, 10);
            this.lbNick.Name = "lbNick";
            this.lbNick.Size = new System.Drawing.Size(105, 47);
            this.lbNick.TabIndex = 27;
            this.lbNick.Text = "Никнейм";
            // 
            // pbSettings
            // 
            this.pbSettings.Image = ((System.Drawing.Image)(resources.GetObject("pbSettings.Image")));
            this.pbSettings.Location = new System.Drawing.Point(759, 7);
            this.pbSettings.Name = "pbSettings";
            this.pbSettings.Size = new System.Drawing.Size(20, 20);
            this.pbSettings.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSettings.TabIndex = 28;
            this.pbSettings.TabStop = false;
            this.pbSettings.Click += new System.EventHandler(this.pbSettings_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 503);
            this.Controls.Add(this.pbSettings);
            this.Controls.Add(this.lbNick);
            this.Controls.Add(this.pbAvatar);
            this.Controls.Add(this.btnCreateChat);
            this.Controls.Add(this.MessagesContainer);
            this.Controls.Add(this.ChatsContainer);
            this.Controls.Add(this.btnFindChat);
            this.Controls.Add(this.tbSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbUsersOnline);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Redis Messenger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.PopUpMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSettings)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUsersOnline;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox tbMessage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.Button btnFindChat;
        private System.Windows.Forms.FlowLayoutPanel ChatsContainer;
        private System.Windows.Forms.FlowLayoutPanel MessagesContainer;
        private System.Windows.Forms.NotifyIcon SystemTrayIcon;
        private System.Windows.Forms.ContextMenuStrip PopUpMenu;
        private System.Windows.Forms.ToolStripMenuItem SettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.Button btnCreateChat;
        private System.Windows.Forms.PictureBox pbAvatar;
        private System.Windows.Forms.Label lbNick;
        private System.Windows.Forms.PictureBox pbSettings;
    }
}

