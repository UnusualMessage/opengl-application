using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public class Rotation
    {
        private const float MinBorder = -180.0f;
        private const float MaxBorder = 180.0f;

        private float _pitch;
        private float _yaw;
        private float _roll;

        public float Yaw
        {
            get => _pitch;
            set => _pitch = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float Pitch
        {
            get => _yaw;
            set => _yaw = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float Roll
        {
            get => _roll;
            set => _roll = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public Rotation(float pitch, float yaw, float roll)
        {
            _pitch = pitch;
            _yaw = yaw;
            _roll = roll;
        }
    }
}
