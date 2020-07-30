using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using Newtonsoft.Json;

namespace SEV_LaptopCodeToHID.Halcon
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CodeReader
    {
        [JsonProperty]
        public string Url { get; set; }
        [JsonProperty]
        public string Name { get; set; }
        [JsonProperty]
        public int Type { get; set; }
        [JsonProperty]
        public int MinLength { get; set; }
        [JsonProperty]
        public int MaxLength { get; set; }
        [JsonProperty]
        public string FilterString { get; set; }
        public HObject ROI { get; set; }
        public HTuple BarcodeTool { get; set; }
        public HTuple DMCodeTool { get; set; }
        public HTuple QRCodeTool { get; set; }

        public CodeReader()
        {
            Name = "null";
            Type = 0;
            MinLength = 1;
            MaxLength = 50;
            FilterString = "";
            // Default Region
            ROI = new HRegion(  new HTuple(100), new HTuple(100), 
                                new HTuple(200), new HTuple(200));
        }

        public string FindCode(HObject ImageIn)
        {
            return null;
        }

        public void Save()
        {
            // Serialize
            string jsonStr;
            if (!Directory.Exists(Url))
            {
                Directory.CreateDirectory(Url);
            }
            string codeReaderUrl = Url + "\\CodeReaderSettings.json";
            jsonStr = JsonConvert.SerializeObject(this);
            File.WriteAllText(codeReaderUrl, jsonStr);

            // Lưu các đối tượng con
            string urlROI = Url + "\\ROI.hobj";
            string urlDMCode = Url + "\\DMCode.dcm";
            string urlQRCode = Url + "\\QRCode.dcm";
            string urlBarcode = Url + "\\Barcode.bcm";
            if (ROI != null)
            {
                HOperatorSet.WriteRegion(ROI, urlROI);
            }
            if (DMCodeTool != null)
            {
                HOperatorSet.WriteDataCode2dModel(DMCodeTool, urlDMCode);
            }
            if (QRCodeTool != null)
            {
                HOperatorSet.WriteDataCode2dModel(QRCodeTool, urlQRCode);
            }
            if (BarcodeTool != null)
            {
                HOperatorSet.WriteBarCodeModel(BarcodeTool, urlBarcode);
            }
        }

        public void Load()
        {
            string urlROI = Url + "\\ROI.hobj";
            string urlDMCode = Url + "\\DMCode.dcm";
            string urlQRCode = Url + "\\QRCode.dcm";
            string urlBarcode = Url + "\\Barcode.bcm";
            if (File.Exists(urlROI))
            {
                HObject tempROI;
                HOperatorSet.ReadRegion(out tempROI, urlROI);
                ROI = tempROI;
            }
            if (File.Exists(urlDMCode))
            {
                HTuple temp2DDMCode;
                HOperatorSet.ReadDataCode2dModel(urlDMCode, out temp2DDMCode);
                DMCodeTool = temp2DDMCode;

            }
            if (File.Exists(urlQRCode))
            {
                HTuple temp2DQRCode;
                HOperatorSet.ReadDataCode2dModel(urlQRCode, out temp2DQRCode);
                QRCodeTool = temp2DQRCode;
            }
            if (File.Exists(urlBarcode))
            {
                HTuple tempBarcode;
                HOperatorSet.ReadBarCodeModel(BarcodeTool, out tempBarcode);
                BarcodeTool = tempBarcode;
            }
        }

        //------------------------------------------------------------
        // Đọc code từ ảnh gửi vào, trả về kết quả dạng string
        //------------------------------------------------------------
        public string Run(HObject imageIn)
        {
            HObject imgCrop;
            HObject symbolQRCode, symbolDMCode, symbolBarcode;
            HTuple  decodedDataBarcode, resultDMCode, resultQRCode, 
                    decodedDataQRCode, decodedDataDMCode;
            HTuple resultTuple;
            string returnStr = "";

            resultTuple = new HTuple();

            // Lấy ảnh theo vùng ROI
            HOperatorSet.ReduceDomain(imageIn, ROI, out imgCrop);

            // Sử dụng Tool đọc code tương ứng, xử lý ảnh đã Crop
            // Kết quả trả về resultTuple
            switch (Type)
            {

                case 0: // Barcode type ----------------------------------------------------------
                    if (BarcodeTool == null)
                    {
                        BarcodeTool = new HBarCode(new HTuple(), new HTuple());
                    }
                    // Find Code
                    HOperatorSet.FindBarCode(   imgCrop, out symbolBarcode, 
                                                BarcodeTool, new HTuple("auto"), 
                                                out decodedDataBarcode);
                    // Get Result
                    resultTuple = decodedDataBarcode;
                    break;

                case 1: // DMCode type ------------------------------------------------------------
                    if (DMCodeTool == null)
                    {
                        DMCodeTool = new HDataCode2D(   "Data Matrix ECC 200", 
                                                        new HTuple("default_parameters"),
                                                        new HTuple("enhanced_recognition"));
                    }
                    // Find Code
                    HOperatorSet.FindDataCode2d(imgCrop, out symbolDMCode, 
                                                DMCodeTool, new HTuple(),
                                                new HTuple(), out resultDMCode, 
                                                out decodedDataDMCode);
                    // Get Result
                    resultTuple = decodedDataDMCode;
                    break;

                case 2: // QRCode type -------------------------------------------------------------
                    if (QRCodeTool == null)
                    {
                        QRCodeTool = new HDataCode2D("QR Code", new HTuple(), new HTuple());
                    }
                    // Find Code
                    HOperatorSet.FindDataCode2d(imgCrop, out symbolQRCode, QRCodeTool, 
                                                new HTuple(), new HTuple(), 
                                                out resultQRCode, out decodedDataQRCode);
                    // Get Result
                    resultTuple = decodedDataQRCode;
                    break;

                default:
                    break;
            }

            // Kiểm tra kết quả tool đọc code, lọc lấy code cần tìm
            if (resultTuple.Length == 0)
            {
                returnStr = "ERR";
            }
            else
            {
                // Filter theo độ dài + chuỗi ký tự bắt buộc (filterString)
                foreach (string item in resultTuple.SArr)
                {
                    // Filter
                    if ((item.Length > MaxLength) || (item.Length < MinLength)) continue;
                    if (!item.Contains(FilterString)) continue;

                    // Lấy code dài nhất đã filter
                    if (item.Length > returnStr.Length)
                    {
                        returnStr = item;
                    }
                }

                // Nếu đã lọc hết => trả về ERR
                if (returnStr.Length < MinLength) returnStr = "ERR";
            }

            return returnStr;
        }
    }
}
