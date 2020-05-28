namespace LT_Support
{
    public class CalibInfoObject
    {
        public bool Calibrated { get; set; }
        public CalibrationNPointTool CalibRobotToCam { get; set; }
        public CalibrationNPointTool CalibCamToRobot { get; set; }
    }
}
