using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LT_Support;

namespace TEST
{
    public partial class FindToolByName : Form
    {
        public List<object> ToolCollection;
        public object01 A123, B134;
        public object02 A234, B245, C256;

        private void btnFindObject_Click(object sender, EventArgs e)
        {
            foreach (object item in ToolCollection)
            {
                string tempName = item.GetType().GetProperty("Name").GetValue(item) == null ? "" : item.GetType().GetProperty("Name").GetValue(item).ToString();
                if (tempName == txtObjectName.Text)
                {
                    int index = (int)item.GetType().GetProperty("Index").GetValue(item);
                    string nameObj = nameof(item);
                    txtObjectInfo.Text = $"Object name : {nameObj}\nObject index : {index}";
                }
            }
        }

        public FindToolByName()
        {
            InitializeComponent();
            ToolCollection = new List<object>();
            A123 = new object01("Name1", 1);
            A234 = new object02("Name2", 2);
            B245 = new object02("Name3", 3);
            B134 = new object01("Name4", 4);
            C256 = new object02("Name5", 5);
            ToolCollection.Add(A123);
            ToolCollection.Add(A234);
            ToolCollection.Add(B245);
            ToolCollection.Add(B134);
            ToolCollection.Add(C256);
        }

        public class object01
        {
            public string Name { get; set; }
            public int Index { get; set; }
            public object01(string name, int index)
            {
                Name = name;
                Index = index;
            }
        }

        public class object02
        {
            public string Name { get; set; }
            public int Index { get; set; }
            public object02(string name, int index)
            {
                Name = name;
                Index = index;
            }
        }
    }
}
