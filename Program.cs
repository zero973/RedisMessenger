using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisMessenger
{
    static class Program
    {

        public static User CurrentUser;

        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CurrentUser = null;
            Application.Run(new MainForm(null));
            DBHelper.DeleteOnlineUser(CurrentUser.Id);
        }
    }
}
