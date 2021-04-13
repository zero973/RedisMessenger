using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RedisMessenger
{
    public partial class CreateAccountForm : Form
    {

        private int Code;
        private OpenFileDialog FileDialog;

        public CreateAccountForm()
        {
            InitializeComponent();
            DialogResult = DialogResult.No;
            Code = new Random().Next(1000, 9999);
            FileDialog = new OpenFileDialog();
            FileDialog.Filter = "PNG images(*.png)|*.png";
            FileDialog.Multiselect = false;
        }

        private async void btnCheckEmail_Click(object sender, EventArgs e)
        {
            if (DBHelper.IsEmailExists(tbEmail.Text))
            {
                MessageBox.Show("Эта почта уже используется другим пользователем. Введите другую почту.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                await SendEmail($"Ваш проверочный код: {Code}", tbEmail.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Изображение должно быть 100x100 и в формате PNG", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FileDialog.Title = "Выбор изображения";
            if(FileDialog.ShowDialog() == DialogResult.OK)
                pbAvatar.Image = ResizeImage(Image.FromFile(FileDialog.FileName), 100, 100);
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Я надеюсь, вы заполнили все поля, инчае будет отвал башки. Продолжить выполнение?", 
                "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (DBHelper.IsUserExists(tbLogin.Text))
            {
                MessageBox.Show("Пользователь с таким логином уже существует. Придумайте другой логин.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (Code + "" == tbCode.Text || tbCode.Text == "1") //это чтобы часто на почту сообщение не отправлять
            {
                Program.CurrentUser = DBHelper.GetUserById(DBHelper.AddUser(new User(0, tbLogin.Text, tbPassword.Text,
                    tbNick.Text, tbEmail.Text, ImageToByteArray(pbAvatar.Image))));
                DialogResult = DialogResult.Yes;
                Close();
            }
            else
                MessageBox.Show("Неверный код", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static async Task SendEmail(string text, string email)
        {
            MailAddress from = new MailAddress("zerostore@mail.ru", "ZeroStore");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Проверка почты";
            m.Body = text;
            SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
            smtp.Credentials = new NetworkCredential("zerostore@mail.ru", "Qawsedrf_321");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
            MessageBox.Show("На указанный Email отправлено письмо с проверочным кодом. Пожалуйста, проверьте почту и введите проверочный код в нижнее поле.", "Уведомление");
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

    }
}
