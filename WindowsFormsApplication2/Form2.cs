using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Folders
{
    public partial class Form_Start : Form
    {
        public Form_Start()
        {
            InitializeComponent();
        }

        private void Form_Start_Load(object sender, EventArgs e)
        {
            
            this.Opacity = 0;
            string str = System.Environment.CurrentDirectory;//获得运行文件的存储路径
            
            FileInfo info = new FileInfo(str + @"\cover.ini");
            if (!info.Exists)
            {
                File.Create(str + @"\cover.ini");
            }
            info.Attributes = FileAttributes.Hidden;
            fileSystemWatcher1.Path = str;
            fileSystemWatcher1.Filter = "cover.ini";
            fileSystemWatcher1.EndInit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double i = this.Opacity * 100;
            i=i+5;
            if(i<100)
            {
                this.Opacity = i/100;
            }
            else
            {
                this.Opacity = 100;
                timer1.Enabled = false;
            }
            
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            double i = this.Opacity * 100;
            i = i - 5;
            if (i >0)
            {
                this.Opacity = i/100;
            }
            else
            {
                this.Opacity = 0;
                timer1.Enabled = false;
                this.Close();
            }
        }


        private void fileSystemWatcher1_Deleted(object sender, System.IO.FileSystemEventArgs e)
        {
            timer2.Enabled =true;
        }
    }
}
