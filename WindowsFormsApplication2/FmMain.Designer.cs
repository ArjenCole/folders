namespace Folders
{
    partial class FmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FmMain));
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.设置目录列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑项目信息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.管理通讯录ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目筛选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.一个月ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.三个月ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.六个月ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.全部ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出当前项目列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开项目ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑项目信息ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.管理通讯录ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 23);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(699, 437);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.DoubleClick += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "文件夹名称";
            this.columnHeader1.Width = 320;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "路径";
            this.columnHeader2.Width = 176;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "创建时间";
            this.columnHeader3.Width = 86;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "修改时间";
            this.columnHeader4.Width = 95;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Window;
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(197, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(502, 23);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.button_search_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label6.Location = new System.Drawing.Point(0, 460);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Loading...";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.工具ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(916, 25);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建项目ToolStripMenuItem,
            this.打开项目ToolStripMenuItem,
            this.toolStripSeparator1,
            this.设置目录列表ToolStripMenuItem,
            this.toolStripSeparator2,
            this.退出ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.文件ToolStripMenuItem.Text = "文件";
            // 
            // 新建项目ToolStripMenuItem
            // 
            this.新建项目ToolStripMenuItem.Name = "新建项目ToolStripMenuItem";
            this.新建项目ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.新建项目ToolStripMenuItem.Text = "新建项目";
            this.新建项目ToolStripMenuItem.Click += new System.EventHandler(this.新建项目ToolStripMenuItem_Click);
            // 
            // 打开项目ToolStripMenuItem
            // 
            this.打开项目ToolStripMenuItem.Name = "打开项目ToolStripMenuItem";
            this.打开项目ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.打开项目ToolStripMenuItem.Text = "打开项目";
            this.打开项目ToolStripMenuItem.Click += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // 设置目录列表ToolStripMenuItem
            // 
            this.设置目录列表ToolStripMenuItem.Name = "设置目录列表ToolStripMenuItem";
            this.设置目录列表ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.设置目录列表ToolStripMenuItem.Text = "设置目录列表";
            this.设置目录列表ToolStripMenuItem.Click += new System.EventHandler(this.设置目录列表ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(145, 6);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.编辑项目信息ToolStripMenuItem,
            this.管理通讯录ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 编辑项目信息ToolStripMenuItem
            // 
            this.编辑项目信息ToolStripMenuItem.Name = "编辑项目信息ToolStripMenuItem";
            this.编辑项目信息ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.编辑项目信息ToolStripMenuItem.Text = "编辑项目信息";
            this.编辑项目信息ToolStripMenuItem.Click += new System.EventHandler(this.编辑项目信息ToolStripMenuItem_Click);
            // 
            // 管理通讯录ToolStripMenuItem
            // 
            this.管理通讯录ToolStripMenuItem.Name = "管理通讯录ToolStripMenuItem";
            this.管理通讯录ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.管理通讯录ToolStripMenuItem.Text = "管理通讯录";
            this.管理通讯录ToolStripMenuItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // 工具ToolStripMenuItem
            // 
            this.工具ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目筛选ToolStripMenuItem,
            this.导出当前项目列表ToolStripMenuItem});
            this.工具ToolStripMenuItem.Name = "工具ToolStripMenuItem";
            this.工具ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.工具ToolStripMenuItem.Text = "工具";
            // 
            // 项目筛选ToolStripMenuItem
            // 
            this.项目筛选ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.一个月ToolStripMenuItem,
            this.三个月ToolStripMenuItem,
            this.六个月ToolStripMenuItem,
            this.全部ToolStripMenuItem});
            this.项目筛选ToolStripMenuItem.Name = "项目筛选ToolStripMenuItem";
            this.项目筛选ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.项目筛选ToolStripMenuItem.Text = "项目筛选";
            // 
            // 一个月ToolStripMenuItem
            // 
            this.一个月ToolStripMenuItem.Checked = true;
            this.一个月ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.一个月ToolStripMenuItem.Name = "一个月ToolStripMenuItem";
            this.一个月ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.一个月ToolStripMenuItem.Text = "一个月";
            this.一个月ToolStripMenuItem.Click += new System.EventHandler(this.TimeFilterToolStripMenuItem_Click);
            // 
            // 三个月ToolStripMenuItem
            // 
            this.三个月ToolStripMenuItem.Name = "三个月ToolStripMenuItem";
            this.三个月ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.三个月ToolStripMenuItem.Text = "三个月";
            this.三个月ToolStripMenuItem.Click += new System.EventHandler(this.TimeFilterToolStripMenuItem_Click);
            // 
            // 六个月ToolStripMenuItem
            // 
            this.六个月ToolStripMenuItem.Name = "六个月ToolStripMenuItem";
            this.六个月ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.六个月ToolStripMenuItem.Text = "六个月";
            this.六个月ToolStripMenuItem.Click += new System.EventHandler(this.TimeFilterToolStripMenuItem_Click);
            // 
            // 全部ToolStripMenuItem
            // 
            this.全部ToolStripMenuItem.Name = "全部ToolStripMenuItem";
            this.全部ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.全部ToolStripMenuItem.Text = "全部";
            this.全部ToolStripMenuItem.Click += new System.EventHandler(this.TimeFilterToolStripMenuItem_Click);
            // 
            // 导出当前项目列表ToolStripMenuItem
            // 
            this.导出当前项目列表ToolStripMenuItem.Name = "导出当前项目列表ToolStripMenuItem";
            this.导出当前项目列表ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.导出当前项目列表ToolStripMenuItem.Text = "导出当前项目列表";
            this.导出当前项目列表ToolStripMenuItem.Click += new System.EventHandler(this.导出当前项目列表ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(707, 3);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.Size = new System.Drawing.Size(206, 224);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDoubleClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(707, 233);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView2.Size = new System.Drawing.Size(206, 227);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellEndEdit);
            this.dataGridView2.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellMouseEnter);
            this.dataGridView2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView2_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开项目ToolStripMenuItem1,
            this.编辑项目信息ToolStripMenuItem1,
            this.管理通讯录ToolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 70);
            // 
            // 打开项目ToolStripMenuItem1
            // 
            this.打开项目ToolStripMenuItem1.Name = "打开项目ToolStripMenuItem1";
            this.打开项目ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.打开项目ToolStripMenuItem1.Text = "打开项目";
            this.打开项目ToolStripMenuItem1.Click += new System.EventHandler(this.listView1_DoubleClick);
            // 
            // 编辑项目信息ToolStripMenuItem1
            // 
            this.编辑项目信息ToolStripMenuItem1.Name = "编辑项目信息ToolStripMenuItem1";
            this.编辑项目信息ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.编辑项目信息ToolStripMenuItem1.Text = "编辑项目信息";
            this.编辑项目信息ToolStripMenuItem1.Click += new System.EventHandler(this.编辑项目信息ToolStripMenuItem_Click);
            // 
            // 管理通讯录ToolStripMenuItem1
            // 
            this.管理通讯录ToolStripMenuItem1.Name = "管理通讯录ToolStripMenuItem1";
            this.管理通讯录ToolStripMenuItem1.Size = new System.Drawing.Size(148, 22);
            this.管理通讯录ToolStripMenuItem1.Text = "管理通讯录";
            this.管理通讯录ToolStripMenuItem1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "folders";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            this.notifyIcon1.DoubleClick += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // FmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 477);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "FmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "folders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmMain_FormClosing);
            this.Load += new System.EventHandler(this.FmMain_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置目录列表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStripMenuItem 打开项目ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 管理通讯录ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑项目信息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem 工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目筛选ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 一个月ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 三个月ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 六个月ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 全部ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 编辑项目信息ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 管理通讯录ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 打开项目ToolStripMenuItem1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripMenuItem 导出当前项目列表ToolStripMenuItem;
    }
}

