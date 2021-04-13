using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisMessenger
{
    public partial class LogInForm : Form
    {
        public LogInForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.No;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            User u = DBHelper.GetUserByLoginPassword(tbLogin.Text, tbPassword.Text);
            if(u == null)
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.Yes;
            Program.CurrentUser = u;
            Close();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if(new CreateAccountForm().ShowDialog() == DialogResult.Yes)
            {
                DialogResult = DialogResult.Yes;
                Close();
            }
        }
    }
}
