using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleTCP;
using System.IO.Ports;
using System.Text.RegularExpressions;

namespace SEV_LaptopCode_Client
{
    public partial class FormMain : Form
    {
        public SimpleTcpClient ReaderTCPClient;
        public MainDisplay formMainDisplay;
        public SerialPort COMHandheld, COMGmes;
        public FormMain()
        {
            InitializeComponent();
            //------------------------------------------------------------
            // Khởi tạo form hiển thị
            //------------------------------------------------------------
            formMainDisplay = new MainDisplay() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true, Left = 50 };
            pnlMainDisplay.Controls.Add(formMainDisplay);
            formMainDisplay.Show();
            //------------------------------------------------------------
            // Kết nối đến Server đọc code
            //------------------------------------------------------------
            try
            {
                ReaderTCPClient = new SimpleTcpClient();
                ReaderTCPClient.Connect("127.0.0.1", 12345);
                ReaderTCPClient.DataReceived += ProcessReaderServerMessage;
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server đọc code");
                this.Dispose();
            }

            try
            {
                COMHandheld = new SerialPort("COM6", 9600, Parity.None, 8, StopBits.One);
                COMHandheld.DataReceived += ProcessHandheldPortMessage;
                COMHandheld.DtrEnable = true;
                COMHandheld.Open();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối tay cầm");
                this.Dispose();
            }

            try
            {
                COMGmes = new SerialPort("COM8", 9600, Parity.None, 8, StopBits.One);
                //COMGmes.DataReceived += ProcessHandheldPortMessage;
                COMGmes.DtrEnable = true;
                COMGmes.Open();
            }
            catch
            {
                MessageBox.Show("Không thể kết nối Gmes");
                this.Dispose();
            }

        }

        private void ProcessReaderServerMessage(object sender, SimpleTCP.Message e)
        {
            //throw new NotImplementedException();
        }

        private string CodeSumString = "";
        private void ProcessHandheldPortMessage(object sender, SerialDataReceivedEventArgs e)
        {
            string strReceive = COMHandheld.ReadExisting();
            if (!strReceive.Contains("\r"))
            {
                CodeSumString += strReceive;
            }
            else
            {
                CodeSumString += strReceive;
                if (true)
                {
                    CodeSumString = CodeSumString.Replace("\r", "");
                    ReaderTCPClient.WriteLine($"Trigger,{CodeSumString},");
                    formMainDisplay.SetDataCode1(CodeSumString);
                    string serverRplString = "";
                    try
                    {
                        serverRplString = ReaderTCPClient.WriteLineAndGetReply($"Trigger,{CodeSumString},", new TimeSpan(0, 0, 5)).MessageString;
                    }
                    catch
                    {
                        serverRplString = "ERRTimeout, ERRTimeout, ERRTimeout";
                    }
                    formMainDisplay.SetDataCode2(serverRplString.Trim());
                    if (!serverRplString.Contains("ERR"))
                    {
                        formMainDisplay.SetOKLamp();
                    }
                    else
                    {
                        formMainDisplay.SetNGLamp();
                    }
                    CodeSumString = "";
                }
            }
        }

        private void pbExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
