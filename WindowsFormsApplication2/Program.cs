using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Folders
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread t = new Thread(ShowCover);
            t.Start();
            mscCtrl.fmMain = new FmMain();

            Application.Run(mscCtrl.fmMain);
            
            
        }
        static void ShowCover()
        {
            mscCtrl.fmCover = new FmCover();
            mscCtrl.fmCover.ShowDialog();

            if (mscCtrl.fmCover.InvokeRequired)
            {
                Action<string> actionDelegate = (x) => { mscCtrl.fmCover.Activate(); };
                mscCtrl.fmCover.Invoke(actionDelegate, string.Empty);
            }
        }

    }
}
