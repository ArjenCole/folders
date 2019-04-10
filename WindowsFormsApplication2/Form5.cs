using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Folders
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            foreach(string fe_str in msc_FolderList.pList)
            {
                listView1.Items.Add(fe_str);
            }
            lblDefaultPath.Text = msc_FolderList.defaultPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                if(!listviewContain(folderBrowserDialog1.SelectedPath))
                {
                    listView1.Items.Add(folderBrowserDialog1.SelectedPath);
                }   
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                DirectoryInfo dir = new DirectoryInfo(folderBrowserDialog1.SelectedPath);//新建字典信息变量
                FileSystemInfo[] f = dir.GetFileSystemInfos();//读取文件信息
                foreach (FileSystemInfo fe_FSI in f)//遍历文件
                {
                    if ((fe_FSI is DirectoryInfo)&& (!listviewContain(lastChar(folderBrowserDialog1.SelectedPath)+ fe_FSI.Name)))//如果为文件夹
                    {
                        listView1.Items.Add(lastChar(folderBrowserDialog1.SelectedPath)  + fe_FSI.Name);
                    }
                }
            }
        }
        private bool listviewContain(string para_str)
        {
            foreach (ListViewItem fe_LVI in listView1.Items)
            {
                if (fe_LVI.Text==para_str)
                {
                    return true;
                }
            }
            return false;
        }
        private string lastChar(string para_str)
        {
            return para_str.EndsWith(@"\") ? para_str :( para_str + @"\");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem fe_LVI in listView1.SelectedItems)
            {
                fe_LVI.Remove();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> tmp_List = new List<string>();
            foreach (ListViewItem fe_LVI in listView1.Items)
            {
                tmp_List.Add(fe_LVI.Text);
            }
            if (tmp_List.Count == 0)
            {
                MessageBox.Show("文件地址列表不可为空", "提示", MessageBoxButtons.OK);
                return;
            }
            msc_FolderList.pList_fill(tmp_List);
            msc_FolderList.defaultPath = tmp_List.Contains(lblDefaultPath.Text) ? lblDefaultPath.Text : tmp_List[0];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button6.Enabled = listView1.SelectedItems.Count == 1? true :false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            lblDefaultPath.Text = lastChar(listView1.SelectedItems[0].SubItems[0].Text);
            button6.Enabled = false;
        }
    }
}
