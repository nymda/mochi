using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mocchi
{
    public partial class encoding : Form
    {
        private Form1 f1;

        public encoding()
        {
            InitializeComponent();
        }

        public int Pixelsdrawn
        {
            get
            {
                return this.Pixelsdrawn;
            }
            set
            {
                this.Pixelsdrawn = value;
            }
        }
        public int Pixelstotal
        {
            get
            {
                return this.Pixelstotal;
            }
            set
            {
                this.Pixelstotal = value;
            }
        }

        public void updateBar(int totalpix, int drawnpix)
        {
            int percentage = (drawnpix / (totalpix / 100));
            this.Invoke(new MethodInvoker(delegate ()
            {
                progressBar1.Value = percentage;
            }));
        }

        private void encoding_Load(object sender, EventArgs e)
        {

            
        }
    }
}
