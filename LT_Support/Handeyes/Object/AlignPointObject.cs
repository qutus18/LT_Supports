
namespace LT_Support
{
    public class AlignPointObject
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Theta { get; set; }

        public AlignPointObject()
        {
            X = 0;
            Y = 0;
            Theta = 0;
        }

        public AlignPointObject(float x, float y, float theta)
        {
            X = x;
            Y = y;
            Theta = theta;
        }
    }
}
