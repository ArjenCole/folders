using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Folders
{
    public static class mscIni
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName,string lpKeyName,string lpDefault,StringBuilder lpReturnedString,int nSize,string lpFileName);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string mpAppName,string mpKeyName,string mpDefault,string mpFileName);
        public static string filePath = "";//该变量保存INI文件所在的具体物理位置

        
        public static string getValue(string para_KeyName)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);                 //获取INI文件的文件名
            StringBuilder stringBuilder = new StringBuilder(1024);               //定义一个最大长度为1024的可变字符串
            GetPrivateProfileString(fileName, para_KeyName, "", stringBuilder, 1024, filePath);          //读取INI文件
            return stringBuilder.ToString();								//返回INI文件的内容
        }
        public static void setValue(string para_KeyName,string para_KeyValue)
        {
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);                 //获取INI文件的文件名
            StringBuilder stringBuilder = new StringBuilder(1024);               //定义一个最大长度为1024的可变字符串
            WritePrivateProfileString(fileName, para_KeyName, para_KeyValue, filePath); 		//修改INI文件中节点的内容
        }
        public static void creatINI()
        {
            if (!File.Exists(filePath))
            {
                FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                fs1.Dispose();
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);                 //获取INI文件的文件名
                WritePrivateProfileString(fileName, "TimeFilter", "-1", filePath);
                WritePrivateProfileString(fileName, "DefaultPath", @"C:", filePath);
                WritePrivateProfileString(fileName, "PathListCount", "1", filePath);
                WritePrivateProfileString(fileName, "PathList1", @"C:", filePath);
            }
                
        }
    }

}
