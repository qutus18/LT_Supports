using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using Newtonsoft.Json;
using System.IO;
using LT_Support.Acquision;

namespace SEV_LaptopCodeToHID.Halcon
{
    [JsonObject(MemberSerialization.OptIn)]
    public class VisionNew
    {
        [JsonProperty]
        public string Url { get; set; }
        // CamSettings.json
        public CameraSet CameraSettings { get; set; }
        [JsonProperty]
        public int NumberCodeReader
        {
            get
            {
                if (CodeReaders != null)
                {
                    return CodeReaders.Count;
                }
                else
                {
                    return 0;
                }
            }
            set { }
        }
        public List<string> ListCodeReaderName
        {
            get
            {
                List<string> tempListCodeReader = new List<string>();
                foreach (CodeReader item in CodeReaders)
                {
                    tempListCodeReader.Add(item.Name);
                }
                return tempListCodeReader;
            }
            private set { }
        }
        // Thư mục CodeReaders\1,2,3,4,5
        [JsonProperty]
        public List<CodeReader> CodeReaders { get; set; }
        // Khởi tạo lại khi mở chương trình
        public HFramegrabber Camera { get; set; }

        [JsonConstructor]
        public VisionNew(string url, int numberCodeReader)
        {
            Url = url;
            Load(numberCodeReader);
        }

        private void Load(int numberCodeReaderToLoad)
        {
            string jsonStr;
            //------------------------------------------------------------
            // Load cấu hình camera
            //------------------------------------------------------------
            string cameraSetUrl = Url + "\\CamSettings.json";
            if (File.Exists(cameraSetUrl))
            {
                jsonStr = File.ReadAllText(cameraSetUrl);
                CameraSettings = JsonConvert.DeserializeObject<CameraSet>(jsonStr);
            }
            else
            {
                CameraSettings = new CameraSet();
            }
            //------------------------------------------------------------
            // Load cài đặt các module đọc code
            //------------------------------------------------------------
            CodeReaders = new List<CodeReader>();
            for (int i = 0; i < numberCodeReaderToLoad; i++)
            {
                string codeReaderFolder = Url + "\\" + i.ToString();
                string codeReaderUrl = codeReaderFolder + "\\CodeReaderSettings.json";

                if (Directory.Exists(codeReaderFolder))
                {
                    jsonStr = File.ReadAllText(codeReaderUrl);
                    CodeReader tempCodeReader
                        = JsonConvert.DeserializeObject<CodeReader>(jsonStr);
                    CodeReaders.Add(tempCodeReader);
                }
            }
            //------------------------------------------------------------
            // Khởi tạo Camera
            //------------------------------------------------------------
            if (CameraSettings.Device.Length > 0)
            {
                string interfaceName = CameraSettings.Interface;
                string device = CameraSettings.Device;
                string generic = CameraSettings.Generic;
                Camera = new HFramegrabber(interfaceName, 1, 1, 0, 0, 0, 0, "default",
                                                         new HTuple(-1), new HTuple("default"), generic,
                                                         "default", new HTuple("default"), new HTuple(device),
                                                         new HTuple(-1), new HTuple(-1));
            }
            else
            {
                Camera = null;
            }
        }

        public void Save()
        {
            string jsonStr;
            //------------------------------------------------------------
            // Lưu cấu hình Vision
            //------------------------------------------------------------
            if (!Directory.Exists(Url))
            {
                Directory.CreateDirectory(Url);
            }
            string visionSettingsUrl = Url + $"\\VisionConfig.json";
            jsonStr = JsonConvert.SerializeObject(this);
            File.WriteAllText(visionSettingsUrl, jsonStr);
            //------------------------------------------------------------
            // Lưu cấu hình Camera
            //------------------------------------------------------------
            string cameraSetUrl = Url + "\\CamSettings.json";
            jsonStr = JsonConvert.SerializeObject(CameraSettings);
            File.WriteAllText(cameraSetUrl, jsonStr);
            //------------------------------------------------------------
            // Lưu cấu hình Code Reader
            //------------------------------------------------------------
            for (int i = 0; i < NumberCodeReader; i++)
            {
                string codeReaderFolder = Url + "\\" + i.ToString();
                string codeReaderUrl = codeReaderFolder + "\\CodeReaderSettings.json";
                CodeReaders[i].Url = codeReaderFolder;
                CodeReaders[i].Save();
            }
        }

        public void AddNewCodeReader()
        {
            CodeReader tempNewCodeReader = new CodeReader();
            tempNewCodeReader.Name = "New Code Reader";
            CodeReaders.Add(tempNewCodeReader);
        }

        public string Run()
        {
            string returnString = "";
            HObject img;
            if (Camera == null)
            {
                return "Camera not init";
            }

            //------------------------------------------------------------
            // Lấy ảnh
            //------------------------------------------------------------
            try
            {
                HOperatorSet.GrabImage(out img, Camera);
            }
            catch
            {
                return "Camera grab error";
            }

            //------------------------------------------------------------
            // Lấy kết quả chuỗi đọc được
            //------------------------------------------------------------
            foreach (CodeReader codeReader in CodeReaders)
            {
                string strResult = codeReader.Run(img);
                returnString += strResult + ",";
            }

            return returnString;
        }
    }
}
