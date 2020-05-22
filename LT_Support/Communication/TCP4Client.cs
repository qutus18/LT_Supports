using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;

namespace LT_Support
{
    public class TCP4Client
    {
        private Thread ContinuesReceiveThread;
        private IPEndPoint ServerEndPoint;
        private Socket TCP4Socket;
        public bool Connected { get; set; }

        //------------------------------------------------------------
        // Event gửi dữ liệu khi nhận được qua TCP
        //------------------------------------------------------------
        public delegate void TCP4ReceivedEventHandle(string receivedString);
        public TCP4ReceivedEventHandle ReceivedStringFromServerEvent;

        //------------------------------------------------------------
        // Event gửi dữ liệu khi nhận được qua TCP
        //------------------------------------------------------------
        public delegate void TCP4DisconnectEventHandle();
        public TCP4DisconnectEventHandle ServerDisconnectEvent;

        //------------------------------------------------------------
        // 1. Kết nối đến địa chỉ IP/Port tương ứng
        // 2. Mở thread nhận dữ liệu liên tục từ socket vừa kết nối
        //------------------------------------------------------------
        public TCP4Client(string ipAddress, int port)
        {
            //------------------------------------------------------------
            // Đóng socket cũ nếu đã tồn tại
            //------------------------------------------------------------
            if (TCP4Socket != null)
            {
                TCP4Socket.Shutdown(SocketShutdown.Both);
                TCP4Socket.Close();
            }
            //------------------------------------------------------------
            // Kết nối đến socket
            //------------------------------------------------------------
            try
            {
                IPAddress serverAddress = IPAddress.Parse(ipAddress);
                ServerEndPoint = new IPEndPoint(serverAddress, port);
                TCP4Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                TCP4Socket.Connect(ServerEndPoint);
                TCP4Socket.ReceiveTimeout = 1000;
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
                byte[] buffer = new byte[0x3e8];
                try
                {
                    int numberByteReceive = TCP4Socket.Receive(buffer, buffer.Length, SocketFlags.None);
                    if (numberByteReceive > 5)
                    {
                        string receivedString = Encoding.ASCII.GetString(buffer).Replace("\r\n", "").Trim();
                        ReceivedStringFromServerEvent?.Invoke(receivedString);
                    }
                    else
                    {
                        throw new Exception("Receive non charector");
                    }
                }
                catch
                {
                    if (Connected)
                    {
                        // First check
                        if (CheckConnection() == false)
                        {
                            Reconnect5Times();
                        }
                        // Final check
                        if (CheckConnection() == false)
                        {
                            ServerDisconnectEvent?.Invoke();
                        }
                    }
                }
                finally
                {
                    Thread.Sleep(10);
                }
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
                    if (TCP4Socket != null)
                    {
                        TCP4Socket.Shutdown(SocketShutdown.Both);
                        TCP4Socket.Close();
                    }
                    //------------------------------------------------------------
                    // Kết nối đến socket
                    //------------------------------------------------------------
                    try
                    {
                        TCP4Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        TCP4Socket.Connect(ServerEndPoint);
                        TCP4Socket.ReceiveTimeout = 1000;
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
                checker1 = TCP4Socket.Poll(1000, SelectMode.SelectRead);
                checker2 = (TCP4Socket.Available == 0);
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
                    TCP4Socket.Send(new byte[] { 1 });
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
        // Đóng Socket và Thread
        //------------------------------------------------------------
        public void Dispose()
        {
            try
            {
                ContinuesReceiveThread.Abort();
            }
            catch { }
            try
            {
                TCP4Socket.Shutdown(SocketShutdown.Both);
                TCP4Socket.Close();
                TCP4Socket.Dispose();
            }
            catch { }

        }
    }
}
