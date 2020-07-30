namespace LT_Support
{
    //------------------------------------------------------------
    // Đối tượng Matrix sử dụng tính toán Vision - 3x3 Float
    //------------------------------------------------------------
    public class VisionMatrixObject
    {
        public float[] Value { get; set; }
        public VisionMatrixObject()
        {
            Value = new float[9];
        }

        public VisionMatrixObject(float[] inFloatArr)
        {
            try
            {
                Value = (float[])inFloatArr.Clone();
            }
            catch
            {
                Value = new float[9];
            }
        }
    }
}
