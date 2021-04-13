using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisMessenger
{
    public partial class MainForm : Form
    {

        private User _User;
        private Chat[] Chats;

        private int CurChatId;
        private Control LastChatControl;

        public MainForm(User u)
        {
            InitializeComponent();
            _User = u;
            CurChatId = -1;
        }

        public void OnChatControlClicked(int chatId, Control control)
        {
            if(LastChatControl != null)
                LastChatControl.BackColor = Color.White;
            LastChatControl = control;
            control.BackColor = Color.Aqua;

            CurChatId = chatId;
            LoadMessages(chatId);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_User == null)
            {
                if (new LogInForm().ShowDialog() != DialogResult.Yes)
                {
                    Close();
                    return;
                }
                _User = Program.CurrentUser;
                DBHelper.AddOnlineUser(_User.Id);
                lbNick.Text = _User.Nick;
                pbAvatar.Image = Image.FromStream(new MemoryStream(_User.Avatar));
                UpdateUsersOnline();
                LoadChats("");
                DBHelper.OnAnyAction += DBHelper_OnAnyAction;
            }
        }

        private void DBHelper_OnAnyAction(ActionTypes actionType, int arg)
        {
            switch (actionType)
            {
                case ActionTypes.UserOnline: UpdateUsersOnline(); break;
                case ActionTypes.NewMessage:
                    if (CurChatId == arg)
                        LoadMessages(CurChatId);
                    break;
                case ActionTypes.ChatAdded: LoadChats(tbSearch.Text); break;
            }
        }

        private void UpdateUsersOnline()
        {
            User[] usersOnline = DBHelper.GetOnlineUsers();
            lbUsersOnline.Invoke((MethodInvoker)(() => { lbUsersOnline.Text = $"Пользователей онлайн: {usersOnline.Length}{Environment.NewLine}"; }));
            foreach (User u in usersOnline)
                lbUsersOnline.Invoke((MethodInvoker)(() => { lbUsersOnline.Text += $"{u.Nick}{Environment.NewLine}"; }));
        }

        private void LoadChats(string search)
        {
            ChatsContainer.Invoke((MethodInvoker)(() => { ChatsContainer.Controls.Clear(); }));
            Chats = DBHelper.GetChats();
            foreach (Chat c in Chats)
                if (c.Name.Contains(search))
                    ChatsContainer.Invoke((MethodInvoker)(() => { ChatsContainer.Controls.Add(new ChatControl(c, this)); }));
        }

        private void LoadMessages(int chatId)
        {
            MessagesContainer.Invoke((MethodInvoker)(() => { MessagesContainer.Controls.Clear(); }));
            foreach (Message m in DBHelper.GetMessages(chatId))
                 MessagesContainer.Invoke((MethodInvoker)(() => { MessagesContainer.Controls.Add(new MessageControl(m)); }));
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (CurChatId < 0)
            {
                MessageBox.Show("Вы не выбрали чат, в который будет отправлено сообщение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DBHelper.AddMessage(new Message(0, GetCurDate(), CurChatId, _User.Id, tbMessage.Text));
        }

        private void btnFindChat_Click(object sender, EventArgs e)
        {
            LoadChats(tbSearch.Text);
        }

        private void btnCreateChat_Click(object sender, EventArgs e)
        {
            new CreateChatForm(_User.Id).ShowDialog();
        }

        private string GetCurDate()
        {
            DateTime T = DateTime.Now;
            return $"{T.Day}.{T.Month}.{T.Year} {T.Hour}:{T.Minute}";
        }

        private void SystemTrayIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
        }

        private void SettingsMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm(_User).ShowDialog();
            _User = Program.CurrentUser;
            lbNick.Text = _User.Nick;
            pbAvatar.Image = Image.FromStream(new MemoryStream(_User.Avatar));
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pbSettings_Click(object sender, EventArgs e)
        {
            new SettingsForm(_User).ShowDialog();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DBHelper.OnAnyAction -= DBHelper_OnAnyAction;
        }
    }
}
