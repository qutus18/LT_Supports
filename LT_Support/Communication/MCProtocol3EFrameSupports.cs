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
        /// Lấy giá trị bit theo số thứ tự bit nhập vào
        /// </summary>
        /// <param name="Dx"></param>
        /// <param name="bitNum"></param>
        /// <returns></returns>
        public static Boolean Get_Bit(int Dx, int bitNum)
        {
            int DxTemp = Dx;
            DxTemp = (DxTemp & (int)(Math.Pow(2, bitNum)));
            if (DxTemp == 0) return false;
            return true;
        }

        /// <summary>
        /// Tính toán chuỗi gửi Dx sang PLC
        /// </summary>
        /// <param name="Dx_Num"></param>
        /// <param name="Dx_Data"></param>
        /// <returns></returns>
        public static byte[] Cal_WriteToPLCMessage_Dx_Single_1E_Frame(int Dx_Num, int Dx_Data)
        {
            // Chuỗi bắt đầu
            byte[] _header = { 0x03, 0xFF, 0x0A, 0x00 };
            // Chuỗi kết thúc
            byte[] _terminal = { 0x20, 0x44, 0x01, 0x00 };
            // Chuỗi tên biến
            byte[] _DxArr = Int2ByteArray(Dx_Num);
            // Chuỗi giá trị biến
            byte[] _DxDataArr = new byte[2];
            Array.Copy(Int2ByteArray(Dx_Data), 0, _DxDataArr, 0, 2);
            // Trả về
            List<byte> outList = _header.ToList();
            outList.AddRange(_DxArr.ToList());
            outList.AddRange(_terminal.ToList());
            outList.AddRange(_DxDataArr.ToList());

            return outList.ToArray();
        }

        /// <summary>
        /// Tính toán chuối gửi để nhận numberOfData Dx từ PLC
        /// </summary>
        /// <param name="Dx_Num"></param>
        /// <param name="num_NumberOfData"></param>
        /// <returns></returns>
        public static byte[] Cal_ReadFromPLCMessage_Dx_Number_1E_Frame(int Dx_Num, int num_NumberOfData)
        {
            // Chuỗi bắt đầu
            byte[] _header = { 0x01, 0xFF, 0x0A, 0x00 };
            // Chuỗi kết thúc
            byte[] _terminal = { 0x20, 0x44 };
            // Chuỗi tên biến
            byte[] _DxArr = Int2ByteArray(Dx_Num);
            // Chuỗi số lượng giá trị cần lấy
            byte[] _DxNumArr = new byte[2];
            Array.Copy(Int2ByteArray(num_NumberOfData), 0, _DxNumArr, 0, 2);
            // Trả về
            List<byte> outList = _header.ToList();
            outList.AddRange(_DxArr.ToList());
            outList.AddRange(_terminal.ToList());
            outList.AddRange(_DxNumArr.ToList());
            return outList.ToArray();
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
        /// <param name="Dx_Num"></param>
        /// <param name="Dx_Data"></param>
        /// <returns></returns>
        public static byte[] Cal_WriteToPLCMessage_Dx_Single_3E_Frame(int Dx_Num, int Dx_Data)
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
            byte[] _terminal = { 0xA8, 0x01, 0X00 };

            // Chuỗi giá trị biến
            byte[] _DxDataArr = new byte[2];
            Array.Copy(Int2ByteArray(Dx_Data), 0, _DxDataArr, 0, 2);

            // REQ message - List
            List<byte> req_outList = _motitor_Time_Arr.ToList();
            req_outList.AddRange(_header.ToList());
            req_outList.AddRange(_DxArr.ToList());
            req_outList.AddRange(_terminal.ToList());
            req_outList.AddRange(_DxDataArr.ToList());

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
    }
}
