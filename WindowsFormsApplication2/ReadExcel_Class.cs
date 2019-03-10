using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace Folders
{
    class ReadExcel_Class
    {
        //生成->项目属性->64位
        public int RowCnt = 0;//表格行数
        public int ColCnt = 0;//表格列数
        public string[,] Cell = new string[5000, 500];//默认表单5000行*500列

        private DataSet myds = new DataSet();//创建数据集
        public void read(string fileadd="",string sheetname="")//核心方法，根据地址fileadd读取文件中的表单sheetname
        {
            OleDbConnection olecnt = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
                + fileadd + ";Extended Properties=Excel 8.0");//连接EXCEL数据库 要添加using System.Data.OleDb;！！
            olecnt.Open();//打开数据库连接
            OleDbDataAdapter oledbda = new OleDbDataAdapter("select * from [" + sheetname + "$]", olecnt);
            myds.Clear();
            oledbda.Fill(myds);
            oledbda.Dispose();
            olecnt.Close();

            RowCnt = myds.Tables[0].Rows.Count;//获取数据集行列数量
            ColCnt= myds.Tables[0].Columns.Count;

            int tmprow = 0;int tmpcol =0;//获取表单准确行列数量
            for (int i = 0; i <= RowCnt - 1;i++)
            {
                for(int j=0;j<=ColCnt-1;j++)
                {
                    Cell[i+1, j+1] = myds.Tables[0].Rows[i][j].ToString();
                    if (Cell[i + 1, j + 1] != "")
                    {
                        if (i> tmprow) {tmprow = i;}
                        if (j> tmpcol) {tmpcol = i;}
                    }
                }
            }
            RowCnt = tmprow;ColCnt = tmpcol;
        }

        public void read(string pFilePath)
        {
            DataTable rtDT = new DataTable();
            FileStream tFS = new FileStream(pFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader tSR = new StreamReader(tFS, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = tSR.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(aryLine[i]);
                        rtDT.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = rtDT.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    rtDT.Rows.Add(dr);
                }
            }
            tSR.Close();
            tFS.Close();


            RowCnt = rtDT.Rows.Count;//获取数据集行列数量
            ColCnt = rtDT.Columns.Count;

            int tmprow = 0; int tmpcol = 0;//获取表单准确行列数量
            for (int i = 0; i <= RowCnt - 1; i++)
            {
                for (int j = 0; j <= ColCnt - 1; j++)
                {
                    Cell[i + 1, j + 1] = rtDT.Rows[i][j].ToString();
                    if (Cell[i + 1, j + 1] != "")
                    {
                        if (i > tmprow) { tmprow = i; }
                        if (j > tmpcol) { tmpcol = i; }
                    }
                }
            }
            RowCnt = tmprow; ColCnt = tmpcol;
        }

    }
}
