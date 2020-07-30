using System;
using System.Collections.Generic;

namespace LT_Support
{
    public class HandeyeCalib
    {
        public CalibInfoObject CalibInfo { get; set; }
        public List<AlignPointObject> ListPointVisionNormal { get; set; }
        public List<AlignPointObject> ListPointVisionRotate { get; set; }
        public List<AlignPointObject> ListPointRobotNormal { get; set; }
        public List<AlignPointObject> ListPointRobotRotate { get; set; }

        public HandeyeCalib()
        {
            IniticalHE();
        }

        public void IniticalHE()
        {
            CalibInfo = new CalibInfoObject();
            ListPointVisionNormal = new List<AlignPointObject>();
            ListPointVisionRotate = new List<AlignPointObject>();
            ListPointRobotNormal = new List<AlignPointObject>();
            ListPointRobotRotate = new List<AlignPointObject>();
        }

        public bool AddPointNormal(AlignPointObject rbPoint, AlignPointObject visionPoint)
        {
            ListPointRobotNormal.Add(rbPoint);
            ListPointVisionNormal.Add(visionPoint);
            return true;
        }

        public bool AddPointRotate(AlignPointObject rbPoint, AlignPointObject visionPoint)
        {
            ListPointRobotRotate.Add(rbPoint);
            ListPointVisionRotate.Add(visionPoint);
            return true;
        }

        public bool ProcessHE()
        {
            bool checkNumberPointResult = CheckNumberPointToRunHEProcess();
            if (checkNumberPointResult == false) return false;
            return true;
        }

        private bool CheckNumberPointToRunHEProcess()
        {
            return true;
        }
    }
}
