using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Folders
{
    public class mc_FolderInfoType
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string CreationTime { get; set; }
        public string EditTime { get; set; }
    }
    static class msc_FolderList
    {
        public static List<mc_FolderInfoType> fList=new List<mc_FolderInfoType>();//文件夹列表
        public static List<mc_FolderInfoType> fList_s = new List<mc_FolderInfoType>();//检索后文件夹列表
        public static List<string> pList = new List<string>();//地址列表
        public static List<string> cList = new List<string>();//检索关键字列表
        public static int timeFilter = 1;
        public static string defaultPath
        {
            get
            {
                return defaultpath == "" ? fList[0].Path : defaultpath;
            }
            set
            {
                defaultpath = value;
            }
        }

        private static string defaultpath = "";
        

        /// <summary>
        /// 填充地址列表（覆盖原列表）
        /// </summary>
        /// <param name="add_pList">需填充地址列表List<string>格式</param>
        public static void pList_fill(List<string> new_pList)
        {
            pList = new_pList;
            if (pList != null) { flist_flash(); }
        }
        /// <summary>
        /// 文件列表刷新（不修改地址列表）
        /// </summary>
        public static void flist_flash()
        {
            fList =new List<mc_FolderInfoType>();
            foreach (string path_str in pList)//遍历地址列表
            {
                DirectoryInfo dir = new DirectoryInfo(path_str);//新建字典信息变量
                FileSystemInfo[] f;
                try
                {
                    f = dir.GetFileSystemInfos();//读取文件信息
                    foreach (FileSystemInfo i in f)//遍历文件
                    {
                        if (i is DirectoryInfo)//如果为文件夹
                        {
                            FileInfo fin = new FileInfo(i.FullName);
                            string tmp_str = pList.Find(s => s == path_str + @"\" + fin.Name);
                            if (string.IsNullOrEmpty(tmp_str))
                            {
                                mc_FolderInfoType tmp_FIT = new mc_FolderInfoType();
                                tmp_FIT.Name = fin.Name;
                                tmp_FIT.Path = path_str + @"\";
                                tmp_FIT.CreationTime = fin.CreationTime.ToString(@"yyyy/MM/dd");
                                tmp_FIT.EditTime = fin.LastWriteTime.ToString(@"yyyy/MM/dd");
                                fList.Add(tmp_FIT);
                            }
                        }
                    }
                    search();
                }
                catch
                {
                    f = null;
                }
                
            }
        }
        /// <summary>
        /// 填充检索关键字列表（覆盖原列表）
        /// </summary>
        /// <param name="new_cList">关键字列表List格式</param>
        public static void cList_fill(List<string> new_cList)
        {
            if (new_cList == null) { return; }
            cList = new List<string>();
            foreach (string c in new_cList)
            {
                string tmp_str = RemoveSuffix(c);
                cList.Add(tmp_str);
            }
            search();
        }
        public static string RemoveSuffix(string pStr)
        {
            string rtS = pStr;
            string[] replace_str = new string[] { "省", "市", "地区", "盟", "区", "旗", "县", "新区" };
            foreach (string r in replace_str)
                rtS = rtS.Replace(r, ""); 
            return rtS;
        }
        /// <summary>
        /// 查找符合条件的文件夹并列入flist_s列表中
        /// </summary>
        public static void search()
        {
            if(cList==null)
            {
                fList_s = fList;
            }
            else
            {
                fList_s = null;
                fList_s = fList.FindAll(t => flag(t));
            }
            
        }
        /// <summary>
        /// search中lambda表达式委托函数
        /// </summary>
        /// <param name="tmp_FIT">文件夹信息类型</param>
        /// <param name="tmp_arr">检索关键词数组</param>
        /// <returns>包含所有关键词返回true否则为false</returns>
        static bool flag(mc_FolderInfoType tmp_FIT)
        {
            TimeSpan ts= DateTime.Now- DateTime.Parse(tmp_FIT.EditTime);
            if ((ts.Days > timeFilter * 30) &&(timeFilter>0)){return false;}
            foreach(string c in cList)
            {   
                if (tmp_FIT.Name.IndexOf(c) < 0)
                {
                    return false;
                }
            }
            return true;
        }


    }
}
