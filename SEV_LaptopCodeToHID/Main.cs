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
using LT_Support;
using Gma.System.MouseKeyHook;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace SEV_LaptopCodeToHID
{
    public partial class Main : Form
    {
        public MainDisplay formMainDisplay;
        public MainSetting formMainSetting;
        public Halcon.Vision Vision;
        public TCP4Client TCPClientReader;
        public System.Timers.Timer TimerResetOLDCodeValue;
        public string OldCodeString = "";
        public IKeyboardMouseEvents KeyboardMonitor;
        public System.Timers.Timer TimerStartVision;
        public int DelayTimeSendEnter;
        public SerialPort GD4500SerialPort;

        public Main()
        {
            InitializeComponent();

            DelayTimeSendEnter = 3000;

            KeyboardMonitor = Hook.GlobalEvents();
            KeyboardMonitor.KeyDown += ProcessKeyEvent;
            formMainDisplay = new MainDisplay() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            formMainSetting = new MainSetting() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.panelControlDetail.Controls.Add(formMainDisplay);
            this.panelControlDetail.Controls.Add(formMainSetting);
            formMainDisplay.Show();
            formMainSetting.Hide();
            Vision = new Halcon.Vision();
            Vision.DisplayWD = hSmartWindowControlMain.HalconWindow;
            TimerStartVision = new System.Timers.Timer();
            TimerStartVision.Interval = 100;
            TimerStartVision.Enabled = true;
            TimerStartVision.Elapsed += ProcessRunVision;
            TimerStartVision.Stop();

            //TCPClientReader = new TCP4Client("192.168.125.100", 51236);
            //TCPClientReader.ReceivedStringFromServerEvent += ProcessStringFromDLCodeServer;

            TimerResetOLDCodeValue = new System.Timers.Timer(5000);
            TimerResetOLDCodeValue.AutoReset = true;
            TimerResetOLDCodeValue.Elapsed += ResetOldStringFromHandheld;

            string[] listComport = SerialPort.GetPortNames();
            string comportName = "";
            try
            {
                comportName = listComport.Last();
            }
            catch
            {
                comportName = "COM6";
            }

            GD4500SerialPort = new SerialPort(comportName, 9600, Parity.None, 8, StopBits.One);
            GD4500SerialPort.DataReceived += ProcessSerialPortMessage;
            GD4500SerialPort.DtrEnable = true;
            GD4500SerialPort.Open();

        }

        public string CodeSumString = "";
        private void ProcessSerialPortMessage(object sender, SerialDataReceivedEventArgs e)
        {
            string strReceive = GD4500SerialPort.ReadExisting();
            if (!strReceive.Contains("\r"))
            {
                CodeSumString += strReceive;
            }
            else
            {
                CodeSumString += strReceive;
                if (CodeSumString != OldCodeString)
                {
                    // Luu gia tri code cu, tranh doc 2 code trung nhau trong khoang 5s
                    TimerResetOLDCodeValue.Start();
                    OldCodeString = CodeSumString;

                    //Console.WriteLine(CodeSumString);
                    CodeSumString = CodeSumString.Replace("\r", "");
                    Invoke(new MethodInvoker(delegate
                    {
                        CodeSumString = Regex.Replace(CodeSumString, "[+^%~()]", "{$0}");
                        System.Windows.Forms.SendKeys.SendWait(CodeSumString);  // Paste
                        System.Threading.Thread.Sleep(10);
                    }));

                    CodeSumString = "";
                    TimerStartVision.Start();
                }
            }
        }

        private void ProcessRunVision(object sender, ElapsedEventArgs e)
        {
            formMainDisplay.SetDataCode1("Received Cover Code!");
            TimerStartVision.Stop();
            string result = Vision.Run();
            formMainDisplay.SetDataCode2(result);

            if (!result.Contains("ERR"))
            {
                formMainDisplay.SetOKLamp();
                OldCodeString = "";
                System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                SendTextToClipBroard("", result);
            }
            else
            {
                try
                {
                    formMainDisplay.SetNGLamp();
                    System.Threading.Thread.Sleep(10);
                    System.Windows.Forms.SendKeys.SendWait("^a");
                    System.Threading.Thread.Sleep(10);
                    System.Windows.Forms.SendKeys.SendWait("{DELETE}");
                }
                catch { }
            }
        }

        private void ProcessKeyEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Oemplus)
            {
                e.Handled = true;
                TimerStartVision.Start();

            }
        }

        private void ResetOldStringFromHandheld(object sender, ElapsedEventArgs e)
        {
            OldCodeString = "";
            TimerResetOLDCodeValue.Stop();
        }

        private void ProcessStringFromDLCodeServer(string receivedString)
        {
            receivedString = receivedString.Replace('\0', ' ').Trim();
            if (!receivedString.Contains("NoRead"))
            {
                if (OldCodeString != receivedString)
                {
                    OldCodeString = receivedString;
                    TimerResetOLDCodeValue.Start();
                    formMainDisplay.SetDataCode1(receivedString);

                    string result = Vision.Run();
                    formMainDisplay.SetDataCode2(result);

                    if (!result.Contains("ERR"))
                    {
                        SendTextToClipBroard(receivedString, result);
                    }
                }
            }
        }

        private void SendTextToClipBroard(string inputString1, string inputString2)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string, string>(SendTextToClipBroard), new object[] { inputString1, inputString2 });
            }
            else
            {
                System.Windows.Clipboard.Clear();  // Always clear the clipboard first
                string stringWrite;
                if (inputString1.Length > 0)
                {
                    stringWrite = inputString1;
                    stringWrite = Regex.Replace(stringWrite, "[+^%~()]", "{$0}");
                    SendKeys.SendWait(stringWrite); // Paste
                    System.Threading.Thread.Sleep(10);
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                    System.Threading.Thread.Sleep(10);
                }

                System.Threading.Thread.Sleep(DelayTimeSendEnter);

                foreach (string item in inputString2.Split(','))
                {
                    stringWrite = item;
                    stringWrite = Regex.Replace(stringWrite, "[+^%~()]", "{$0}");
                    SendKeys.SendWait(stringWrite);
                    System.Threading.Thread.Sleep(10);
                    System.Windows.Forms.SendKeys.SendWait("{ENTER}");
                    System.Threading.Thread.Sleep(DelayTimeSendEnter);
                }

            }
        }

        private void pbCloseMainForm_Click(object sender, EventArgs e)
        {
            try
            {
                KeyboardMonitor.KeyDown -= ProcessKeyEvent;
                KeyboardMonitor.Dispose();
                GD4500SerialPort.Close();
            }
            catch { }
            this.Dispose();
        }

        private void pbSettingsMain_Click(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            // Ngắt kết nối camera để cài đặt
            //------------------------------------------------------------
            Vision.HFramegrabber.Dispose();
            LT_Support.Forms.CameraConnect tempCameraSetting = new LT_Support.Forms.CameraConnect();
            tempCameraSetting.ShowDialog();
            tempCameraSetting.Dispose();

            formMainDisplay.Hide();
            formMainSetting.Show();
        }

        private void pbHomeMain_Click(object sender, EventArgs e)
        {
            formMainDisplay.Show();
            formMainSetting.Hide();
        }

        private void pbCameraTriggerMain_Click(object sender, EventArgs e)
        {
            string result = Vision.Run();
            formMainDisplay.SetDataCode2(result);
            formMainDisplay.SetOKLamp();
        }

        private void timerMain_Tick(object sender, EventArgs e)
        {
            txtClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void txtDelayTimeMain_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DelayTimeSendEnter = int.Parse(txtDelayTimeMain.Text);
            }
            catch
            {
                DelayTimeSendEnter = 3000;
            }
        }

        public bool LiveViewON = false;
        private void pbLiveView_Click(object sender, EventArgs e)
        {
            if (!LiveViewON)
            {
                LiveViewON = true;
                Vision.LiveView();
            }
            else
            {
                LiveViewON = false;
                Vision.StopLiveView();
            }
        }
    }
}
