using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace RedisMessenger
{
    public partial class CreateChatForm : Form
    {

        private int UserId;
        private OpenFileDialog FileDialog;

        public CreateChatForm(int userId)
        {
            InitializeComponent();
            UserId = userId;
            FileDialog = new OpenFileDialog();
            FileDialog.Filter = "PNG images(*.png)|*.png";
            FileDialog.Multiselect = false;
        }

        private void btnChooseAvatar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Изображение должно быть 100x100 и в формате PNG", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            FileDialog.Title = "Выбор изображения";
            if (FileDialog.ShowDialog() == DialogResult.OK)
                pbAvatar.Image = ResizeImage(Image.FromFile(FileDialog.FileName), 100, 100);
        }

        private void btnCreateChat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Я надеюсь, вы заполнили все поля, инчае будет отвал башки. Продолжить выполнение?",
                "Уведомление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            DBHelper.AddChat(new Chat(0, tbName.Text, ImageToByteArray(pbAvatar.Image)));
            MessageBox.Show("Чат успешно создан", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
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
