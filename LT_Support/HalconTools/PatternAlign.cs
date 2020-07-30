using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Support
{
    public class PatternAlign
    {
        //------------------------------------------------------------
        // Đầu vào
        //------------------------------------------------------------
        public HImage Image { get; set; }
        public HRegion ROI { get; set; }
        public HShapeModel ShapeModel { get; set; }

        public PatternAlign()
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
