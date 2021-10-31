using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public class Rotation
    {
        private const float MinBorder = -180.0f;
        private const float MaxBorder = 180.0f;

        private float _rotationByX;
        private float _rotationByY;
        private float _rotationByZ;

        public float RotationByX
        {
            get => _rotationByX;
            set => _rotationByX = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float RotationByY
        {
            get => _rotationByY;
            set => _rotationByY = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float RotationByZ
        {
            get => _rotationByZ;
            set => _rotationByZ = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public Rotation(float x, float y, float z)
        {
            _rotationByX = x;
            _rotationByY = y;
            _rotationByZ = z;
        }
    }
}
