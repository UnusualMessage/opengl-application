using OpenTK.Mathematics;

namespace TransformationApplication.Mathematics.Base
{
    public class Rotation
    {
        private const float MinBorder = -180.0f;
        private const float MaxBorder = 180.0f;

        private float _pitch;
        private float _yaw;
        private float _roll;

        public float Pitch
        {
            get => _pitch;
            set
            {
                float angle = MathHelper.Clamp(value, MinBorder, MaxBorder);
                _pitch = angle;
            }
        }

        public float Yaw
        {
            get => _yaw;
            set
            {
                float angle = MathHelper.Clamp(value, MinBorder, MaxBorder);
                _yaw = angle;
            }
        }

        public float Roll
        {
            get => _roll;
            set
            {
                float angle = MathHelper.Clamp(value, MinBorder, MaxBorder);
                _roll = angle;
            }
        }

        public Rotation()
        {
            _pitch = 0;
            _yaw = 0;
            _roll = 0;
        }

        public Rotation(float pitch, float yaw, float roll)
        {
            _pitch = pitch;
            _yaw = yaw;
            _roll = roll;
        }

        private Rotation(Rotation rotation)
        {
            _pitch = rotation.Pitch;
            _yaw = rotation.Yaw;
            _roll = rotation.Roll;
        }

        public Rotation Clone()
        {
            return new Rotation(this);
        }
    }
}
