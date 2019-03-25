using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Folders
{
    public class mc_Tree
    {
        public string Name { get; set; }
        public List<mc_Tree> son = new List<mc_Tree>();
    }
    static class msc_Tree
    {
        public static mc_Tree S = new mc_Tree();
        /// <summary>
        /// 根据二维数组填充数据
        /// </summary>
        /// <param name="cells">二维数组</param>
        public static void fill(string[,] cells)
        {
            mc_Tree P = new mc_Tree();
            mc_Tree C = new mc_Tree();
            S.Name = "中国";
            for(int i=1;i<=cells.GetUpperBound(0);i++)
            {
                int p_index = S.son.FindIndex(t => t.Name == cells[i, 2]);
                if(p_index < 0)
                {
                    S.son.Add(new mc_Tree());
                    p_index = S.son.Count-1;
                    S.son[p_index].Name = cells[i,2];
                }
                P = S.son[p_index];
                int c_index = P.son.FindIndex(t => t.Name == cells[i, 3]);
                if (c_index < 0)
                {
                    P.son.Add(new mc_Tree());
                    c_index = P.son.Count - 1;
                    P.son[c_index].Name = cells[i, 3];
                }
                C = P.son[c_index];
                int co_index = C.son.FindIndex(t => t.Name == cells[i, 4]);
                if (co_index < 0)
                {
                    C.son.Add(new mc_Tree());
                    co_index = C.son.Count - 1;
                    C.son[co_index].Name = cells[i, 4];
                    
                }
                P.son[c_index] = C;
                S.son[p_index] = P;
            }
        }

        public static void SetList(string comboBox1Txt,string comboBox2Txt,ComboBox comboBox)
        {

            comboBox.Items.Clear();
            List<mc_Tree> tmp1_tree = msc_Tree.S.son.FindAll(t => t.Name == null ? false : t.Name.Contains(comboBox1Txt));
            if (tmp1_tree == null) { return; }
            foreach (mc_Tree fe1_tree in tmp1_tree)
            {
                if(comboBox2Txt=="")
                {
                    foreach (mc_Tree fe2_tree in fe1_tree.son)
                    {
                        comboBox.Items.Add(fe2_tree.Name);
                    }
                }
                else
                {
                    List<mc_Tree> tmp2_tree = fe1_tree.son.FindAll(t => t.Name == null ? false : t.Name.Contains(comboBox2Txt));
                    if (tmp2_tree == null) { return; }
                    foreach (mc_Tree fe2_tree in tmp2_tree)
                    {
                        foreach (mc_Tree fe3_tree in fe2_tree.son)
                        {
                            comboBox.Items.Add(fe3_tree.Name);
                        }
                    }
                }
            }
        }

        /*测试用 输出列表
        public static List<string> sList = new List<string>();
        public static bool output1()
        {
            sList = new List<string>();
            foreach (mc_Tree fe1 in S.son)
            {
                string tmp_str = fe1.Name;
                foreach (mc_Tree fe2 in fe1.son)
                {
                    tmp_str += "," + fe2.Name;
                    foreach (mc_Tree fe3 in fe2.son)
                    {
                        sList.Add(tmp_str+"," + fe3.Name);
                    }
                }
            }
            return false;
        }
        */
    }
}
