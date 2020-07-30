using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TEST
{
    public partial class TaskLearnDemo : Form
    {
        public TaskLearnDemo()
        {
            InitializeComponent();
        }

        private void btnCalculator_Click(object sender, EventArgs e)
        {
            var stopWatch = new Stopwatch();
            string result = "";
            var task = new Task(() =>
            {
                stopWatch.Start();
                result = Fibo(textBox1.Text).ToString();
            });
            task.ContinueWith((previewTask) =>
            {
                textBox2.Text = result;
                stopWatch.Stop();
                label2.Text = (stopWatch.ElapsedMilliseconds).ToString();
                stopWatch.Reset();
            }, TaskScheduler.FromCurrentSynchronizationContext()
            );
            task.Start();

        }

        private object Fibo(string nthValue)
        {
            try
            {
                ulong x = 0, y = 1, z = 0, nth, i;
                nth = Convert.ToUInt64(nthValue);
                for (i = 0; i < nth; i++)
                {
                    z = x + y;
                    x = y;
                    y = z;
                }
                return z;
            }
            catch { }
            return 0;
        }
    }
}
