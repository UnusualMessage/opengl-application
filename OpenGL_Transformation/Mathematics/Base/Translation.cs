using OpenTK.Mathematics;

namespace TransformationApplication.Mathematics.Base
{
    public class Translation
    {
        private const float MinBorder = -10.0f;
        private const float MaxBorder = 10.0f;

        private float _x;
        private float _y;
        private float _z;

        public float X
        {
            get => _x;
            set => _x = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float Y
        {
            get => _y;
            set => _y = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float Z
        {
            get => _z;
            set => _z = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public Translation()
        {
            _x = 0;
            _y = 0;
            _z = 0;
        }

        public Translation(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        private Translation(Translation translation)
        {
            _x = translation.X;
            _y = translation.Y;
            _z = translation.Z;
        }

        public Translation Clone()
        {
            return new Translation(this);
        }
    }
}
