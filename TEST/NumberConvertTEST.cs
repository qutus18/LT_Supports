using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using log4net;

namespace TEST
{
    public partial class NumberConvertTEST : Form
    {
        public NumberConvertTEST()
        {
            InitializeComponent();

        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            try
            {
                int numberInput = int.Parse(txtNumberInput.Text);
                byte[] outByteArr = BitConverter.GetBytes(numberInput);
                //------------------------------------------------------------
                // Hiển thị giá trị byte[]
                //------------------------------------------------------------
                string strByte = "";
                foreach (byte item in outByteArr)
                {
                    if (strByte != "")
                    {
                        strByte += "-" + item.ToString();
                    }
                    else
                    {
                        strByte += item.ToString();
                    }
                }
                txtConvertByte.Text = strByte;
                //------------------------------------------------------------
                // Hiển thị giá trị int16
                //------------------------------------------------------------
                int outInt16 = (int)BitConverter.ToInt16(outByteArr, 0);
                txtConvertInt16.Text = outInt16.ToString();
                //------------------------------------------------------------
                // Hiển thị giá trị int32
                //------------------------------------------------------------
                int outInt32 = (int)BitConverter.ToInt32(outByteArr, 0);
                txtConvertInt32.Text = outInt32.ToString();
                //------------------------------------------------------------
                // Hiển thị Ascii
                //------------------------------------------------------------
                string outAscii = "";
                foreach (byte item in outByteArr)
                {
                    outAscii += (char)item;
                }
                txtConvertString.Text = "Hello World. I'm Latus";
                //------------------------------------------------------------
                // Hiển thị Bit Array
                //------------------------------------------------------------
                string outBitString = "";
                bool[][] arrBool = LT_Support.PLC3e.ConvertWordToBitArray(new int[] { numberInput }, 1);
                if (arrBool.Length > 0)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        outBitString += (arrBool[0][i] == true ? 1 : 0).ToString();
                    }
                }
                txtConvertBit.Text = outBitString;
                //------------------------------------------------------------
                // Hiển thị Bit Raise Pulse
                //------------------------------------------------------------
                outBitString = "";
                bool[][] arrBoolOld = LT_Support.PLC3e.ConvertWordToBitArray(new int[] { 1234 }, 1);
                bool[][] arrBoolNew = LT_Support.PLC3e.ConvertWordToBitArray(new int[] { 218 }, 1);
                arrBool = LT_Support.PLC3e.CompareBitArrarToFallPulse(arrBoolOld, arrBoolNew);
                if (arrBool.Length > 0)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        outBitString += (arrBool[0][i] == true ? 1 : 0).ToString();
                    }
                }
                txtConvertBit.Text = outBitString;
                //------------------------------------------------------------
                // Hiển thị Int[] từ String
                //------------------------------------------------------------
                int[] convertedIntArr = LT_Support.PLC3e.StringToWordArr(txtConvertString.Text);
                string strInt = "";
                foreach (int item in convertedIntArr)
                {
                    if (strInt != "")
                    {
                        strInt += "-" + item.ToString();
                    }
                    else
                    {
                        strInt += item.ToString();
                    }
                }
                txtConvertBit.Text = strInt;
                //------------------------------------------------------------
                // Hiển thị DWord
                //------------------------------------------------------------
                int dWord = LT_Support.PLC3e.GetDWordFromWordArr(convertedIntArr, 3);
                txtDWord.Text = dWord.ToString();
                //------------------------------------------------------------
                // Hiển thị SubString
                //------------------------------------------------------------
                string subString = LT_Support.PLC3e.GetStringFromWordArr(convertedIntArr, 3, 4);
                txtSubStringConvert.Text = subString;
            }
            catch
            {
                txtNumberInput.Text = "";
                txtConvertByte.Text = "";
                txtConvertInt16.Text = "";
                txtConvertInt32.Text = "";
                txtConvertString.Text = "";
                txtConvertBit.Text = "";
            }
        }

        private void btnCreatePLC3e_Click(object sender, EventArgs e)
        {
            LT_Support.PLC3eClient PLCConnection = new LT_Support.PLC3eClient("192.168.125.100", 4568, "D1000", 5, "D2000", 10);
            //LT_Support.PLC3eClient PLCConnection = new LT_Support.PLC3eClient("127.0.0.1", 12345, "D1000", 5, "D2000", 10);
            PLCConnection.SendDataArr[0] = 12345;
            PLCConnection.SendDataArr[1] = -12345;
            PLCConnection.SendDataArr[2] = -6789;
            PLCConnection.ReceivedCommandFromServerEvent += CountCommand;
        }

        int countXTCmd, countHECmd;
        private static readonly log4net.ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private void CountCommand(string commandString)
        {
            switch (commandString){
                case ("XT"):
                    countXTCmd += 1;
                    break;
                case ("HE"):
                    countHECmd += 1;
                    break;
                default:
                    break;
            }
            log.Info($"Number command count:\r\n XT: {countXTCmd} --- HE: {countHECmd}");
        }
    }
}
