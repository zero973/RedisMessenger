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
    public partial class ChatControl : UserControl
    {

        private Chat _Chat;
        private MainForm _MainForm;

        public ChatControl(Chat chat, MainForm m)
        {
            InitializeComponent();
            _Chat = chat;
            _MainForm = m;
            lbChatName.Text = chat.Name;
            pbAvatar.Image = Image.FromStream(new MemoryStream(chat.Avatar));
        }

        private void ChatControl_Click(object sender, EventArgs e)
        {
            _MainForm.OnChatControlClicked(_Chat.Id, this);
        }

    }
}
