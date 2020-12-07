using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AverageColor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void getImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений|*.bmp;*.png;*.jpg";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                Photo.bitmap = (Bitmap)Image.FromFile(openFileDialog.FileName);
                Photo.imgAdress = openFileDialog.FileName;

                pictureBox1.BackgroundImage = Photo.bitmap;
                pictureBox1.Size = Photo.bitmap.Size;
                panel1.AutoScroll = true;
                panel1.AutoScrollMinSize = Photo.bitmap.Size;
                panel1.Invalidate();
            }
            catch
            {

            }
        }

        private void modeButton_Click(object sender, EventArgs e)
        {
            if(Photo.bitmap != null)
                panel2.BackColor = Photo.getModeColor(Photo.bitmap, 20);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
