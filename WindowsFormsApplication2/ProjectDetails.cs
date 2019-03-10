using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folders
{
    public class ProjectDetails
    {
        private string p = "";
        public string P
        {
            get { return p; }
            set { p = value; }
        }
        private string c = "";
        public string C
        {
            get { return c; }
            set { c = value; }
        }
        private string pname = "";
        public string Pname
        {
            get { return pname; }
            set { pname = value; }
        }
        private string pindex = "";
        public string Pindex
        {
            get { return pindex; }
            set { pindex = value; }
        }
        private string pincharge = "";
        public string Pincharge
        {
            get { return pincharge; }
            set { pincharge = value; }
        }
        private string cincharge = "";
        public string Cincharge
        {
            get { return cincharge; }
            set { cincharge = value; }
        }
        private string pdep = "";
        public string Pdep
        {
            get { return pdep; }
            set { pdep = value; }
        }
        private string pnote = "";
        public string Pnote
        {
            get { return pnote; }
            set { pnote = value; }
        }

        private string[] contacts = null;
        public string[] Contacts
        {
            get { return contacts; }
            set { contacts = value; }
        }

        private string foldername = "";
        public string FolderName()
        {
            //string y = DateTime.Now.Year.ToString().Substring(2,2);
            string y = DateTime.Now.ToString("yyyy");
            string m = DateTime.Now.ToString("MM");//"mm"表示分钟
            string d = DateTime.Now.ToString("dd");
            foldername =y + m + "【" + p + "】" + "【" + c + "】" + pname;
            return foldername;
        }
        public string FolderName(string str)
        {
            foldername = str;
            return foldername;
        }

    }
}
