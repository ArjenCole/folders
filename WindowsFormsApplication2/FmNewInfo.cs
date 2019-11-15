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
    public partial class FmNewInfo : Form
    {
        public ProjectDetails Pdetails = new ProjectDetails();
        private string xmlPath;

        public enum FormType_enum
        {
            NEW=0,
            EDIT=1,
            CREATE=2
        };
        private FormType_enum formType = new FormType_enum();
        

        public FmNewInfo(string pPath ,FormType_enum pType,ProjectDetails pPD)
        {
            InitializeComponent();
            xmlPath = pType == FormType_enum.NEW ? null : pPath;
            formType = pType;
            Pdetails = pPD;
        }

        private void FmNewInfo_Load(object sender, EventArgs e)
        {
            btnOK.Top = btnNew.Top;
            shiftFormStyle();
            SetComboBox();
            if(Pdetails == null) { Pdetails = new ProjectDetails(); }
            checkBox1.Checked = !string.IsNullOrEmpty(Pdetails.Pnote);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") { comboBox2.Items.Clear(); return; }
            comboBox2.Items.Clear();
            //comboBox2.Text = "";
            msc_Tree.SetList(comboBox1.Text, "",comboBox2);
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            getPdetailsFromTexts();

            //DirectoryInfo Dinfo = new DirectoryInfo(@"E:\项目文件\" + Pdetails.FolderName());//创建文件夹地址字符串
            DirectoryInfo Dinfo = new DirectoryInfo(msc_FolderList.defaultPath + @"\" + Pdetails.FolderName());//创建文件夹地址字符串
            Dinfo.Create();//创建文件夹

            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = xmlPath == null ? msc_FolderList.defaultPath + Pdetails.FolderName() + @"\foldersPD.xml" : xmlPath;
            XmlFile.WriteXml(Pdetails);
            XmlFile.SaveXml();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            getPdetailsFromTexts();
            //DirectoryInfo Dinfo = new DirectoryInfo(@"E:\项目文件\" + Pdetails.FolderName());//创建文件夹地址字符串
            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = xmlPath;
            this.Text = Pdetails.Pnote;
            XmlFile.WriteXml(Pdetails);
            this.Text = this.Text + " " + Pdetails.Pnote;
            XmlFile.SaveXml();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                this.Height = 408;
                textBox1.Visible = true;
                textBox1.Text = Pdetails.Pnote;
            }
            else
            {
                this.Height = 256;
                textBox1.Visible = true;
                textBox1.Text = Pdetails.Pnote;
            }
        }
        //==================================自定义函数================================================================
        /// <summary>
        /// 切换窗体类型，NEW/EDIT
        /// </summary>
        private void shiftFormStyle()
        {
            int i = (int)(formType);
            switch (i)
            {
                case (int)(FormType_enum.NEW):
                    btnNew.Visible = true;
                    btnOK.Visible = false;
                    text_Pname.Enabled = true;
                    Pdetails = null;
                    this.Text = "新建项目";
                    break;
                case (int)(FormType_enum.EDIT):
                    btnNew.Visible = false;
                    btnOK.Visible = true;
                    text_Pname.Enabled = true;
                    setTexts();
                    this.Text = "编辑项目信息";
                    break;
                case (int)(FormType_enum.CREATE):
                    btnNew.Visible = false;
                    btnOK.Visible = true;
                    setTexts();
                    this.Text = "创建项目信息";
                    break;
            }
        }
        private void getPdetailsFromTexts()
        {
            if (Pdetails == null) { Pdetails = new ProjectDetails(); }
            Pdetails.Pname = text_Pname.Text;
            Pdetails.Pindex = text_Pindex.Text;
            Pdetails.Pincharge = text_Pincharge.Text;
            Pdetails.Cincharge = text_Cincharge.Text;
            Pdetails.P = msc_FolderList.RemoveSuffix(comboBox1.Text);
            Pdetails.C = msc_FolderList.RemoveSuffix(comboBox2.Text);
            Pdetails.Pdep = comboBox4.Text;
            Pdetails.Pnote = textBox1.Text;
        }
        private void setTexts()
        {
            text_Pname.Text = Pdetails.Pname;
            text_Pindex.Text=Pdetails.Pindex;
            text_Pincharge.Text=Pdetails.Pincharge;
            text_Cincharge.Text = Pdetails.Cincharge;
            comboBox1.Text=Pdetails.P;
            comboBox2.Text=Pdetails.C;
            comboBox4.Text=Pdetails.Pdep;
            textBox1.Text = Pdetails.Pnote;
        }
        private void SetComboBox()
        {

            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;//设置自动完成的模式
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;//设置自动完成的字符串源
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;//设置自动完成的模式
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;//设置自动完成的字符串源

            foreach (mc_Tree tmp_tree in msc_Tree.S.son)
            {
                if (!(string.IsNullOrEmpty(tmp_tree.Name)))
                {
                    comboBox1.Items.Add(tmp_tree.Name);
                }
            }
            //comboBox1.Text = "";

            string[] department = { "一院", "二院", "三院", "四院", "五院", "六院", "交规院", "道桥院", "大桥院", "景观院", "建筑院", "综合院", "地下院", "交通院" };
            foreach (string str in department)
            {
                comboBox4.Items.Add(str);
            }

            comboBox4.AutoCompleteMode = AutoCompleteMode.Suggest;//设置自动完成的模式
            comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;//设置自动完成的字符串源

        }
    }
}
