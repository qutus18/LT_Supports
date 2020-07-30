using HalconDotNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace LT_Support.Forms
{
    public partial class CameraConnect : Form
    {
        public Dictionary<string, List<string>> DictDevicesAvailable;
        public HFramegrabber MainFrameGrabber;
        private HWindow MainWindow;

        // Status
        private bool CameraConnected = false;
        private bool LiveViewMode = false;

        // Mutex
        Mutex GrabMutex;
        Mutex ProcessImageMutex;
        ArrayList imgList;

        // Event
        AutoResetEvent ImageInEvent;
        ManualResetEvent StopLiveViewEvent;

        // Thread
        Thread ImageLiveAcqThread;
        Thread ImageLiveDisplayThread;

        // Buffer live image
        const int MAX_BUFFERS = 5;

        public Acquision.CameraSet CurrentCameraSet;

        //------------------------------------------------------------
        // Khởi tạo contructor
        //------------------------------------------------------------
        public CameraConnect()
        {
            InitializeComponent();
            MainWindow = hSmartWD.HalconWindow;
            DictDevicesAvailable = new Dictionary<string, List<string>>();

            GrabMutex = new Mutex();
            ProcessImageMutex = new Mutex();

            ImageInEvent = new AutoResetEvent(false);
            StopLiveViewEvent = new ManualResetEvent(false);

            imgList = new ArrayList();

            CurrentCameraSet = new Acquision.CameraSet();
        }

        private async void FindCameraInterface(object sender, ElapsedEventArgs e)
        {
            cbDevices.Items.Clear();
            cbInterfaces.Items.Clear();

            List<string> interfaceListReturn = new List<string>();
            await Task.Run(() =>
            {
                interfaceListReturn = getAvailableInterfaces();
            })
            .ContinueWith((previousTask) =>
            {
                if (interfaceListReturn.Count > 0)
                {
                    cbInterfaces.Items.AddRange(interfaceListReturn.ToArray());
                    cbInterfaces.SelectedIndex = 0;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void pbRefresh_Click(object sender, EventArgs e)
        {
            pbRefresh.Enabled = false;
            pbRefresh.BackColor = Color.LightGray;
            this.Refresh();

            FindCameraInterface(null, null);

            pbRefresh.Enabled = true;
            pbRefresh.BackColor = Color.White;
        }

        private List<string> getAvailableInterfaces()
        {
            DictDevicesAvailable.Clear();
            //------------------------------------------------------------
            // Khai báo thư mục mặc định của Halcon
            //------------------------------------------------------------
            List<string> availableInterfaces = new List<string>();
            string halconRoot = Environment.GetEnvironmentVariable("HALCONROOT");
            string halconArch = Environment.GetEnvironmentVariable("HALCONARCH");
            string a = halconRoot + "/bin/" + halconArch;

            //------------------------------------------------------------
            // Lấy tất cả Interface của Halcon
            //------------------------------------------------------------
            var acquisitionInterfaces = Directory.EnumerateFiles(a, "hacq*.dll");

            //------------------------------------------------------------
            // Với mỗi Interface, tìm tất cả Device không phải dạng XL
            //------------------------------------------------------------
            foreach (string item in acquisitionInterfaces)
            {
                // Extract the interface name with an regular expression
                string interfaceName = Regex.Match(item, "hAcq(.+)(?:\\.dll)").Groups[1].Value;
                if (!interfaceName.EndsWith("xl"))
                {
                    try
                    {
                        // Querry available devices
                        HTuple devices;
                        HInfo.InfoFramegrabber(interfaceName, "info_boards", out devices);
                        // In case that devices were found add it to the available interfaces
                        if (devices.Length > 0)
                        {
                            availableInterfaces.Add(interfaceName);
                            DictDevicesAvailable.Add(interfaceName, devices.SArr.ToList());
                        }
                    }
                    catch (Exception ex)
                    { }
                }
            }

            return availableInterfaces;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            // Kiểm tra lệnh kết nối hay ngắt kết nối
            //------------------------------------------------------------
            if (CameraConnected)
            {
                MainFrameGrabber.Dispose();
                btnConnect.Text = "  Connect";
                CameraConnected = false;
                return;
            }
            //------------------------------------------------------------
            // Lấy giá trị device và generic cho hàm kết nối camera
            //------------------------------------------------------------
            bool isFileInterface = false;
            string regexDevicePattern = @"(device:)(.+?)(\s\|)";
            string regexGenericPattern = @"(suggestion:)(.+)(\s\|)";
            string deviceInfo = cbDevices.SelectedItem.ToString();
            string device = Regex.Match(deviceInfo, regexDevicePattern).Groups[2].Value;
            if (device.Length < 3)
            {
                device = "default";
            }
            HTuple generic;
            if (deviceInfo.Contains("misconfigured"))
            {
                generic = Regex.Match(deviceInfo, regexGenericPattern).Groups[2].Value;
            }
            else
            {
                generic = new HTuple(-1);
            }
            //------------------------------------------------------------
            // Check if is File Interface
            //------------------------------------------------------------
            string interfaceName = cbInterfaces.SelectedItem.ToString();
            if (interfaceName == "File")
            {
                isFileInterface = true;
            }
            if (!isFileInterface)
            {
                try
                {
                    MainFrameGrabber = new HFramegrabber(interfaceName, 1, 1, 0, 0, 0, 0, "default",
                                                         new HTuple(-1), new HTuple("default"), generic,
                                                         "default", new HTuple("default"), new HTuple(device),
                                                         new HTuple(-1), new HTuple(-1));
                }
                catch
                {
                    return;
                }
                try
                {
                    //------------------------------------------------------------
                    // Lấy giá trị Gain, Exposure từ Camera
                    //------------------------------------------------------------
                    HTuple gain = MainFrameGrabber.GetFramegrabberParam("Gain");
                    HTuple exposureTime = MainFrameGrabber.GetFramegrabberParam("ExposureTime");
                    //------------------------------------------------------------
                    // Hiển thị lên ô cài đặt
                    //------------------------------------------------------------
                    txtGainValue.Text = gain.D.ToString();
                    txtExposureValue.Text = exposureTime.D.ToString();
                    //------------------------------------------------------------
                    // Thay đổi trạng thái kết nối
                    //------------------------------------------------------------
                    btnConnect.Text = "  Connected";
                    CameraConnected = true;
                    //------------------------------------------------------------
                    // Cập nhật CameraSet
                    //------------------------------------------------------------
                    CurrentCameraSet.Device = device;
                    CurrentCameraSet.Generic = generic;
                }
                catch (Exception ex) { }
            }
            else
            {
                MainFrameGrabber = null;
            }
            //------------------------------------------------------------
            // Set Window display image 
            //------------------------------------------------------------
            string ImgType;
            int ImgWidth, ImgHeight;
            HImage Img;
            Img = MainFrameGrabber.GrabImageAsync(1);
            Img.GetImagePointer1(out ImgType, out ImgWidth, out ImgHeight);
            MainWindow.SetPart(0, 0, ImgHeight - 1, ImgWidth - 1);
            Img.DispObj(MainWindow);
            Img.Dispose();
        }

        //------------------------------------------------------------
        // Khi thay đổi Interface camera, thì cập nhật list Camera trong combobox Devices
        //------------------------------------------------------------
        private void cbInterfaceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDevices.Items.Clear();
            try
            {
                List<string> deviceListReturn = DictDevicesAvailable[cbInterfaces.SelectedItem.ToString()];
                if (deviceListReturn.Count > 0)
                {
                    cbDevices.Items.AddRange(deviceListReturn.ToArray());
                    cbDevices.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void CamParamsChanged(object sender, EventArgs e)
        {
            double gainValue = 0, exposureValue = 0;
            //------------------------------------------------------------
            // Cập nhật giá trị Gain
            //------------------------------------------------------------
            try
            {
                gainValue = Double.Parse(txtGainValue.Text);
                MainFrameGrabber.SetFramegrabberParam(new HTuple("Gain"),
                                                        new HTuple(gainValue));
            }
            catch { }
            //------------------------------------------------------------
            // Cập nhật giá trị Exposure
            //------------------------------------------------------------
            try
            {
                exposureValue = Double.Parse(txtExposureValue.Text);
                MainFrameGrabber.SetFramegrabberParam(new HTuple("ExposureTime"),
                                                        new HTuple(exposureValue));
            }
            catch { }
            //------------------------------------------------------------
            // Chụp ảnh mới theo thay đổi
            //------------------------------------------------------------
            GrabMutex.WaitOne();
            HImage Img;
            Img = MainFrameGrabber.GrabImage();
            Img.DispObj(MainWindow);
            Img.Dispose();
            GrabMutex.ReleaseMutex();
            //------------------------------------------------------------
            // Cập nhật CameraSet
            //------------------------------------------------------------
            CurrentCameraSet.Gain = gainValue;
            CurrentCameraSet.Exposure = exposureValue;
        }

        private void pbDone_Click(object sender, EventArgs e)
        {
            try
            {
                MainFrameGrabber.Dispose();
            }
            catch { }
            if (ImageLiveAcqThread != null) ImageLiveAcqThread.Abort();
            if (ImageLiveDisplayThread != null) ImageLiveDisplayThread.Abort();

            this.Dispose();

        }

        private void CameraLiveAcq()
        {
            //------------------------------------------------------------
            // INIT
            //------------------------------------------------------------

            while (true)
            {
                //------------------------------------------------------------
                // Grab Image
                //------------------------------------------------------------
                GrabMutex.WaitOne();

                HObject Img;
                Img = MainFrameGrabber.GrabImageAsync(-1);

                GrabMutex.ReleaseMutex();

                //------------------------------------------------------------
                // Add Image to List
                //------------------------------------------------------------
                ProcessImageMutex.WaitOne();
                if (imgList.Count < MAX_BUFFERS)
                {
                    imgList.Add(Img);
                    ImageInEvent.Set();
                }
                else
                {
                    Img.Dispose();
                }
                ProcessImageMutex.ReleaseMutex();

                if (StopLiveViewEvent.WaitOne(0, true))
                {
                    break;
                }
            }
            return;
        }

        private void DisplayLiveImage()
        {
            while (ImageInEvent.WaitOne())
            {
                //------------------------------------------------------------
                // Lấy ảnh từ list buffer
                //------------------------------------------------------------
                ProcessImageMutex.WaitOne();
                HObject Img = (HObject)imgList[0];
                imgList.Remove(Img);
                ProcessImageMutex.ReleaseMutex();

                Invoke(new MethodInvoker(delegate
                {
                    MainWindow.DispObj(Img);
                    Img.Dispose();
                }));

                if (StopLiveViewEvent.WaitOne(0, true))
                {
                    break;
                }
            }

            InitValue();
            return;
        }

        //------------------------------------------------------------
        // Khởi tạo lại các giá trị khi kết thúc live view
        //------------------------------------------------------------
        private void InitValue()
        {
            ImageInEvent.Reset();
            StopLiveViewEvent.Reset();

            int length = imgList.Count;
            for (int i = 0; i < length; i++)
            {
                HObject image = (HObject)imgList[0];
                imgList.Remove(image);
                image.Dispose();
            }
        }

        private void btnLive_Click(object sender, EventArgs e)
        {
            if (MainFrameGrabber == null) return;

            if (!LiveViewMode)
            {
                InitValue();
                ImageLiveAcqThread = new Thread(CameraLiveAcq);
                ImageLiveDisplayThread = new Thread(DisplayLiveImage);
                ImageLiveAcqThread.Start();
                ImageLiveDisplayThread.Start();
                btnLive.BackColor = Color.DarkOrange;
                LiveViewMode = true;
            }
            else
            {
                StopLiveViewEvent.Set();
                btnLive.BackColor = Color.Gray;
                LiveViewMode = false;
            }
        }
    }
}
