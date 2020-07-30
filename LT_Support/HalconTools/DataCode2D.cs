using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Support.HalconTools
{
    public class DataCode2D
    {
        //------------------------------------------------------------
        // Đầu vào
        //------------------------------------------------------------
        public HImage Image { get; set; }
        public HRegion ROI { get; set; }
        public HDataCode2D DataCode2DTool { get; set; }
        public DataCode2DForm ParamsForm { get; set; } 

        public DataCode2D()
        {

        }

        //------------------------------------------------------------
        // Cập nhật đầu vào
        //------------------------------------------------------------
        public void RefreshInput()
        {

        }

        //------------------------------------------------------------
        // Chạy tool, xử lý dữ liệu
        //------------------------------------------------------------
        public void Run()
        {

        }

        //------------------------------------------------------------
        // Đầu ra
        //------------------------------------------------------------
        public HTuple ZOriginValue { get; set; }
        public HRegion ZMargin { get; set; }
        public HXLD ZOriginGraphic { get; set; }
    }
}
