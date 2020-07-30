using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace SEV_LaptopCodeToHID
{
    public partial class MainDisplay : Form
    {
        public System.Timers.Timer TimerHideActiveCodeIcon;

        public MainDisplay()
        {
            InitializeComponent();
            TimerHideActiveCodeIcon = new System.Timers.Timer(5000);
            TimerHideActiveCodeIcon.Elapsed += HideActiveCodeIcon;
            //HideActiveCodeIcon(null, null);
        }

        public void HideActiveCodeIcon(object sender, ElapsedEventArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                pbActiveCodeCover.Hide();
                pbActiveCodeFront.Hide();
                pbActiveCodeBattery.Hide();
                pbActiveCodeBoard.Hide();

                txtBatteryCode.Text = "";
                txtBoardCode.Text = "";
                txtCoverCode.Text = "";
                txtFrontCode.Text = "";
                ResetOKNGLamp();
            }));
            TimerHideActiveCodeIcon.Stop();
        }

        public void SetDataCode1(string inputString)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetDataCode1), new object[] { inputString });
            }
            else
            {
                txtCoverCode.Text = inputString;
                pbActiveCodeCover.Show();
            }
        }

        public void SetDataCode2(string inputString)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(SetDataCode2), new object[] { inputString });
            }
            else
            {
                TimerHideActiveCodeIcon.Start();
                string[] strArr = inputString.Split(',');
                txtFrontCode.Text = strArr[2];
                txtBatteryCode.Text = strArr[1];
                txtBoardCode.Text = strArr[0];
                pbActiveCodeFront.Show();
                pbActiveCodeBattery.Show();
                pbActiveCodeBoard.Show();
            }
        }

        public void SetOKLamp()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { SetOKLamp(); }));
            }
            else
            {
                btnOKNG.BackColor = Color.LawnGreen;
                btnOKNG.Text = "OK";
            }
        }

        public void SetNGLamp()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { SetNGLamp(); }));
            }
            else
            {
                btnOKNG.BackColor = Color.DarkRed;
                btnOKNG.Text = "NG";
            }
        }

        public void ResetOKNGLamp()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(delegate { ResetOKNGLamp(); }));
            }
            else
            {
                btnOKNG.BackColor = Color.Gray;
                btnOKNG.Text = "";
            }
        }
    }
}
