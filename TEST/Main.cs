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
using log4net;
using System.IO;

namespace TEST
{
    public partial class Main : Form
    {
        public TCP4Client EmulatorTCP;
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Timer Timer500ms;

        public Main()
        {
            InitializeComponent();
            Timer500ms = new Timer();
            Timer500ms.Interval = 500;
            Timer500ms.Tick += ReadLog;
            Timer500ms.Enabled = true;
            ConnectToTCPServer();
        }

        private void ReadLog(object sender, EventArgs e)
        {
            string log = "";
            string currentLogFile = AppDomain.CurrentDomain.BaseDirectory + "log\\AppLog_";
            using (var fileStream = new FileStream(currentLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var textReader = new StreamReader(fileStream))
            {
                log = textReader.ReadToEnd();
            }
            if (txtLogBox.Text != log)
            {
                txtLogBox.Text = log;
            }
        }

        private void ConnectToTCPServer()
        {
            log.Debug("Dispose TCP Socket if existed");
            if (EmulatorTCP != null)
            {
                EmulatorTCP.Dispose();
            }
            log.Info("Create New TCP Client Socket");

            EmulatorTCP = new TCP4Client("127.0.0.1", 12345);
            if (EmulatorTCP.Connected)
            {
                log.Info("Connected to Server, follow event");
                EmulatorTCP.ReceivedStringFromServerEvent -= ShowMessageFromServer;
                EmulatorTCP.ReceivedStringFromServerEvent += ShowMessageFromServer;
                EmulatorTCP.ServerDisconnectEvent -= ShowMessageDisconnect;
                EmulatorTCP.ServerDisconnectEvent += ShowMessageDisconnect;
                btnConnect.Text = "Connected";
            }
        }

        private void ShowMessageDisconnect()
        {
            log.Error("Disconnect from Server");
            MessageBox.Show($"Disconnect from server!!!");
        }

        private void ShowMessageFromServer(string receivedString)
        {
            MessageBox.Show($"Server send a message: {receivedString}");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectToTCPServer();
        }
    }
}
