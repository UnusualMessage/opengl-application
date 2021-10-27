namespace TransformationApplication.SceneObjects.Base
{
    public class Rotation
    {
        public float RotationByX { get; set; }
        public float RotationByY { get; set; }
        public float RotationByZ { get; set; }

        public Rotation(float x, float y, float z)
        {
            RotationByX = x;
            RotationByY = y;
            RotationByZ = z;
        }
    }
}
