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
    public partial class MessageControl : UserControl
    {
        public MessageControl(Message m)
        {
            InitializeComponent();
            User u = DBHelper.GetUserById(m.UserId);
            lbMessage.Text = m.Text;
            lbDate.Text = m.Date;
            lbNick.Text = u.Nick;
            pbAvatar.Image = Image.FromStream(new MemoryStream(u.Avatar));
        }
    }
}
