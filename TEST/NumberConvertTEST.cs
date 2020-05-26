using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                txtConvertString.Text = outAscii;
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
    }
}
