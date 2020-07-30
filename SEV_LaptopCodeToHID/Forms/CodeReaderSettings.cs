using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using HalconDotNet;
using System.IO;
using Newtonsoft.Json;

namespace SEV_LaptopCodeToHID.Forms
{
    public partial class CodeReaderSettings : Form
    {
        public List<Halcon.VisionNew> VisionDevices;
        public Halcon.VisionNew CurrentVisionDevice;
        public Halcon.CodeReader CurrentCodeReader;
        public List<string> ListCodeReader;
        public HWindow DisplayWD;
        public string SaveUrl;

        public CodeReaderSettings(string url)
        {
            InitializeComponent();
            VisionDevices = new List<Halcon.VisionNew>();

            // Reload saved VisionDevices
            SaveUrl = url;
            LoadAllVision(SaveUrl);
            cbCameraList.SelectedIndex = 0;
            DisplayWD = hSmartWD.HalconWindow;
        }

        public CodeReaderSettings() : this("D:\\RTCVision")
        {
        }

        //------------------------------------------------------------
        // Load 4 Vision Device đã lưu (hoặc tạo mới nếu chưa có)
        // Thêm vào list VisionDevices
        //------------------------------------------------------------
        private void LoadAllVision(string url)
        {
            if (!Directory.Exists(url))
            {
                Directory.CreateDirectory(url);
            }

            for (int index = 0; index < 1; index++)
            {
                Halcon.VisionNew tempVisionDevice;
                string visionUrl = url + $"\\{index}\\VisionConfig.json";
                if (File.Exists(visionUrl))
                {
                    string jsonStr = File.ReadAllText(visionUrl);
                    tempVisionDevice = JsonConvert.DeserializeObject<Halcon.VisionNew>(jsonStr);
                }
                else
                {
                    tempVisionDevice = new Halcon.VisionNew(url, 0);
                }
                VisionDevices.Add(tempVisionDevice);
            }
        }

        //------------------------------------------------------------
        // Ghi tất cả các Vision device hiện tại ra file
        // theo đường dẫn đã chọn
        //------------------------------------------------------------
        private void SaveAllVision()
        {
            for (int index = 0; index < 1; index++)
            {
                if (index < VisionDevices.Count)
                {
                    VisionDevices[index].Url = SaveUrl + $"\\{index}";
                    VisionDevices[index].Save();
                    string visionUrl = SaveUrl + $"\\VisionConfig_{index}.json";
                    string jsonStr = JsonConvert.SerializeObject(VisionDevices[index]);
                    File.WriteAllText(visionUrl, jsonStr);
                }
            }
        }

        private void cbCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cameraIndex = cbCameraList.SelectedIndex;
            CurrentVisionDevice = VisionDevices[cameraIndex];
            ListCodeReader = CurrentVisionDevice.ListCodeReaderName;
            listboxCodeList.DataSource = ListCodeReader;

            // Hiển thị dữ liệu của Code Reader đầu tiên nếu có
            // Nếu chưa có Code Reader nào thì xóa hiển thị
            if (ListCodeReader.Count > 0)
            {
                listboxCodeList.SelectedIndex = 0;
            }
            else
            {
                ClearDisplayCodeParams();
            }
        }

        private void ClearDisplayCodeParams()
        {
            txtName.Text = "";
            cbCodeType.SelectedIndex = -1;
            txtLengthMin.Text = "";
            txtLengthMax.Text = "";
            txtFilterString.Text = "";
        }

        private void btnAddCode_Click(object sender, EventArgs e)
        {
            if (CurrentVisionDevice == null) return;
            CurrentVisionDevice.AddNewCodeReader();
            ListCodeReader = CurrentVisionDevice.ListCodeReaderName;
            listboxCodeList.DataSource = ListCodeReader;
            listboxCodeList.SelectedIndex = ListCodeReader.Count - 1;
        }

