using System.ComponentModel;
using System.Runtime.CompilerServices;

using OpenTK.Mathematics;

namespace TransformationApplication.Mathematics.Base
{
    public class Rotation : INotifyPropertyChanged
    {
        private const float MinBorder = -180.0f;
        private const float MaxBorder = 180.0f;

        private float _pitch;
        private float _yaw;
        private float _roll;

        public event PropertyChangedEventHandler PropertyChanged;

        public float Pitch
        {
            get => _pitch;
            set
            {
                float angle = MathHelper.Clamp(value, MinBorder, MaxBorder);
                _pitch = angle; NotifyPropertyChanged();
            }
        }

        public float Yaw
        {
            get => _yaw;
            set
            {
                float angle = MathHelper.Clamp(value, MinBorder, MaxBorder);
                _yaw = angle; NotifyPropertyChanged();
            }
        }

        public float Roll
        {
            get => _roll;
            set
            {
                float angle = MathHelper.Clamp(value, MinBorder, MaxBorder);
                _roll = angle; NotifyPropertyChanged();
            }
        }

        public Rotation() { }

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

        public void Reset()
        {
            Pitch = 0.0f;
            Yaw = 0.0f;
            Roll = 0.0f;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
