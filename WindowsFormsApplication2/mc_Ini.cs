using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Folders
{
    class mc_Ini
    {
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName,string lpKeyName,string lpDefault,StringBuilder lpReturnedString,int nSize,string lpFileName);
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string mpAppName,string mpKeyName,string mpDefault,string mpFileName);
        public string filePath = "";//该变量保存INI文件所在的具体物理位置

        
        public string getValue(string para_KeyName)
        {
            if(File.Exists(filePath))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath); 				//获取INI文件的文件名
                StringBuilder stringBuilder = new StringBuilder(1024);               //定义一个最大长度为1024的可变字符串
                GetPrivateProfileString(fileName, para_KeyName, "", stringBuilder, 1024, filePath);          //读取INI文件
                return stringBuilder.ToString();								//返回INI文件的内容
            }
            return "";
        }
        public void setValue(string para_KeyName,string para_KeyValue)
        {
            if (File.Exists(filePath))
            {
                string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath); 				//获取INI文件的文件名
                StringBuilder stringBuilder = new StringBuilder(1024);               //定义一个最大长度为1024的可变字符串
                WritePrivateProfileString(fileName, para_KeyName, para_KeyValue,filePath); 		//修改INI文件中节点的内容
            }

        }
    }

    static class msc_Ini
    {
        public static mc_Ini mc_ini=new mc_Ini();
    }
}