        private void listboxCodeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codeReaderIndex = listboxCodeList.SelectedIndex;
            CurrentCodeReader = CurrentVisionDevice.CodeReaders[codeReaderIndex];
            DisableValueChangedEvent = true;
            UpdateCodeReaderToInterface();
            DisableValueChangedEvent = false;
        }

        private bool DisableValueChangedEvent = false;
        private void UpdateCodeReaderToInterface()
        {
            txtName.Text = CurrentCodeReader.Name;
            cbCodeType.SelectedIndex = CurrentCodeReader.Type;
            txtLengthMin.Text = CurrentCodeReader.MinLength.ToString();
            txtLengthMax.Text = CurrentCodeReader.MaxLength.ToString();
            txtFilterString.Text = CurrentCodeReader.FilterString;
        }

        //------------------------------------------------------------
        // Mỗi khi có thay đổi trong thông số thì cập nhật vào
        // Code Reader
        //------------------------------------------------------------
        private void CodeReaderValueChanged(object sender, EventArgs e)
        {
            if (DisableValueChangedEvent) return;
            try
            {
                CurrentCodeReader.Name = txtName.Text;
                CurrentCodeReader.Type = cbCodeType.SelectedIndex;
                CurrentCodeReader.MinLength = int.Parse(txtLengthMin.Text);
                CurrentCodeReader.MaxLength = int.Parse(txtLengthMax.Text);
                CurrentCodeReader.FilterString = txtFilterString.Text;
            }
            catch { }
        }

        private void btnRemoveCode_Click(object sender, EventArgs e)
        {
            if (CurrentCodeReader != null)
            {
                CurrentVisionDevice.CodeReaders.Remove(CurrentCodeReader);
                CurrentCodeReader = null;
                ListCodeReader = CurrentVisionDevice.ListCodeReaderName;
                listboxCodeList.DataSource = ListCodeReader;
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            // Lấy địa chỉ lưu mới
            FolderBrowserDialog tempFolderDialog = new FolderBrowserDialog();
            tempFolderDialog.ShowDialog();
            string urlNew = tempFolderDialog.SelectedPath;
            if (Directory.Exists(urlNew))
            {
                SaveUrl = urlNew;
            }
            // Lưu VisionDevices
            SaveAllVision();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveAllVision();
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            //------------------------------------------------------------
            // Cập nhật tên trong danh sách Code Reader
            //------------------------------------------------------------
            int currentSelectIndex = listboxCodeList.SelectedIndex;
            ListCodeReader = CurrentVisionDevice.ListCodeReaderName;
            listboxCodeList.DataSource = ListCodeReader;
            listboxCodeList.SelectedIndex = currentSelectIndex;
        }

        private bool editingROI = false;
        HTuple tempDrawingRect;
        Color BackuptButtonColor;
        private void btnEditROI_Click(object sender, EventArgs e)
        {
            if (!editingROI)
            {
                BackuptButtonColor = (sender as Button).BackColor;
                (sender as Button).BackColor = Color.Silver;

                editingROI = true;
                // Find Small Rectangle
                HTuple rRow1, rCol1, rRow2, rCol2;
                HOperatorSet.SmallestRectangle1(CurrentCodeReader.ROI, out rRow1, out rCol1,
                                                out rRow2, out rCol2);
                HOperatorSet.CreateDrawingObjectRectangle1(rRow1, rCol1, rRow2, rCol2,
                                                           out tempDrawingRect);
                // Display
                HOperatorSet.AttachDrawingObjectToWindow(DisplayWD, tempDrawingRect);
            }
            else
            {
                editingROI = false;
                (sender as Button).BackColor = BackuptButtonColor;
                HOperatorSet.DetachDrawingObjectFromWindow(DisplayWD, tempDrawingRect);

                HTuple edittingROI;
                try
                {
                    HOperatorSet.GetDrawingObjectParams(tempDrawingRect,
                                                        new HTuple("row1", "column1", "row2", "column2"),
                                                        out edittingROI);
                    HObject tempROI = new HRegion((HTuple)edittingROI[0], (HTuple)edittingROI[1],
                                                  (HTuple)edittingROI[2], (HTuple)edittingROI[3]);
                    CurrentCodeReader.ROI = tempROI;
                    HOperatorSet.DispObj(CurrentCodeReader.ROI, DisplayWD);
                }
                catch
                {
                    HOperatorSet.DispObj(CurrentCodeReader.ROI, DisplayWD);
                    return;
                }
            }


        }

        private void btnTryOne_Click(object sender, EventArgs e)
        {
            if (CurrentVisionDevice.Camera == null)
            {
                return;
            }

            HObject img;
            HTuple partWidth, partHigh;

            HOperatorSet.GrabImage(out img, CurrentVisionDevice.Camera);

            //------------------------------------------------------------
            // Set display
            //------------------------------------------------------------
            HOperatorSet.GetImageSize(img, out partWidth, out partHigh);
            DisplayWD.SetPart(new HTuple(0), new HTuple(0), partHigh, partWidth);
            img.DispObj(DisplayWD);
            DisplayWD.SetColor("#5f9ea080");
            DisplayWD.SetDraw("margin");
            DisplayWD.SetLineWidth(3.5);

            string codeResult = CurrentCodeReader.Run(img);

            HOperatorSet.DispObj(CurrentCodeReader.ROI, DisplayWD);
            HOperatorSet.DispText(DisplayWD, $"Kết quả : {codeResult}", new HTuple("window"),
                                    new HTuple(100), new HTuple(100), new HTuple("black"),
                                    new HTuple(), new HTuple());
        }
    }
}
