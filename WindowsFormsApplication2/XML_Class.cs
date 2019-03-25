using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Linq.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Folders
{
    public class XML_Class
    {
        /// <summary>
        /// 操作文件地址
        /// </summary>
        private string  filepath="";
        public string FilePath
        {
            set { filepath = value; }
            get { return filepath; }
        }

        /// <summary>
        /// XMl声明所需的三个参数，分别指定默认值
        /// </summary>
        private string version = "1.0";
        public string Version
        {
            set { version = value; }
            get { return version; }
        }
        private string encoding = "utf-8";
        public string Encoding
        {
            set { encoding = value; }
            get { return encoding; }
        }
        private string standalone = "yes";
        public string Standalone
        {
            set { standalone = value; }
            get { return standalone; }
        }
        /// <summary>
        /// XML元素
        /// </summary>
        private XElement xe;
        public XElement Xe
        {
            set { xe = value; }
            get { return xe; }
        }
        
        
        public bool XmlExists(string filePath) 
        {
            return File.Exists(filePath);
        }

        public bool SaveXml()
        {
            try
            {
                XDeclaration xd = new XDeclaration(version, encoding, standalone);//创建XML声明
                XDocument doc = new XDocument(xd,xe);//创建XML文件
                doc.Save(filepath);//保存XML文件
                return true;
            }
            catch
            {
                return false;
            }

        }
        /// <summary>
        /// 编写新的XML文件，此函数需根据实际情况重构
        /// </summary>
        /// <param name="pd">本项目自定义的项目细节类，别处使用需根据实际情况重构</param>
        public void WriteXml(ProjectDetails pd)
        {
            xe = new XElement("root",
                    new XElement("Project",
                        new XElement("Name", pd.Pname),//项目名称
                        new XElement("Index", pd.Pindex),//项目编号
                        new XElement("Province", pd.P),//项目所在省市
                        new XElement("City", pd.C),//项目所在城市
                        new XElement("ProjectLeader", pd.Pincharge),//项目负责人
                        new XElement("ProfessionalDirector", pd.Cincharge),//专业负责人
                        new XElement("SubjectDepartment", pd.Pdep),//主体部门
                        new XElement("ProjectNote", pd.Pnote)//备注
                        )
                    );
            if(pd.Contacts !=null)
            {
                foreach (string tmp_str in pd.Contacts)
                {
                    string[] sArray = Regex.Split(tmp_str, ",", RegexOptions.IgnoreCase);
                    XElement contact = new XElement("Contacts",
                                        new XElement("Name", sArray[0]),
                                        new XElement("CellPhone", sArray[1]),
                                        new XElement("FixedTele", sArray[2]),
                                        new XElement("Department", sArray[3]),
                                        new XElement("Major", sArray[4]),
                                        new XElement("Note", sArray[5])
                                        );
                    xe.Add(contact);
                }
            }

        }
        /// <summary>
        /// 读取XML文件，此函数需根据实际情况重构
        /// </summary>
        /// <param name="strPath">XML文件地址</param>
        /// <returns></returns>
        public ProjectDetails ReadXml(string strPath)
        {
            try
            {
                xe = XElement.Load(strPath);
                IEnumerable<XElement> elements = from ele in xe.Elements("Project")
                                                 select ele;

                ProjectDetails tmp_Pd = new ProjectDetails();
                foreach (XElement element in elements)
                {
                    tmp_Pd.Pname = element.Element("Name").Value;
                    tmp_Pd.Pindex = element.Element("Index").Value;
                    tmp_Pd.P = element.Element("Province").Value;
                    tmp_Pd.C = element.Element("City").Value;
                    tmp_Pd.Pincharge = element.Element("ProjectLeader").Value;
                    tmp_Pd.Cincharge = element.Element("ProfessionalDirector").Value;
                    tmp_Pd.Pdep = element.Element("SubjectDepartment").Value;
                    try
                    {
                        tmp_Pd.Pnote = element.Element("ProjectNote").Value;
                    }
                    catch
                    {
                        tmp_Pd.Pnote = "";
                    }
                }

                elements = null;
                elements = from ele in xe.Elements("Contacts")
                                                 select ele;

                List<string> contacts = new List<string>();
                foreach (XElement element in elements)
                {
                    string tmp_str = element.Element("Name").Value+",";
                    tmp_str += element.Element("CellPhone").Value + ",";
                    tmp_str += element.Element("FixedTele").Value + ",";
                    tmp_str += element.Element("Department").Value + ",";
                    tmp_str += element.Element("Major").Value + ",";
                    tmp_str += element.Element("Note").Value;
                    contacts.Add(tmp_str);
                }
                tmp_Pd.Contacts = contacts.ToArray();
                return tmp_Pd;
            }
            catch
            {
                return null;
            }
        }

    }
}
