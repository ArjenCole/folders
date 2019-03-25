using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Folders
{
    public partial class FmCover : Form
    {
        private bool show100 = false;

        public FmCover()
        {
            InitializeComponent();
        }

        private void FmCover_Load(object sender, EventArgs e)
        {
            
            this.Opacity = 0;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Text = this.Opacity.ToString();
            if (mscCtrl.fmMainStarted && this.show100) 
            {
                this.Opacity -= 0.03;
                if (this.Opacity <= 0)
                {
                    this.Close();
                }
            }
            else
            {
                this.Opacity += 0.08;
                if (this.Opacity >= 1)
                {
                    this.show100 = true;
                    //Thread.Sleep(500);
                }
            }


        }



    }
}
