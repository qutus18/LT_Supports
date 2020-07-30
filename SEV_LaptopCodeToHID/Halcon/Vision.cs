using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using HalconProcs;


namespace SEV_LaptopCodeToHID.Halcon
{
    public class Vision
    {
        public HDataCode2D HDataCode2D_QRCode;
        public HDataCode2D HDataCode2D_DataMatrix;
        public HBarCode HBarcode;

        private HObject HRegion_QRCode;
        private HObject HRegion_DataMatrix;
        private HObject HRegion_Barcode;

        public string ColorQRCode = "#5f9ea080";
        public string ColorDMCode = "#228b2280";
        public string ColorBarCode = "#cd5c5c80";

        public HTuple HFramegrabber;

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        private HWindow displayWD;
        public HWindow DisplayWD
        {
            get { return displayWD; }
            set
            {
                displayWD = value;
                HObject firstImage;
                HTuple partWidth, partHigh;
                HOperatorSet.GrabImage(out firstImage, HFramegrabber);
                HOperatorSet.GetImageSize(firstImage, out partWidth, out partHigh);
                DisplayWD.SetPart(new HTuple(0), new HTuple(0), partHigh, partWidth);
                firstImage.DispObj(DisplayWD);
                DisplayWD.SetColor("#5f9ea080");
                DisplayWD.SetDraw("margin");
                DisplayWD.SetLineWidth(3.5);
            }
        }

        public Vision()
        {

            //------------------------------------------------------------
            // Khởi tạo Camera
            //------------------------------------------------------------
            GenCamera.Init_Acquisition(out HFramegrabber);

            //------------------------------------------------------------
            // Khởi tạo đối tượng đọc code
            //------------------------------------------------------------
            HDataCode2D_QRCode = new HDataCode2D("QR Code", new HTuple(), new HTuple());
            HDataCode2D_DataMatrix = new HDataCode2D("Data Matrix ECC 200", new HTuple("default_parameters"), new HTuple("enhanced_recognition"));
            HBarcode = new HBarCode(new HTuple(), new HTuple());

            //------------------------------------------------------------
            // Load ROI đọc code của 3 loại code
            //------------------------------------------------------------
            HOperatorSet.SetSystem(new HTuple("clip_region"), new HTuple("false"));
            HOperatorSet.ReadRegion(out HRegion_QRCode, new HTuple("D:/Halcon/ROI_QR.hobj"));
            HOperatorSet.ReadRegion(out HRegion_DataMatrix, new HTuple("D:/Halcon/ROI_DM.hobj"));
            HOperatorSet.ReadRegion(out HRegion_Barcode, new HTuple("D:/Halcon/ROI_B39.hobj"));
        }


        public string Run()
        {
            HObject image;
            string returnString = "";

            try
            {
                HOperatorSet.GrabImage(out image, HFramegrabber);
            }
            catch
            {
                return "ERRCAM,ERRCAM,ERRCAM";
            }
            //HOperatorSet.GrabImageAsync(out image, HFramegrabber, new HTuple(-1.0));

            if (image == null)
            {
                return "ERR,ERR,ERR";
            }

            image.DispObj(DisplayWD);

            //------------------------------------------------------------
            // Crop ảnh theo vùng tìm kiếm để tối ưu tốc độ
            //------------------------------------------------------------
            HObject imageQRCode, imageDMCode, imageBarCode;
            HTuple rows, column;

            HOperatorSet.GetRegionPoints(HRegion_QRCode, out rows, out column);
            HOperatorSet.ReduceDomain(image, HRegion_QRCode, out imageQRCode);
            HOperatorSet.ReduceDomain(image, HRegion_DataMatrix, out imageDMCode);
            HOperatorSet.ReduceDomain(image, HRegion_Barcode, out imageBarCode);

            //------------------------------------------------------------
            // Tìm kiếm code theo ảnh
            //------------------------------------------------------------
            HObject symbolQRCode, symbolDMCode, symbolBarcode;
            HTuple decodedDataBarcode, resultDMCode, resultQRCode, decodedDataQRCode, decodedDataDMCode;

            HOperatorSet.FindDataCode2d(imageQRCode, out symbolQRCode, HDataCode2D_QRCode, new HTuple(), new HTuple(), out resultQRCode, out decodedDataQRCode);
            HOperatorSet.FindDataCode2d(imageDMCode, out symbolDMCode, HDataCode2D_DataMatrix, new HTuple(), new HTuple(), out resultDMCode, out decodedDataDMCode);
            HOperatorSet.FindBarCode(imageBarCode, out symbolBarcode, HBarcode, new HTuple("Code 39"), out decodedDataBarcode);

            //------------------------------------------------------------
            // Xử lý chuỗi đầu ra
            //------------------------------------------------------------
            DisplayWD.SetColor(ColorBarCode);
            HRegion_Barcode.DispObj(displayWD);
            if (decodedDataBarcode.Length == 0)
            {
                returnString += "ERR";
            }
            else
            {
                string lengthCode = "";
                foreach (string item in decodedDataBarcode.SArr)
                {
                    if (item.Length > lengthCode.Length)
                    {
                        lengthCode = item;
                    }
                }
                if (lengthCode.Length < 12) lengthCode = "ERR";
                returnString += "" + lengthCode;
                symbolBarcode.DispObj(DisplayWD);
            }

            DisplayWD.SetColor(ColorDMCode);
            HRegion_DataMatrix.DispObj(displayWD);
            if (decodedDataDMCode.Length == 0)
            {
                returnString += ",ERR";
            }
            else
            {
                returnString += "," + decodedDataDMCode.SArr[0];
                symbolDMCode.DispObj(DisplayWD);
            }

            DisplayWD.SetColor(ColorQRCode);
            HRegion_QRCode.DispObj(displayWD);
            if (decodedDataQRCode.Length == 0)
            {
                returnString += ",ERR";
            }
            else
            {
                returnString += "," + decodedDataQRCode.SArr[0];
                symbolQRCode.DispObj(DisplayWD);
            }


            DisplayWD.SetColor(ColorQRCode);
            return returnString;
        }

        private bool LiveTrigger = false;
        public void LiveView()
        {
            HObject image;
            LiveTrigger = true;
            while (LiveTrigger)
            {
                try
                {
                    HOperatorSet.GrabImageAsync(out image, HFramegrabber, new HTuple(-1));
                    image.DispObj(displayWD);
                    image.Dispose();
                    HOperatorSet.WaitSeconds(new HTuple(0.2));
                }
                catch
                {
                    break;
                }
            }
           
        }

        internal void StopLiveView()
        {
            LiveTrigger = false;
        }
    }
}
