using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folders
{
    public static class mscCtrl
    {
        public static FmMain fmMain;
        public static FmCover fmCover;

        public static bool fmMainStarted = false;

        public static ProjectDetails getPD(string pPath)
        {
            ProjectDetails rtPD = new Folders.ProjectDetails();
            if (File.Exists(pPath))
            {
                XML_Class x = new XML_Class();
                rtPD = x.ReadXml(pPath);
            }
            else
            {
                rtPD = null;
            }
            return rtPD;
        }
    }
}
