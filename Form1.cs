using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BCNColouringBookLogoAdder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string WebSite = "";
        private static Random random = new Random();

        private void button1_Click(object sender, EventArgs e)
        {
            if (WebSite.Length > 0)
            {
                Resize();
                string filepath = Directory.GetCurrentDirectory();
                DirectoryInfo d = new DirectoryInfo(filepath);
                string output = "";
                foreach (var file in d.GetFiles("*.jpg"))
                {
                    using (Image image = Image.FromFile(file.ToString()))
                    using (Image watermarkImage = Image.FromFile(@"Resized.png"))
                    using (Graphics imageGraphics = Graphics.FromImage(image))
                    using (TextureBrush watermarkBrush = new TextureBrush(watermarkImage))
                    {
                        int x = 20;
                        int y = 20;
                        watermarkBrush.TranslateTransform(x, y);
                        imageGraphics.FillRectangle(watermarkBrush, new Rectangle(new Point(x, y), new Size(300, 280)));
                        using (Font arialFont = new Font("Arial", 20))
                        {
                            PointF Location = new PointF(100f, 1000f);
                            imageGraphics.DrawString(WebSite, arialFont, Brushes.Blue, Location);
                        }

                        output = file.ToString();
                        output = "Watermarked_" + output;
                        image.Save(output);
                    }
                }
            }
            else
            {
                label3.Text = "Enter your website address first.";
            }

        }

        public void Resize()
        {
            using (var srcImage = Image.FromFile(@"watermark.png"))
            {
                var newWidth = (int)(300);
                var newHeight = (int)(280);
                using (var newImage = new Bitmap(newWidth, newHeight))
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                    newImage.Save(@"Resized.png");
                }
            }
        }


        private void WebSiteText_TextChanged(object sender, EventArgs e)
        {
            WebSite = WebSiteText.Text.ToString();
        }
    }
}
