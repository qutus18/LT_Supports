using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace LT_Support.Communication
{
    public class MCProtocol3EFrame
    {
        private TcpClient PLCSocket;
        private Thread ContinuesReceiveThread;
        private IPEndPoint ServerEndPoint;
        public bool Connected { get; set; }

        //------------------------------------------------------------
        // Địa chỉ, độ dài vùng dữ liệu truyền thông với PLC
        //------------------------------------------------------------
        public string DataToPLCAddress { get; set; }
        public int InputOffset { get; set; }
        public string DataFromPLCAddress { get; set; }
        public int OutputOffset { get; set; }
        public int InWordData { get; set; }

        //------------------------------------------------------------
        // Event mất kết nối TCP
        //------------------------------------------------------------
        public delegate void TCP4DisconnectEventHandle();
        public TCP4DisconnectEventHandle ServerDisconnectEvent;

        //------------------------------------------------------------
        // Event gửi dữ liệu khi nhận được qua TCP
        //------------------------------------------------------------
        public delegate void TCP4ReceivedCommandEventHandle(string commandString);
        public TCP4ReceivedCommandEventHandle ReceivedCommandFromServerEvent;


        //------------------------------------------------------------
        // Khởi tạo
        //------------------------------------------------------------
        public MCProtocol3EFrame()
        {
            PLCSocket = null;
            DataToPLCAddress = "";
            DataFromPLCAddress = "";
        }

        public MCProtocol3EFrame(string plcAddress, int plcPort, string dataIn, int dataInLength, string dataOut, int dataOutLenght) : this()
        {
            UpdateDataLinkAddress(dataIn, dataInLength, dataOut, dataOutLenght);
            PLCClientInit(plcAddress, plcPort);
        }

        //------------------------------------------------------------
        // 1. Kết nối đến địa chỉ IP/Port của PLC tương ứng
        // 2. Mở thread nhận dữ liệu liên tục từ socket vừa kết nối
        //------------------------------------------------------------
        public void PLCClientInit(string ipAddress, int port)
        {
            //------------------------------------------------------------
            // Đóng socket cũ nếu đã tồn tại
            //------------------------------------------------------------
            if (PLCSocket != null)
            {
                PLCSocket.Client.Shutdown(SocketShutdown.Both);
                PLCSocket.Close();
                PLCSocket.Dispose();
            }
            //------------------------------------------------------------
            // Kết nối đến socket
            //------------------------------------------------------------
            try
            {
                IPAddress serverAddress = IPAddress.Parse(ipAddress);
                ServerEndPoint = new IPEndPoint(serverAddress, port);
                PLCSocket = new TcpClient(AddressFamily.InterNetwork);
                PLCSocket.Connect(ServerEndPoint);
                PLCSocket.ReceiveTimeout = 1000;
                Connected = true;
            }
            catch
            {
                Connected = false;
            }
            //------------------------------------------------------------
            // Nếu đã kết nối thành công, tạo Thread nhận dữ liệu từ socket
            //------------------------------------------------------------
            if (Connected)
            {
                ContinuesReceiveThread = new Thread(new ThreadStart(ContinuesReceiveFromTCPSocket));
                ContinuesReceiveThread.IsBackground = true;
                ContinuesReceiveThread.Start();
            }
        }

        //------------------------------------------------------------
        // Nhận dữ liệu từ Server và gửi Event
        //------------------------------------------------------------
        private void ContinuesReceiveFromTCPSocket()
        {
            while (true)
            {
                try
                {

                }
                catch
                {

                }
                finally
                {
                    Thread.Sleep(10);
                }
            }
        }

        //------------------------------------------------------------
        // Nhận dữ liệu từ PLC. Bắt đầu từ wordAddress, số lượng Word: numberData
        //------------------------------------------------------------
        public int[] GET_Data_PLC(int wordAddress, int numberData)
        {
            //------------------------------------------------------------
            // Tạo stream từ kết nối TCP Client
            //------------------------------------------------------------ 
            NetworkStream stream = PLCSocket.GetStream();
            //------------------------------------------------------------
            // Tạo dữ liệu đúng định dạng
            // Gửi qua kết nối stream sang PLC
            //------------------------------------------------------------
            byte[] sent_Data = PLC3e.Cal_ReadFromPLCMessage_Dx_Multi_3E_Frame(wordAddress, numberData);
            stream.Write(sent_Data, 0, sent_Data.Length);
            stream.Flush();
            //------------------------------------------------------------
            // Nhận dữ liệu xác nhận từ PLC
            //------------------------------------------------------------
            byte[] receive_Data = new byte[0x3e8];
            stream.ReadTimeout = 500;
            try
            {
                stream.Read(receive_Data, 0, 0x3e8);
            }
            catch { return null; }
            //------------------------------------------------------------
            // Check xác nhận từ PLC
            // So sánh chuỗi để xác nhận không có lỗi gửi nhận
            //------------------------------------------------------------
            byte[] compareArr = { 0xD0, 0x00, 0x00, 0xFF, 0xFF, 0x03, 0x00, 0x02, 0x00, 0x00, 0x00 };
            Array.Copy(BitConverter.GetBytes(numberData * 2 + 2), 0, compareArr, 7, 2);
            byte[] receiveArr = new byte[compareArr.Length];
            Array.Copy(receive_Data, receiveArr, receiveArr.Length);
            if (!receiveArr.SequenceEqual(compareArr)) return null;
            //------------------------------------------------------------
            // Tách dữ liệu PLC từ chuỗi nhận về
            //------------------------------------------------------------
            byte[] dataOutArr_byte = new byte[numberData * 2];
            Array.Copy(receive_Data, compareArr.Length, dataOutArr_byte, 0, dataOutArr_byte.Length);
            //------------------------------------------------------------
            // Chuyển dữ liệu về kiểu interger
            //------------------------------------------------------------
            int[] dataOutArr = new int[numberData];
            for (int i = 0; i < numberData; i++)
            {
                dataOutArr[i] = (int)BitConverter.ToInt16(dataOutArr_byte, i * 2);
            }
            //------------------------------------------------------------
            // Gửi chuỗi interger đọc từ PLC
            //------------------------------------------------------------
            return dataOutArr;
        }


        //------------------------------------------------------------
        // Cập nhật địa chỉ gửi nhận dữ liệu với PLC
        //------------------------------------------------------------
        public void UpdateDataLinkAddress(string dataIn, int inOffset, string dataOut, int outOffset)
        {
            DataToPLCAddress = dataOut;
            DataFromPLCAddress = dataIn;
        }

        private bool CheckConnection()
        {
            //------------------------------------------------------------
            // checker1 kiểm tra kết nối, true nếu Closed, hoặc Data availble
            // checker2 kiểm tra true nếu không có Data availble
            //------------------------------------------------------------
            bool checker1 = false;
            bool checker2 = false;
            try
            {

                checker1 = PLCSocket.Client.Poll(1000, SelectMode.SelectRead);
                checker2 = (PLCSocket.Available == 0);
            }
            catch { }

            // Nếu cả 2 cùng true => Kết nối đang đóng
            if (checker1 && checker2)
            {
                Connected = false;
                return false;
            }
            else
            {
                //------------------------------------------------------------
                // Kiểm tra lại bằng cách gửi dữ liệu (kiểm tra đứt cáp, ngắt đột ngột phía host)
                //------------------------------------------------------------
                try
                {
                    PLCSocket.Client.Send(new byte[] { 1 });
                }
                catch
                {
                    Connected = false;
                    return false;
                }
                Connected = true;
                return true;
            }
        }

        //------------------------------------------------------------
        // Thử kết nối lại vào Server 5 lần
        //------------------------------------------------------------
        private void Reconnect5Times()
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    //------------------------------------------------------------
                    // Đóng socket cũ nếu đã tồn tại
                    //------------------------------------------------------------
                    if (PLCSocket != null)
                    {
                        PLCSocket.Client.Shutdown(SocketShutdown.Both);
                        PLCSocket.Close();
                        PLCSocket.Dispose();
                    }
                    //------------------------------------------------------------
                    // Kết nối đến socket
                    //------------------------------------------------------------
                    try
                    {
                        PLCSocket = new TcpClient(AddressFamily.InterNetwork);
                        PLCSocket.Connect(ServerEndPoint);
                        PLCSocket.ReceiveTimeout = 1000;
                        Connected = true;
                        break;
                    }
                    catch
                    {
                        Connected = false;
                    }
                }
                catch
                {

                }
            }
        }

    }
}
