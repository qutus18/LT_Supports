using System;

namespace LT_Support
{
    public static class AlignSupports
    {
        //------------------------------------------------------------
        // Nhân 2 ma trận 3x3
        //------------------------------------------------------------
        public static VisionMatrixObject Mul(VisionMatrixObject inMat01, VisionMatrixObject inMat02)
        {
            float[] arrOut = new float[9];
            float m11 = inMat01.Value[0];
            float m12 = inMat01.Value[1];
            float m13 = inMat01.Value[2];
            float m21 = inMat01.Value[3];
            float m22 = inMat01.Value[4];
            float m23 = inMat01.Value[5];
            float m31 = inMat01.Value[6];
            float m32 = inMat01.Value[7];
            float m33 = inMat01.Value[8];

            float n11 = inMat02.Value[0];
            float n12 = inMat02.Value[1];
            float n13 = inMat02.Value[2];
            float n21 = inMat02.Value[3];
            float n22 = inMat02.Value[4];
            float n23 = inMat02.Value[5];
            float n31 = inMat02.Value[6];
            float n32 = inMat02.Value[7];
            float n33 = inMat02.Value[8];

            arrOut[0] = m11 * n11 + m12 * n21 + m13 * n31;
            arrOut[1] = m11 * n12 + m12 * n22 + m13 * n32;
            arrOut[2] = m11 * n13 + m12 * n23 + m13 * n33;
            arrOut[3] = m21 * n11 + m22 * n21 + m23 * n31;
            arrOut[4] = m21 * n12 + m22 * n22 + m23 * n32;
            arrOut[5] = m21 * n13 + m22 * n23 + m23 * n33;
            arrOut[6] = m31 * n11 + m32 * n21 + m33 * n31;
            arrOut[7] = m31 * n12 + m32 * n22 + m33 * n32;
            arrOut[8] = m31 * n13 + m32 * n23 + m33 * n33;
            return new VisionMatrixObject(arrOut);
        }
        //------------------------------------------------------------
        // Nghịch đảo ma trận 3x3
        //------------------------------------------------------------
        public static VisionMatrixObject Invert(VisionMatrixObject inMat)
        {
            float[] arrOut = new float[9];
            float m11 = inMat.Value[0];
            float m12 = inMat.Value[1];
            float m13 = inMat.Value[2];
            float m21 = inMat.Value[3];
            float m22 = inMat.Value[4];
            float m23 = inMat.Value[5];
            float m31 = inMat.Value[6];
            float m32 = inMat.Value[7];
            float m33 = inMat.Value[8];

            arrOut[0] = (m22 * m33 - m23 * m32) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[1] = -(m12 * m33 - m13 * m32) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[2] = (m12 * m23 - m13 * m22) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[3] = -(m21 * m33 - m23 * m31) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[4] = (m11 * m33 - m13 * m31) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[5] = -(m11 * m23 - m13 * m21) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[6] = (m21 * m32 - m22 * m31) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[7] = -(m11 * m32 - m12 * m31) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            arrOut[8] = (m11 * m22 - m12 * m21) / (m11 * m22 * m33 - m11 * m23 * m32 - m21 * m12 * m33 + m31 * m12 * m23 + m21 * m13 * m32 - m31 * m13 * m22);
            return new VisionMatrixObject(arrOut);
        }
        //------------------------------------------------------------
        // Chuyển đổi Point qua phép CalibNPoint
        //------------------------------------------------------------
        public static AlignPointObject ConvertPointCalibNPoint(AlignPointObject inputPoint, CalibrationNPointTool calibTool)
        {
            return null;
        }
        //------------------------------------------------------------
        // Chuyển đổi Point thành ma trận 3x3
        //------------------------------------------------------------
        public static VisionMatrixObject ConvertPointToMatrix(AlignPointObject inputPoint)
        {
            float[] arrOut = new float[9];
            arrOut[0] = (float)Math.Cos(inputPoint.Theta);
            arrOut[1] = (float)-Math.Sin(inputPoint.Theta);
            arrOut[2] = inputPoint.X;
            arrOut[3] = (float)Math.Sin(inputPoint.Theta);
            arrOut[4] = (float)Math.Cos(inputPoint.Theta);
            arrOut[5] = inputPoint.Y;
            arrOut[6] = 0;
            arrOut[7] = 0;
            arrOut[8] = 1;
            return new VisionMatrixObject(arrOut);
        }
        //------------------------------------------------------------
        // Chuyển đổi ma trận 3x3 sang Point
        //------------------------------------------------------------
        public static AlignPointObject ConvertMatrixToPoint(VisionMatrixObject inputMatrix)
        {
            float x, y, theta;
            x = inputMatrix.Value[2];
            y = inputMatrix.Value[5];
            //------------------------------------------------------------
            // Cal Theta from sin + cos
            //------------------------------------------------------------
            float tempTheta = (float)Math.Asin(inputMatrix.Value[3]);
            if (Math.Cos(tempTheta) == inputMatrix.Value[0])
            {
                theta = tempTheta;
            }
            else
            {
                theta = (float)(Math.PI - tempTheta);
            }
            //------------------------------------------------------------
            // Convert theta if over -PI/PI
            //------------------------------------------------------------
            for (int i = 0; i < 4; i++)
            {
                if (theta < (float)(-Math.PI))
                {
                    theta += (float)Math.PI;
                }
                if (theta > (float)(Math.PI))
                {
                    theta -= (float)Math.PI;
                }
            }
            return new AlignPointObject(x, y, theta);
        }
        //------------------------------------------------------------
        // Tính toán Offset từ tọa độ Robot cũ - mới
        //------------------------------------------------------------
        public static AlignPointObject CalPointOffset(AlignPointObject newRBPoint, AlignPointObject masterRBPoint)
        {
            return new AlignPointObject(newRBPoint.X - masterRBPoint.X, newRBPoint.Y - masterRBPoint.Y, newRBPoint.Theta - masterRBPoint.Theta);
        }
        //------------------------------------------------------------
        // Rad To Deg
        //------------------------------------------------------------
        public static float ToDeg(float inRad)
        {
            return (float)(inRad * 180.0 / Math.PI);
        }
        //------------------------------------------------------------
        // Deg to Rad
        //------------------------------------------------------------
        public static float ToRad(float inDeg)
        {
            return (float)(inDeg * Math.PI / 180.0);
        }
    }
}
