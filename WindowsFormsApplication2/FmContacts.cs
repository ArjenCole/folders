using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Folders
{
    public partial class FmContacts : Form
    {
        public ProjectDetails Pdetails = null;
        private string currentXMLpath;

        public FmContacts(string pXMLpath)
        {
            InitializeComponent();
            currentXMLpath = pXMLpath;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn[] c = new DataGridViewTextBoxColumn[6];
            string[] headertxt = { "姓名", "手机", "固定电话", "部门", "专业", "备注" };
            for(int i=0; i<=5;i++)
            {
                c[i] = new DataGridViewTextBoxColumn();
                c[i].HeaderText = headertxt[i];
                dataGridView1.Columns.Add(c[i]);
            }
            
            foreach(string tmp_str in Pdetails.Contacts)
            {
                string[] sArray = Regex.Split(tmp_str, ",", RegexOptions.IgnoreCase);

                int index = dataGridView1.Rows.Add();
                for(int i=0;i<=5;i++)
                {
                    dataGridView1.Rows[index].Cells[i].Value = sArray[i];
                }
                
            }
            //c1.Width = 70;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Pdetails.Contacts = null;
            List<string> contacts = new List<string>();
            foreach(DataGridViewRow r in dataGridView1.Rows)
            {
                string tmp_str = "";
                for (int i = 0; i <= 5; i++)
                {
                    string v = "";
                    if (r.Cells[i].Value!=null) {v = r.Cells[i].Value.ToString();}
                    tmp_str += v + ",";
                }
                tmp_str = tmp_str.Remove(tmp_str.Length - 1, 1);
                if (tmp_str.Replace(",", "") != "")
                { contacts.Add(tmp_str); }
            }
            Pdetails.Contacts = contacts.ToArray();

            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = currentXMLpath;
            XmlFile.WriteXml(Pdetails);
            XmlFile.SaveXml();

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
