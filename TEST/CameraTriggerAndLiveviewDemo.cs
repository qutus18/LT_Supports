using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using LT_Testing;
using System.Threading;

namespace TEST
{
    public partial class CameraTriggerAndLiveviewDemo : Form
    {
        public Dictionary<string, List<string>> DictDevicesAvailable;
        private HWindow Window;
        HImage ImageMain;
        HFramegrabber CameraFrameGrabber01;
        public CameraTriggerAndLiveviewDemo()
        {
            InitializeComponent();
            DictDevicesAvailable = new Dictionary<string, List<string>>();
            Window = hSWindow1.HalconWindow;
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

        private async void btnGetDevices_Click(object sender, EventArgs e)
        {
            cbDeviceList.Items.Clear();
            cbInterfaceList.Items.Clear();
            List<string> interfaceListReturn = new List<string>();
            await Task.Run(() =>
            {
                interfaceListReturn = getAvailableInterfaces();
            })
            .ContinueWith((previousTask) =>
            {
                if (interfaceListReturn.Count > 0)
                {
                    cbInterfaceList.Items.AddRange(interfaceListReturn.ToArray());
                    cbInterfaceList.SelectedIndex = 0;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void cbInterfaceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDeviceList.Items.Clear();
            try
            {
                List<string> deviceListReturn = DictDevicesAvailable[cbInterfaceList.SelectedItem.ToString()];
                if (deviceListReturn.Count > 0)
                {
                    cbDeviceList.Items.AddRange(deviceListReturn.ToArray());
                    cbDeviceList.SelectedIndex = 0;
                }
            }
            catch { }
        }

        private void btnConnectDevice_Click(object sender, EventArgs e)
        {
            bool isFileInterface = false;
            string regexDevicePattern = @"(device:)(.+?)(\s\|)";
            string regexGenericPattern = @"(suggestion:)(.+)(\s\|)";
            string deviceInfo = cbDeviceList.SelectedItem.ToString();
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
            string interfaceName = cbInterfaceList.SelectedItem.ToString();
            if (interfaceName == "File")
            {
                isFileInterface = true;
            }
            if (!isFileInterface)
            {
                CameraFrameGrabber01 = new HFramegrabber(interfaceName, 1, 1, 0, 0, 0, 0, "default", new HTuple(-1), new HTuple("default"), generic, "default", new HTuple("default"), new HTuple(device), new HTuple(-1), new HTuple(-1));
            }
            else
            {
                CameraFrameGrabber01 = null;
            }
            //------------------------------------------------------------
            // Set Window display image 
            //------------------------------------------------------------
            string ImgType;
            int ImgWidth, ImgHeight;
            HImage Img;
            if (!isFileInterface)
            {
                Img = CameraFrameGrabber01.GrabImageAsync(1);
            }
            else
            {
                if (ListImageFiles.Count > 0)
                {
                    HOperatorSet.ReadImage(out HObject imageRead, ListImageFiles[0]);
                    Img = new HImage(imageRead);
                }
                else
                {
                    Img = new HImage("byte", 512, 512);
                }
            }

            Img.GetImagePointer1(out ImgType, out ImgWidth, out ImgHeight);
            Window.SetPart(0, 0, ImgHeight - 1, ImgWidth - 1);
            Img.DispObj(Window);
            Img.Dispose();
            Window.SetDraw("margin");
            Window.SetLineWidth(5);
            Window.SetColor(new HTuple("blue"));
        }

        public int IndexInListImageFile = -1;
        private void btnGrabImage_Click(object sender, EventArgs e)
        {
            if (cbInterfaceList.SelectedItem.ToString() != "File")
            {
                ImageMain = CameraFrameGrabber01.GrabImage();
                ImageMain.DispObj(Window);
            }
            else
            {
                IndexInListImageFile++;
                if (IndexInListImageFile >= ListImageFiles.Count)
                {
                    IndexInListImageFile = 0;
                }
                HOperatorSet.ReadImage(out HObject ImageRead, ListImageFiles[IndexInListImageFile]);
                ImageMain = new HImage(ImageRead);
                ImageMain.DispObj(Window);
            }
        }

        private async void btnProcTest_Click(object sender, EventArgs e)
        {
            List<double> result1 = new List<double>();
            List<double> result2 = new List<double>();
            var watch = System.Diagnostics.Stopwatch.StartNew();

            Task cal1 = new Task(() =>
            {
                for (int i = 0; i < 500; i++)
                {
                    Mul_MultiThread_Test.Proc_Mul_Testing_MultiThread(new HTuple(i), new HTuple(100), out HTuple result);
                    result1.Add(result.D);
                    //Thread.Sleep(10);
                }
            });

            Task cal2 = new Task(() =>
            {
                for (int j = 0; j < 500; j++)
                {
                    Mul_MultiThread_Test.Proc_Mul_Testing_MultiThread(new HTuple(j), new HTuple(10), out HTuple result);
                    result2.Add(result.D);
                    //Thread.Sleep(10);
                }
            });
            cal1.Start();
            cal2.Start();

            await Task.WhenAll(cal1, cal2);

            var outTime = watch.ElapsedMilliseconds;
            Console.WriteLine("_______________________________________________________________________________");
            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine(result1[i] + "-" + result2[i]);
            }
            Console.WriteLine("IN Time: " + outTime.ToString());

        }

        public List<string> ListImageFiles = new List<string>();
        private void cbDeviceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> templateListFiles = new List<string>();
            if (cbInterfaceList.SelectedItem.ToString() == "File")
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.ShowDialog();
                if (openFileDialog.FileNames != null)
                {
                    foreach (string item in openFileDialog.FileNames)
                    {
                        templateListFiles.Add(item);
                    }
                }
            }
            if (templateListFiles.Count > 0)
            {
                ListImageFiles = templateListFiles;
            }
        }

        public HDrawingObject ROIFindDataCodeDrawing;
        public HRegion ROIFindDataCode;
        private void btnEditROI_Click(object sender, EventArgs e)
        {
            if (ROIFindDataCodeDrawing == null)
            {
                ROIFindDataCodeDrawing = new HDrawingObject(100, 100, 300, 300);
            }
            try
            {
                HOperatorSet.AttachDrawingObjectToWindow(Window, ROIFindDataCodeDrawing);
            }
            catch
            {

            }
        }

        private void btnDoneEditROI_Click(object sender, EventArgs e)
        {
            try
            {
                HOperatorSet.DetachDrawingObjectFromWindow(Window, ROIFindDataCodeDrawing);
                HOperatorSet.GetDrawingObjectParams(ROIFindDataCodeDrawing, new HTuple("row1", "column1", "row2", "column2"), out HTuple genParams);
                ROIFindDataCode = new HRegion(new HTuple(genParams[0].I), genParams[1], genParams[2], genParams[3]);
                ROIFindDataCode.DispObj(Window);
            }
            catch { }
        }

        public HDataCode2D MainDataCode2D;
        private void btnFindCode_Click(object sender, EventArgs e)
        {
            if (ROIFindDataCode == null)
            {
                MessageBox.Show("Set ROI First!");
                return;
            }

            if (MainDataCode2D == null)
            {
                Mul_MultiThread_Test.Init_Data_Code2D_Max(out HTuple DataCodeHandle);
                MainDataCode2D = new HDataCode2D(DataCodeHandle.H);
            }

            Mul_MultiThread_Test.Find_Data_Code2D_Max_Recognition(ImageMain, ROIFindDataCode, out HObject imageReduce, out HObject codeMargin, MainDataCode2D, new HTuple(2), out HTuple resultCodeHandles, out HTuple decodedDataStrings);
            ImageMain.DispObj(Window);
            ROIFindDataCode.DispObj(Window);
            HXLD xldMarginCode = new HXLD(codeMargin);
            xldMarginCode.DispXld(Window);
            codeMargin.DispObj(Window);
            Window.DispText(decodedDataStrings, "window", new HTuple(10), new HTuple(10), new HTuple("black"), new HTuple(), new HTuple());
        }
    }
}
