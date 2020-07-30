using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT_Support.Acquision
{
    public class CameraSet
    {
        public double Gain { get; set; }
        public double Exposure { get; set; }
        public string Interface { get; set; }
        public string Generic { get; set; }
        public string Device { get; set; }

        public CameraSet()
        {
            Gain = 0;
            Exposure = 0;
            Interface = "GigEVision2";
            Generic = "force_ip=192.168.125.181/C4:2F:90:F9:10:DB/192.168.125.1/255.255.255.0";
            Device = "c42f90f910db_Hikvision_MVCEUF41GM";
        }
    }
}
