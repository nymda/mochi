using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mocchi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public byte[] data;
        public byte[] bufferdata;
        public string filePath;
        public List<int> percentages = new List<int> { };
        public Form encoding = new encoding();

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    data = File.ReadAllBytes(filePath);
                    bufferdata = data;
                    MemoryStream ms = new MemoryStream(data);
                    pictureBox1.Image = Image.FromStream(ms);
                    Image representation = new Bitmap(100, 100);
                    Graphics g = Graphics.FromImage(representation);
                    g.FillRectangle(Brushes.White, 0, 0, 100, 100);
                    pictureBox2.Image = representation;
                    numericUpDown1.Maximum = data.Length;
                }
            }
        }

        public int appears(int value)
        {
            int counter = 0;
            for (int y = 0; y < percentages.Count; y++)
            {
                if (percentages[y] == value)
                {
                    counter++;
                }
            }

            return counter;
        }
        public static Color Rainbow(float progress)
        {
            progress = progress / 10;
            float div = (Math.Abs(progress % 1) * 6);
            int ascending = (int)((div % 1) * 255);
            int descending = 255 - ascending;

            switch ((int)div)
            {
                case 0:
                    return Color.FromArgb(255, 255, ascending, 0);
                case 1:
                    return Color.FromArgb(255, descending, 255, 0);
                case 2:
                    return Color.FromArgb(255, 0, 255, ascending);
                case 3:
                    return Color.FromArgb(255, 0, descending, 255);
                case 4:
                    return Color.FromArgb(255, ascending, 0, 255);
                default: // case 5:
                    return Color.FromArgb(255, 255, 0, descending);
            }
        }

        public void glitchImage()
        {
            Random rnd = new Random();
            byte[] b = new byte[1];
            int location;
            rnd.NextBytes(b);
            location = rnd.Next((int)numericUpDown1.Value, bufferdata.Length);
            int percentage = (location / (bufferdata.Length / 100));
            Image representation = pictureBox2.Image;
            Graphics g = Graphics.FromImage(representation);
            g.DrawLine(new Pen(Rainbow(appears(percentage))), percentage, 100, percentage, 0);
            percentages.Add(percentage);
            pictureBox2.Image = representation;
            bufferdata[location] = b[0];
            try
            {
                MemoryStream ms = new MemoryStream(bufferdata);
                pictureBox1.Image = Image.FromStream(ms);
            }
            catch
            {
                try
                {
                    pictureBox1.Dispose();
                    MemoryStream ms2 = new MemoryStream(data);
                    pictureBox1.Image = Image.FromStream(ms2);
                }
                catch
                {

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            glitchImage();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            glitchImage();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            timer1.Interval = (int)numericUpDown2.Value;
        }

        public void encodeAndSave(string filename)
        {
            Bitmap fresh = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            Graphics gr = Graphics.FromImage(fresh);

            for (int i = 0; i < pictureBox1.Image.Width; i++)
            {
                for (int o = 0; o < pictureBox1.Image.Height; o++)
                {
                    Bitmap n = (Bitmap)pictureBox1.Image;
                    Color p = n.GetPixel(i, o);
                    SolidBrush b = new SolidBrush(p);
                    gr.FillRectangle(b, i, o, 1, 1);
                }
            }

            this.Invoke(new MethodInvoker(delegate ()
            {
                encoding.Close();
            }));

            fresh.Save(filename);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Png files | *.png";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                encoding.Show();
                Thread a = new Thread(() => encodeAndSave(saveFileDialog1.FileName));
                a.IsBackground = true;
                a.Start();
            }
        }
    }
}
