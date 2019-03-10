using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Folders
{
    
    public partial class Form1 : Form
    {
        ReadExcel_Class edr = new ReadExcel_Class();
        public ProjectDetails pd = null;
        
        static string currentFolder = "";
        static int currentRowIndex
        {
            get
            {
                int index = msc_FolderList.fList_s.FindIndex(t => t.Path + t.Name == currentFolder);
                return index;
            }
        }
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int x = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Width;
            int y = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Size.Height;
            this.Location = new Point((x-this.Width)/2, (y-this.Height)/2);
            string str = System.Environment.CurrentDirectory;//获得运行文件的存储路径
            edr.read(str + "\\2015年全国城市省市县区行政级别对照表-(最新最全).xls", "全国城市省市县区域列表");
            /*
            Control.CheckForIllegalCrossThreadCalls = false;//关闭跨线程安全检查
            Application.OpenForms["Form_Start"].Close();//关闭封面窗体
            Control.CheckForIllegalCrossThreadCalls=true;//打开跨线程安全检查
            */
            File.Delete(str + @"\cover.ini");
            msc_Tree.fill(edr.Cell);
            SetComboBox();
            iniRead();
            this.Activate();
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            iniSave();
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "") {comboBox2.Items.Clear(); comboBox3.Items.Clear(); return; }
            comboBox2.Items.Clear();
            comboBox2.Text = "";
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            
            msc_Tree.SetList(comboBox1.Text,"",comboBox2);
        }
        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "") {comboBox3.Items.Clear(); return; }
            comboBox3.Items.Clear();
            comboBox3.Text = "";
            
            msc_Tree.SetList(comboBox1.Text,comboBox2.Text,comboBox3);
        }
        private void button_search_Click(object sender, EventArgs e)
        {
            TimeFilterToolStripMenuItem_Click(全部ToolStripMenuItem, e);
            List<string> nc =new List<string>();
            if (comboBox1.Text != "") { nc.Add(comboBox1.Text); }
            if (comboBox2.Text != "") { nc.Add(comboBox2.Text); }
            if (comboBox3.Text != "") { nc.Add(comboBox3.Text); }
            if (textBox1.Text != "") { nc.Add(textBox1.Text); }
            msc_FolderList.cList_fill(nc);
            ShowOnListview();
            
        }//点击搜索
        private void button_reset_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox1.Text = "";
            button_search_Click(sender,e);
        }
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int c = listView1.SelectedIndices[0];//获得选中项的索引
                string tmp_str = listView1.Items[c].SubItems[1].Text + listView1.Items[c].Text;//组装绝对地址
                currentFolder = tmp_str;
                System.Diagnostics.Process.Start("explorer.exe", tmp_str);//打开双击文件夹 
            }
            catch
            {
                MessageBox.Show("未选择项目", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }//打开双击文件夹 
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //在此处设断点，发现点击不同的Item后，此事件居然执行了2次 
            //第一次是取消当前Item选中状态，导致整个ListView的SelectedIndices变为 空集
            //第二次才将新选中的Item设置为选中状态，SelectedIndices变为 非空集
            //如果不加listview.SelectedIndices.Count>0判断，将导致获取listview.Items[]索引超界的异常
            if (listView1.SelectedIndices.Count > 0)
            {
                int c = listView1.SelectedIndices[0];//获得选中项的索引
                currentFolder = listView1.Items[c].SubItems[1].Text + listView1.Items[c].Text;//组装绝对地址

                int pos_int = listView1.Items[currentRowIndex].Text.LastIndexOf("】");
                string tmp_str = listView1.Items[currentRowIndex].SubItems[1].Text + listView1.Items[currentRowIndex].Text +
                    @"\" + listView1.Items[currentRowIndex].Text.Remove(0,pos_int+1) + ".xml";//组装XML文件绝对地址
                if (File.Exists(tmp_str))
                {
                    XML_Class x = new XML_Class();
                    pd= x.ReadXml(tmp_str);
                }
                else
                {
                    pd = null;
                }
                if(pd !=null)
                {
                    tmp_str = listView1.Items[currentRowIndex].Text.ToString();
                    tmp_str = pd.FolderName(tmp_str);
                }
                ShowDetailsOnDV(pd);
            }
        }
        
        #region listView失去焦点后仍旧保持选中行高亮
        private void listView1_Validated(object sender, EventArgs e)
        {
            /*
            if (currentRowIndex != -1)
            {
                listView1.Items[currentRowIndex].Selected=true;
                listView1.Items[currentRowIndex].Focused = true;
                listView1.FocusedItem.BackColor = SystemColors.Highlight;
                listView1.FocusedItem.ForeColor = Color.White;
            }
            */
        }
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            /*
            foreach(ListViewItem lvi in listView1.Items)
            {
                lvi.ForeColor = Color.Black;
                lvi.BackColor = SystemColors.Window;
            }
            */
        }
        #endregion
        
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0) { listView1.Focus(); return; }
            switch (dataGridView1.CurrentCell.RowIndex)
            {
                case 0:
                    pd.Pname = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 1:
                    pd.Pindex = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 2:
                    pd.P = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 3:
                    pd.C = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 4:
                    pd.Pincharge = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 5:
                    pd.Cincharge = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 6:
                    pd.Pdep= dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 7:
                    pd.Pnote = dataGridView1.CurrentCell.Value.ToString();
                    break;
            }

            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = @"E:\项目文件\" + pd.FolderName(1) + @"\" + pd.Pname + @".xml";
            XmlFile.WriteXml(pd);
            XmlFile.SaveXml();
            listView1.Focus();
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            编辑项目信息ToolStripMenuItem_Click(sender, e);
        }
        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCell.Value == null) { return; }
            try
            {
                string tmp=pd.Contacts[dataGridView2.CurrentCell.RowIndex];
            }
            catch
            {
                List<string> contacts = new List<string>();
                foreach(string str in pd.Contacts)
                {
                    contacts.Add(str);
                }
                contacts.Add(",,,,,");
                pd.Contacts = contacts.ToArray();
            }

            string tmp_str = pd.Contacts[dataGridView2.CurrentCell.RowIndex];
            string[] sArray = Regex.Split(tmp_str, ",", RegexOptions.IgnoreCase);
            sArray[dataGridView2.CurrentCell.ColumnIndex] = dataGridView2.CurrentCell.Value.ToString();
            tmp_str = "";
            for (int i = 0; i <= sArray.GetUpperBound(0); i++)
            {
                tmp_str += sArray[i] + ",";
            }
            pd.Contacts[dataGridView2.CurrentCell.RowIndex] = tmp_str.Remove(tmp_str.Length - 1, 1);

            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = @"E:\项目文件\" + pd.FolderName(1) + @"\" +pd.Pname + @".xml";
            XmlFile.WriteXml(pd);
            XmlFile.SaveXml();
            listView1.Focus();
        }
        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView2.Rows.Count <= 0) return;
            try
            {
                string[] sArray = Regex.Split(pd.Contacts[e.RowIndex], ",", RegexOptions.IgnoreCase);
                string[] headertxt = { "姓名", "手机", "固定电话", "部门", "专业", "备注" };
                string tmp_str ="";
                for (int i = 0; i <= headertxt.GetUpperBound(0); i++)
                {
                    tmp_str = tmp_str+ string.Format("{0}:{1}\r\n", headertxt[i], sArray[i]);//\r\n换行
                }
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = tmp_str;
            }
            catch
            {
                dataGridView2.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = "";
            }

        }
        private void TimeFilterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            一个月ToolStripMenuItem.Checked = false;
            三个月ToolStripMenuItem.Checked = false;
            六个月ToolStripMenuItem.Checked = false;
            全部ToolStripMenuItem.Checked = false;
            ToolStripMenuItem tmp_TSMI = (ToolStripMenuItem)sender;
            tmp_TSMI.Checked = true;
            switch (tmp_TSMI.Name)
            {
                case "一个月ToolStripMenuItem":
                    msc_FolderList.timeFilter = 1;
                    break;
                case "三个月ToolStripMenuItem":
                    msc_FolderList.timeFilter = 3;
                    break;
                case "六个月ToolStripMenuItem":
                    msc_FolderList.timeFilter = 6;
                    break;
                case "全部ToolStripMenuItem":
                    msc_FolderList.timeFilter = -1;
                    break;
            }
            msc_FolderList.search();
            ShowOnListview();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int c = listView1.SelectedIndices[0];//获得选中项的索引
                currentFolder = listView1.Items[c].SubItems[1].Text + listView1.Items[c].Text;//组装绝对地址
                pd.FolderName(listView1.Items[currentRowIndex].Text);
                if ((pd == null) || (pd.Contacts == null)) { return; }
                Form4 form4 = new Form4();
                form4.Pdetails = pd;
                form4.ShowDialog();
                listView1.Focus();
                ShowOnListview();
                listView1.Items[currentRowIndex].Selected = true;//设定选中
            }
            catch
            {
                MessageBox.Show("未选择项目或项目未建立项目信息文件。", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #region ======================================菜单栏======================================
        private void 新建项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(OpenForm3(Form3.FormType_enum.NEW) == DialogResult.OK)
            {
                msc_FolderList.flist_flash();
                ShowOnListview();
            }
        }
        private void 编辑项目信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int c = listView1.SelectedIndices[0];//获得选中项的索引
                currentFolder = listView1.Items[c].SubItems[1].Text + listView1.Items[c].Text;//组装绝对地址
                Form3.FormType_enum flag = Form3.FormType_enum.EDIT;
                if (pd == null)
                {
                    pd = new ProjectDetails();
                    
                    int pos_int = listView1.Items[currentRowIndex].Text.LastIndexOf("】");
                    pd.Pname = listView1.Items[currentRowIndex].Text.Remove(0, pos_int + 1);
                    flag = Form3.FormType_enum.CREATE;
                }
                pd.FolderName(listView1.Items[currentRowIndex].Text);
                OpenForm3(flag);
                ShowOnListview();
                listView1.Items[currentRowIndex].Selected = true;//设定选中
            }
            catch
            {
                MessageBox.Show("未选择项目。", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            button1_Click(sender, e);
        }
        private void 设置目录列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            if (form5.ShowDialog() == DialogResult.OK)
            {
                ShowOnListview();
            }
        }


        #endregion
        #region ======================================自定义函数======================================
        /// <summary>
        /// 将edr中数据读入s\p\c并且填入combobox中
        /// </summary>
        private void SetComboBox()
        {

            comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;//设置自动完成的模式
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;//设置自动完成的字符串源
            comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;//设置自动完成的模式
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;//设置自动完成的字符串源
            comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;//设置自动完成的模式
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;//设置自动完成的字符串源
            
            
            foreach(mc_Tree tmp_tree in msc_Tree.S.son)
            {
                if(!(string.IsNullOrEmpty(tmp_tree.Name)))
                {
                    comboBox1.Items.Add(tmp_tree.Name);
                }                
            }
            comboBox1.Text = "";
        }
        /// <summary>
        /// 刷新listview1列表
        /// </summary>
        private void ShowOnListview()
        {
            listView1.Items.Clear();
            foreach(mc_FolderInfoType fe_fit in msc_FolderList.fList_s)
            {
                listView1.Items.Add(fe_fit.Name);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(fe_fit.Path);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(fe_fit.CreationTime);
                listView1.Items[listView1.Items.Count - 1].SubItems.Add(fe_fit.EditTime);
            }
            if(msc_FolderList.timeFilter==-1)
            {
                label6.Text = "共找到" + listView1.Items.Count + "条记录";
            }
            else
            {
                label6.Text = string.Format("最近{0}个月内找到{1}条记录", msc_FolderList.timeFilter.ToString(), listView1.Items.Count.ToString()); 
            }
            
            dataGridView1.DataSource = null;
            dataGridView2.Rows.Clear();
        }
        /// <summary>
        /// 读取ini文件设置参数及地址列表
        /// </summary>
        private void iniRead()
        {
            msc_Ini.mc_ini.filePath = System.Environment.CurrentDirectory + @"\folders.ini";
            msc_FolderList.timeFilter= int.Parse(msc_Ini.mc_ini.getValue("TimeFilter"));

            一个月ToolStripMenuItem.Checked = false;
            三个月ToolStripMenuItem.Checked = false;
            六个月ToolStripMenuItem.Checked = false;
            全部ToolStripMenuItem.Checked = false;
            switch (msc_FolderList.timeFilter)
            {
                case 1:
                    一个月ToolStripMenuItem.Checked = true;
                    break;
                case 3:
                    三个月ToolStripMenuItem.Checked = true;
                    break;
                case 6:
                    六个月ToolStripMenuItem.Checked = true;
                    break;
                case -1:
                    全部ToolStripMenuItem.Checked = true;
                    break;
            }

            List<string> tmp_list_str = new List<string>();
            for (int i = 1; i <= int.Parse(msc_Ini.mc_ini.getValue("PathListCount")); i++)
            {
                tmp_list_str.Add(msc_Ini.mc_ini.getValue("PathList" + i.ToString()));
            }
            msc_FolderList.pList_fill(tmp_list_str);
            ShowOnListview();
        }
        /// <summary>
        /// 保存参数及地址列表到ini文件
        /// </summary>
        private void iniSave()
        {
            msc_Ini.mc_ini.setValue("TimeFilter", msc_FolderList.timeFilter.ToString());
            msc_Ini.mc_ini.setValue("PathListCount", msc_FolderList.pList.Count.ToString());//地址列表的数量，注意是数量不是最大下标
            for (int i = 1; i <= int.Parse(msc_Ini.mc_ini.getValue("PathListCount")); i++)
            {
                msc_Ini.mc_ini.setValue("PathList" + i.ToString(), msc_FolderList.pList[i - 1]);
            }
        }
        /// <summary>
        /// 显示项目详细信息
        /// </summary>
        /// <param name="pd">XML文件中读取的详细信息数据</param>
        private void ShowDetailsOnDV(ProjectDetails pd)
        {
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();


            if ((pd==null)||(pd.Pname==""))
            {
                dataGridView1.DataSource = null;
            }
            else
            {
                DataGridViewTextBoxColumn c1 = new DataGridViewTextBoxColumn();
                c1.HeaderText = "姓名";
                //c1.Width = 70;
                DataGridViewTextBoxColumn c2 = new DataGridViewTextBoxColumn();
                c2.HeaderText = "联系方式";
                dataGridView2.Columns.Add(c1);
                dataGridView2.Columns.Add(c2);

                //this.Text = pd.Pname;
                dataGridView1.DataSource = new List<Fruit>(){
                    new Fruit() {项="项目名称",值=pd.Pname },
                    new Fruit() {项="项目编号",值=pd.Pindex },
                    new Fruit() {项="项目所在省市",值=pd.P },
                    new Fruit() {项="项目所在城市",值=pd.C },
                    new Fruit() {项="项目负责人",值=pd.Pincharge },
                    new Fruit() {项="专业负责人",值=pd.Cincharge },
                    new Fruit() {项="主体部门",值=pd.Pdep },
                    new Fruit() {项="备注",值=pd.Pnote }
                };
                

                foreach (string tmp_str in pd.Contacts)
                {
                    string[] sArray = Regex.Split(tmp_str, ",", RegexOptions.IgnoreCase);

                    int index = dataGridView2.Rows.Add();
                    dataGridView2.Rows[index].Cells[0].Value = sArray[0];
                    dataGridView2.Rows[index].Cells[1].Value = sArray[1];

                }
            }

        }
        /// <summary>
        /// 用于dataview显示的构造类
        /// </summary>
        private class Fruit
        {
            public string 项 { get; set; }
            public string 值 { get; set; }
        };
        /// <summary>
        /// 设置form3的传递参数并且打开form3
        /// </summary>
        /// <param name="formtype">form3的打开类型，NEW/EDIT</param>
        /// <returns>DialogResult类</returns>
        private DialogResult OpenForm3(Form3.FormType_enum formtype)
        {
            Form3 form3 = new Form3();
            form3.FormType = formtype;
            form3.Pdetails = pd;
            DialogResult r= form3.ShowDialog();
            listView1.Focus();
            return r;
            
        }

        #endregion ======================================自定义函数======================================

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                notifyIcon1.Visible = false;
                this.ShowInTaskbar = true;
            }         
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }
        }
    }
}
