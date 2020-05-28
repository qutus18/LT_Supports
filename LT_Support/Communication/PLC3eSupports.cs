using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Support
{
    public static class PLC3e
    {
        //------------------------------------------------------------
        // Đầu vào là mảng word, số lượng phần tử cần chuyển đổi
        // Đầu ra là mảng các mảng bit[16] tương ứng với giá trị word đầu vào
        //------------------------------------------------------------
        public static bool[][] ConvertWordToBitArray(int[] inputInt16Data, int numberWordToConvert)
        {
            List<bool[]> outputList = new List<bool[]>();
            //------------------------------------------------------------
            // Chuyển từng Word sang mảng bit 16 phần tử
            //------------------------------------------------------------
            for (int index = 0; index < numberWordToConvert; index++)
            {
                bool[] boolArrConverted = new bool[16];
                if (index >= inputInt16Data.Length) break;
                for (int bitIndex = 0; bitIndex < 16; bitIndex++)
                {
                    //------------------------------------------------------------
                    // So sánh từng bit tương ứng để kiểm tra ON/OFF
                    //------------------------------------------------------------
                    boolArrConverted[bitIndex] = (inputInt16Data[index] & (int)(Math.Pow(2, bitIndex))) == 0 ? false : true;
                }
                //------------------------------------------------------------
                // Thêm mảng Bit[16] vào List đầu ra
                //------------------------------------------------------------
                outputList.Add(boolArrConverted);
            }
            //------------------------------------------------------------
            // Thêm mảng 0000 vào List nếu số lượng Word yêu cầu vượt quá lượng Data đầu vào
            //------------------------------------------------------------
            bool[] allFalseBitArr = new bool[16];
            while (outputList.Count < numberWordToConvert)
            {
                outputList.Add(allFalseBitArr);
            }
            //------------------------------------------------------------
            // Trả về List chuyển đổi thành dạng Arr
            //------------------------------------------------------------
            return outputList.ToArray();
        }

        //------------------------------------------------------------
        // So sánh để trả ra mảng xung sườn lên
        //------------------------------------------------------------
        public static bool[][] CompareBitArrarToRaisePulse(bool[][] oldBitArr, bool[][] currentBitArr)
        {
            //------------------------------------------------------------
            // Khởi tạo mảng Bit đầu ra
            //------------------------------------------------------------
            bool[][] outputBitArr = new bool[oldBitArr.Length][];
            for (int i = 0; i < oldBitArr.Length; i++)
            {
                outputBitArr[i] = new bool[16];
            }
            //------------------------------------------------------------
            // So sánh trả về Raise Pulse Bit
            //------------------------------------------------------------
            for (int index1 = 0; index1 < outputBitArr.Length; index1++)
            {
                for (int index2 = 0; index2 < 16; index2++)
                {
                    outputBitArr[index1][index2] = currentBitArr[index1][index2] == true ? (oldBitArr[index1][index2] == false ? true : false) : false;
                }
            }
            return outputBitArr;
        }

        //------------------------------------------------------------
        // So sánh bit để trả ra mảng xung sườn xuống
        //------------------------------------------------------------
        public static bool[][] CompareBitArrarToFallPulse(bool[][] oldBitArr, bool[][] currentBitArr)
        {
            //------------------------------------------------------------
            // Khởi tạo mảng Bit đầu ra
            //------------------------------------------------------------
            bool[][] outputBitArr = new bool[oldBitArr.Length][];
            for (int i = 0; i < oldBitArr.Length; i++)
            {
                outputBitArr[i] = new bool[16];
            }
            //------------------------------------------------------------
            // So sánh trả về Raise Pulse Bit
            //------------------------------------------------------------
            for (int index1 = 0; index1 < outputBitArr.Length; index1++)
            {
                for (int index2 = 0; index2 < 16; index2++)
                {
                    outputBitArr[index1][index2] = currentBitArr[index1][index2] == false ? (oldBitArr[index1][index2] == true ? true : false) : false;
                }
            }
            return outputBitArr;
        }

        /// <summary>
        /// Chuyển đổi int sang mảng byte - sử dụng để gửi Mc Protocol Message
        /// </summary>
        /// <param name="dx_Num"></param>
        /// <returns></returns>
        public static byte[] Int2ByteArray(int dx_Num)
        {
            return BitConverter.GetBytes(dx_Num);
        }

        /// <summary>
        /// Tính toán chuỗi gửi Dx sang PLC	dạng 3E_Frame
        /// </summary>
        public static byte[] Cal_WriteToPLCMessage_Dx_Multi_3E_Frame(int Dx_Num, int[] Dx_Data)
        {
            // Chuỗi kết nối mặc định ( Subheader - 50 00 // Network - 00 // PC No - FF // Destination Module - FF 03 // Station No - 00
            byte[] _connect_Arr = { 0x50, 0x00, 0x00, 0xFF, 0xFF, 0x03, 0x00 };   // Sẽ + Datalength + REQ message

            // Monitor time
            byte[] _motitor_Time_Arr = { 0x10, 0x00 };

            // Chuỗi bắt đầu
            byte[] _header = { 0x01, 0x14, 0x00, 0x00 };

            // Chuỗi tên biến
            byte[] _DxArr = new byte[3];
            Array.Copy(Int2ByteArray(Dx_Num), 0, _DxArr, 0, 3);

            // Chuỗi kết thúc : Device code + Number data (2 byte)
            int numberDataToSend = Dx_Data.Length;
            List<byte> _listTerminal = new List<byte>();
            _listTerminal.Add(0xA8);
            _listTerminal.AddRange(Int2ByteArray(numberDataToSend).Take(2).ToList());

            // Chuỗi giá trị biến
            List<byte> _listDxDataArr = new List<byte>();
            foreach (int item in Dx_Data)
            {
                _listDxDataArr.AddRange(Int2ByteArray(item).Take(2).ToList());
            }

            // REQ message - List
            List<byte> req_outList = _motitor_Time_Arr.ToList();
            req_outList.AddRange(_header.ToList());
            req_outList.AddRange(_DxArr.ToList());
            req_outList.AddRange(_listTerminal);
            req_outList.AddRange(_listDxDataArr);

            // Tính toán Datalength
            int data_length = req_outList.Count();
            byte[] _Data_Length_Arr = new byte[2];
            Array.Copy(Int2ByteArray(data_length), 0, _Data_Length_Arr, 0, 2);

            // Return message - List
            List<byte> return_List = _connect_Arr.ToList();
            return_List.AddRange(_Data_Length_Arr.ToList());
            return_List.AddRange(req_outList);

            return return_List.ToArray();
        }

        public static byte[] Cal_ReadFromPLCMessage_Dx_Multi_3E_Frame(int Dx_Num, int Dx_NumberData)
        {
            // Chuỗi kết nối mặc định ( Subheader - 50 00 // Network - 00 // PC No - FF // Destination Module - FF 03 // Station No - 00
            byte[] _connect_Arr = { 0x50, 0x00, 0x00, 0xFF, 0xFF, 0x03, 0x00 };   // Sẽ + Datalength + REQ message

            // Monitor time
            byte[] _motitor_Time_Arr = { 0x10, 0x00 };

            // Chuỗi bắt đầu
            byte[] _header = { 0x01, 0x04, 0x00, 0x00 };
            // Chuỗi tên biến
            byte[] _DxArr = new byte[3];
            Array.Copy(Int2ByteArray(Dx_Num), 0, _DxArr, 0, 3);
            // Chuỗi kết thúc : Device code + Number data (2 byte)
            byte[] _terminal = { 0xA8, 0x00, 0x00 };
            Array.Copy(Int2ByteArray(Dx_NumberData), 0, _terminal, 1, 2);

            // REQ message - List
            List<byte> req_outList = _motitor_Time_Arr.ToList();
            req_outList.AddRange(_header.ToList());
            req_outList.AddRange(_DxArr.ToList());
            req_outList.AddRange(_terminal.ToList());

            // Tính toán Datalength
            int data_length = req_outList.Count();
            byte[] _Data_Length_Arr = new byte[2];
            Array.Copy(Int2ByteArray(data_length), 0, _Data_Length_Arr, 0, 2);

            // Return message - List
            List<byte> return_List = _connect_Arr.ToList();
            return_List.AddRange(_Data_Length_Arr.ToList());
            return_List.AddRange(req_outList);

            return return_List.ToArray();
        }

        //------------------------------------------------------------
        // Chuyển đổi String sang arr dạng Int để gửi sang PLC
        //------------------------------------------------------------
        public static int[] StringToWordArr(string stringIN)
        {
            char[] charArrOfStringIN = stringIN.ToCharArray();
            List<byte> stringByteList = new List<byte>();
            List<int> stringIntList = new List<int>();
            //------------------------------------------------------------
            // Chuyển đổi String => Byte[]
            //------------------------------------------------------------
            foreach (char item in charArrOfStringIN)
            {
                stringByteList.Add(Convert.ToByte(item));
            }
            //------------------------------------------------------------
            // Chuyển đổi Byte[] => Int[]
            //------------------------------------------------------------
            if (stringByteList.Count % 2 == 1)
            {
                stringByteList.Add(0);
            }
            int numberWordConvert = stringByteList.Count / 2;
            byte[] stringByteArr = stringByteList.ToArray();

            for (int i = 0; i < numberWordConvert; i++)
            {
                stringIntList.Add((int)BitConverter.ToUInt16(stringByteArr, i * 2));
            }
            return stringIntList.ToArray();
        }

        //------------------------------------------------------------
        // Lấy giá trị Double Word từ 1 vị trí trong mảng Word
        //------------------------------------------------------------
        public static int GetDWordFromWordArr(int[] dataIN, int index)
        {
            if ((dataIN.Length - index) < 2) return -1;
            List<byte> dwordListByte = new List<byte>();
            dwordListByte.AddRange(Int2ByteArray(dataIN[index]).Take(2));
            dwordListByte.AddRange(Int2ByteArray(dataIN[index + 1]).Take(2).ToList());
            int outputDWord = BitConverter.ToInt32(dwordListByte.ToArray(), 0);
            return outputDWord;
        }

        //------------------------------------------------------------
        // Lấy giá trị String từ WordArr + Index + Length
        //------------------------------------------------------------
        public static string GetStringFromWordArr(int[] dataIN, int index, int lenght)
        {
            if ((dataIN.Length - index - lenght) < 2) return null;
            List<byte> stringListByte = new List<byte>();
            for (int i = index; i < index + lenght; i++)
            {

            stringListByte.AddRange(Int2ByteArray(dataIN[i]).Take(2));
            }
            string outputString = "";
            foreach (var item in stringListByte)
            {
                outputString += (char)item;
            }
            return outputString;
        }

        //------------------------------------------------------------
        // Chuyển DWord sang Word[2]
        //------------------------------------------------------------
        public static int[] DWordToWordArray(int dWordIN)
        {
            byte[] dWordByteArr = Int2ByteArray(dWordIN);
            int[] outWordArr = new int[2];
            outWordArr[0] = BitConverter.ToInt16(dWordByteArr, 0);
            outWordArr[1] = BitConverter.ToInt16(dWordByteArr, 2);
            return outWordArr;
        }
    }
}
