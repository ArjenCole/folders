using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Folders
{
    
    public partial class FmMain : Form
    {
        ReadExcel_Class edr = new ReadExcel_Class();
        public ProjectDetails currentPD = null;
        public string currentXMLpath;
        private bool isActived = true;//用于判断窗体是否再次获得焦点，获得后失去焦点隐藏窗体

        private static Dictionary<string, FileSystemWatcher> dicFSW = new Dictionary<string, FileSystemWatcher>();


        static string currentFolder = "";
        static int currentRowIndex
        {
            get
            {
                int index = msc_FolderList.fList_s.FindIndex(t => t.Path + t.Name == currentFolder);
                return index;
            }
        }
        public FmMain()
        {
            //Thread.Sleep(600);
            InitializeComponent();

            string str = System.Environment.CurrentDirectory;//获得运行文件的存储路径
            //edr.read(str + "\\2015年全国城市省市县区行政级别对照表-(最新最全).xls", "全国城市省市县区域列表");
            edr.read(str + "\\2015年全国城市省市县区行政级别对照表-(最新最全).csv");
            msc_Tree.fill(edr.Cell);


        }
        private void FmMain_Load(object sender, EventArgs e)
        {
            iniRead();
            setFileSystemWatcher();
            mscCtrl.fmMainStarted = true;
            this.Activate();
        }
        private void FmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            iniSave();
        }
        private void button_search_Click(object sender, EventArgs e)
        {
            TimeFilterToolStripMenuItem_Click(全部ToolStripMenuItem, e);
            List<string> nc =new List<string>();
            if (textBox1.Text != "")
            {
                nc.Add(textBox1.Text);
                lblTbGrayWord.Visible = false;
            }
            else
            {
                lblTbGrayWord.Visible = true;
            }
            msc_FolderList.cList_fill(nc);
            ShowOnListview();
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
                string XMLpath = listView1.Items[currentRowIndex].SubItems[1].Text + listView1.Items[currentRowIndex].Text +
                        @"\" + listView1.Items[currentRowIndex].Text.Remove(0, pos_int + 1) + ".xml";//组装XML文件绝对地址
                ProjectDetails tPD = mscCtrl.getPD(XMLpath);
                currentPD = tPD;
                currentXMLpath = XMLpath;
                if (tPD == null)
                {
                    XMLpath = listView1.Items[currentRowIndex].SubItems[1].Text + listView1.Items[currentRowIndex].Text +
                        @"\foldersPD.xml";//组装XML文件绝对地址
                    tPD = mscCtrl.getPD(XMLpath);
                    currentPD = tPD;
                    currentXMLpath = XMLpath;

                }
                ShowDetailsOnDV(tPD);
            }
        }
       
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0) { listView1.Focus(); return; }
            switch (dataGridView1.CurrentCell.RowIndex)
            {
                case 0:
                    currentPD.Pname = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 1:
                    currentPD.Pindex = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 2:
                    currentPD.P = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 3:
                    currentPD.C = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 4:
                    currentPD.Pincharge = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 5:
                    currentPD.Cincharge = dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 6:
                    currentPD.Pdep= dataGridView1.CurrentCell.Value.ToString();
                    break;
                case 7:
                    currentPD.Pnote = dataGridView1.CurrentCell.Value.ToString();
                    break;
            }
            msc_FolderList.updatePD(currentPD, currentFolder);
            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = currentXMLpath;
            XmlFile.WriteXml(currentPD);
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
                string tmp=currentPD.Contacts[dataGridView2.CurrentCell.RowIndex];
            }
            catch
            {
                List<string> contacts = new List<string>();
                foreach(string str in currentPD.Contacts)
                {
                    contacts.Add(str);
                }
                contacts.Add(",,,,,");
                currentPD.Contacts = contacts.ToArray();
            }

            string tmp_str = currentPD.Contacts[dataGridView2.CurrentCell.RowIndex];
            string[] sArray = Regex.Split(tmp_str, ",", RegexOptions.IgnoreCase);
            sArray[dataGridView2.CurrentCell.ColumnIndex] = dataGridView2.CurrentCell.Value.ToString();
            tmp_str = "";
            for (int i = 0; i <= sArray.GetUpperBound(0); i++)
            {
                tmp_str += sArray[i] + ",";
            }
            currentPD.Contacts[dataGridView2.CurrentCell.RowIndex] = tmp_str.Remove(tmp_str.Length - 1, 1);

            msc_FolderList.updatePD(currentPD, currentFolder);
            XML_Class XmlFile = new XML_Class();
            XmlFile.FilePath = currentXMLpath;
            XmlFile.WriteXml(currentPD);
            XmlFile.SaveXml();
            listView1.Focus();
        }
        private void dataGridView2_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0 || dataGridView2.Rows.Count <= 0) return;
            try
            {
                string[] sArray = Regex.Split(currentPD.Contacts[e.RowIndex], ",", RegexOptions.IgnoreCase);
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
                currentPD.FolderName(listView1.Items[currentRowIndex].Text);
                if ((currentPD == null) || (currentPD.Contacts == null)) { return; }
                FmContacts fmContacts = new FmContacts(currentXMLpath);
                fmContacts.Pdetails = currentPD;
                fmContacts.ShowDialog();
                listView1.Focus();
                ShowOnListview();
                listView1.Items[currentRowIndex].Selected = true;//设定选中
            }
            catch
            {
                this.isActived = false;
                MessageBox.Show("未选择项目或项目未建立项目信息文件。", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.isActived = true;
            }

        }
        #region ======================================菜单栏======================================
        private void 新建项目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(OpenFmNewInfo(FmNewInfo.FormType_enum.NEW) == DialogResult.OK)
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
                FmNewInfo.FormType_enum flag = FmNewInfo.FormType_enum.EDIT;
                if (currentPD == null)
                {
                    currentPD = new ProjectDetails();
                    
                    int pos_int = listView1.Items[currentRowIndex].Text.LastIndexOf("】");
                    currentPD.Pname = listView1.Items[currentRowIndex].Text.Remove(0, pos_int + 1);
                    flag = FmNewInfo.FormType_enum.CREATE;
                }
                currentPD.FolderName(listView1.Items[currentRowIndex].Text);
                if (OpenFmNewInfo(flag) == DialogResult.OK)
                {
                    ShowOnListview();
                }

                listView1.Items[currentRowIndex].Selected = true;//设定选中
            }
            catch
            {
                this.isActived = false;
                MessageBox.Show("未选择项目。", " 提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.isActived = true;
            }
        }
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.isActived = false;
            this.button1_Click(sender, e);
            this.isActived = true;
        }
        private void 设置目录列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            this.isActived = false;
            if (form5.ShowDialog() == DialogResult.OK)
            {
                setFileSystemWatcher();
                ShowOnListview();
            }
            this.isActived = true;
        }

        private void setFileSystemWatcher()
        {
            if (dicFSW.Count > 0) foreach (FileSystemWatcher feFSW in dicFSW.Values) feFSW.Dispose();
            dicFSW.Clear();
            foreach (string feS in msc_FolderList.pList)
            {
                if (!System.IO.Directory.Exists(feS)) continue;
                FileSystemWatcher tFSW = new FileSystemWatcher(feS);
                tFSW.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);
                tFSW.Created += new FileSystemEventHandler(fileSystemWatcher_Changed);
                tFSW.Deleted += new FileSystemEventHandler(fileSystemWatcher_Changed);
                tFSW.Renamed += new RenamedEventHandler(fileSystemWatcher_Changed);
                tFSW.NotifyFilter = NotifyFilters.DirectoryName;
                tFSW.IncludeSubdirectories = false;
                tFSW.EnableRaisingEvents = true;
                dicFSW.Add(feS, tFSW);
            }
        }

        private void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (mscCtrl.fmMain.InvokeRequired)
            {
                Action<string> actionDelegate = (x) =>
                {
                    msc_FolderList.flist_flash();
                    ShowOnListview();
                };
                mscCtrl.fmMain.Invoke(actionDelegate, string.Empty);
            }
        }


        #endregion
        #region ======================================自定义函数======================================

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
            mscIni.filePath = System.Environment.CurrentDirectory + @"\folders.ini";
            mscIni.creatINI();

            msc_FolderList.defaultPath = mscIni.getValue("DefaultPath");

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
            for (int i = 1; i <= int.Parse(mscIni.getValue("PathListCount")); i++)
            {
                tmp_list_str.Add(mscIni.getValue("PathList" + i.ToString()));
            }
            msc_FolderList.pList_fill(tmp_list_str);
            ShowOnListview();
        }
        /// <summary>
        /// 保存参数及地址列表到ini文件
        /// </summary>
        private void iniSave()
        {
            mscIni.setValue("TimeFilter", msc_FolderList.timeFilter.ToString());
            mscIni.setValue("DefaultPath", msc_FolderList.defaultPath);
            mscIni.setValue("PathListCount", msc_FolderList.pList.Count.ToString());//地址列表的数量，注意是数量不是最大下标
            for (int i = 1; i <= int.Parse(mscIni.getValue("PathListCount")); i++)
            {
                mscIni.setValue("PathList" + i.ToString(), msc_FolderList.pList[i - 1]);
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

                if (pd.Contacts != null) 
                {
                    foreach (string tmp_str in pd.Contacts)
                    {
                        string[] sArray = Regex.Split(tmp_str, ",", RegexOptions.IgnoreCase);

                        int index = dataGridView2.Rows.Add();
                        dataGridView2.Rows[index].Cells[0].Value = sArray[0];
                        dataGridView2.Rows[index].Cells[1].Value = sArray[2];
                    }
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
        private DialogResult OpenFmNewInfo(FmNewInfo.FormType_enum formtype)
        {
            this.isActived = false;
            FmNewInfo fmNewInfo = new FmNewInfo(currentXMLpath, formtype , currentPD);
            DialogResult r= fmNewInfo.ShowDialog();
            listView1.Focus();
            this.isActived = true;
            return r;            
        }


        #endregion ======================================自定义函数======================================

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
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
        private void FmMain_Deactivate(object sender, EventArgs e)
        {
            if (this.isActived)
            {
                this.WindowState = FormWindowState.Minimized;
                this.isActived = false;
            }
        }

        private void FmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isActived)
                this.isActived = true;
        }

        private void 导出当前项目列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string itemListStr = "";
            foreach (ListViewItem feLVI in listView1.Items)
            {
                itemListStr += "\r\n" + feLVI.Text;
            }
            this.isActived = false;
            string tPath = getSavePath();
            this.isActived = true;
            if (tPath != "")
            {
                StreamWriter SW = new StreamWriter(tPath, true);
                SW.Write(itemListStr);
                SW.Dispose();
            }


        }

        private static string getSavePath(string pFileName = "", string pFilter = "*.TXT|*.TXT")
        {
            //保存对话框获取保存地址
            using (SaveFileDialog SFD = new SaveFileDialog())
            {
                SFD.Filter = pFilter;
                SFD.FileName = pFileName;
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    return SFD.FileName;
                }   
                else
                {
                    return string.Empty;
                }   
            }
        }

        private void lblTbGrayWord_Click(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }
}
