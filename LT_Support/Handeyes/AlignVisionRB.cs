namespace LT_Support
{
    public class AlignVisionRB
    {
        public VisionMatrixObject PBase11 { get; set; }
        public VisionMatrixObject PBase12 { get; set; }
        public VisionMatrixObject PBase21 { get; set; }
        public VisionMatrixObject PBase22 { get; set; }
        public AlignPointObject Master11 { get; set; }
        public AlignPointObject Master12 { get; set; }
        public AlignPointObject Master21 { get; set; }
        public AlignPointObject Master22 { get; set; }

        //------------------------------------------------------------
        // Tính toán master theo tool, object
        //------------------------------------------------------------
        public bool TT(int toolNum, int objectNum, AlignPointObject visionPoint, AlignPointObject robotPoint, CalibInfoObject calibInfo)
        {
            if ((toolNum > 2) || (toolNum < 1) || (objectNum > 2) || (objectNum < 1))
            {
                return false;
            }
            try
            {
                //------------------------------------------------------------
                // Mặc định sử dụng điểm master Robot lấy góc == 0
                //------------------------------------------------------------
                AlignPointObject masterRBPoint = new AlignPointObject(robotPoint.X, robotPoint.Y, (float)0.0);
                VisionMatrixObject masterPBase = new VisionMatrixObject();
                AlignPointObject masterRBInVisionBase = AlignSupports.ConvertPointCalibNPoint(masterRBPoint, calibInfo.CalibRobotToCam);
                VisionMatrixObject masterPRobot = AlignSupports.ConvertPointToMatrix(masterRBInVisionBase);
                VisionMatrixObject masterPRobotInv = AlignSupports.Invert(masterPRobot);
                VisionMatrixObject masterPVision = AlignSupports.ConvertPointToMatrix(visionPoint);
                //------------------------------------------------------------
                // Tính toán PBase = PRobotInv * PVision
                //------------------------------------------------------------
                masterPBase = AlignSupports.Mul(masterPRobotInv, masterPVision);
                //------------------------------------------------------------
                // Lưu giá trị master theo tool, object tương ứng
                //------------------------------------------------------------
                //switch (toolNum + objectNum * 0.1)
                //{
                //    case (1.1):
                //        PBase11 = masterPBase;
                //        Master11 = masterRBPoint;
                //        break;
                //    case (1.2):
                //        PBase12 = masterPBase;
                //        Master12 = masterRBPoint;
                //        break;
                //    case (2.1):
                //        PBase21 = masterPBase;
                //        Master21 = masterRBPoint;
                //        break;
                //    case (2.2):
                //        PBase22 = masterPBase;
                //        Master22 = masterRBPoint;
                //        break;
                //    default:
                //        break;
                //}
            }
            catch
            {
                return false;
            }
            return true;
        }

        //------------------------------------------------------------
        // Tính toán tọa độ tính toán autocalib theo tool, object
        //------------------------------------------------------------
        public AlignPointObject XT(int toolNum, int objectNum, AlignPointObject visionPoint, CalibInfoObject calibInfo)
        {
            if ((toolNum > 2) || (toolNum < 1) || (objectNum > 2) || (objectNum < 1))
            {
                return null;
            }
            try
            {
                AlignPointObject masterRBPoint = new AlignPointObject();
                VisionMatrixObject masterPBase = new VisionMatrixObject();
                //------------------------------------------------------------
                // lấy giá trị master theo tool, object tương ứng
                //------------------------------------------------------------
                //double condition = toolNum + objectNum * 0.1;
                //switch (condition)
                //{
                //    case (1.1):
                //        masterPBase = PBase11;
                //        masterRBPoint = Master11;
                //        break;
                //    case (1.2):
                //        masterPBase = PBase12;
                //        masterRBPoint = Master12;
                //        break;
                //    case (2.1):
                //        masterPBase = PBase21;
                //        masterRBPoint = Master21;
                //        break;
                //    case (2.2):
                //        masterPBase = PBase22;
                //        masterRBPoint = Master22;
                //        break;
                //    default:
                //        break;
                //}
                //------------------------------------------------------------
                // Tính toán PAlign
                //------------------------------------------------------------
                VisionMatrixObject pBaseInvert = AlignSupports.Invert(masterPBase);
                VisionMatrixObject pVision = AlignSupports.ConvertPointToMatrix(visionPoint);
                VisionMatrixObject pRobot = AlignSupports.Mul(pVision, pBaseInvert);
                //------------------------------------------------------------
                // Tính toán tọa độ tâm tool Robot, chuyển đổi tọa độ từ hệ Vision sang hệ Robot
                //------------------------------------------------------------
                AlignPointObject pointRBInVisionBase = AlignSupports.ConvertMatrixToPoint(pRobot);
                AlignPointObject pointRB = AlignSupports.ConvertPointCalibNPoint(pointRBInVisionBase, calibInfo.CalibCamToRobot);
                //------------------------------------------------------------
                // Tính toán Offset trả về Robot
                //------------------------------------------------------------
                AlignPointObject pointRBOffset = AlignSupports.CalPointOffset(pointRB, masterRBPoint);
                return pointRBOffset;
            }
            catch
            {
                return null;
            }
        }


    }
}
