using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TransformationApplication.Mathematics.Base
{
    public class Position : INotifyPropertyChanged
    {
        private float _x;
        private float _y;
        private float _z;

        public event PropertyChangedEventHandler PropertyChanged;

        public float X
        {
            get => _x;
            set
            {
                _x = value; NotifyPropertyChanged();
            }
        }

        public float Y
        {
            get => _y;
            set
            {
                _y = value; NotifyPropertyChanged();
            }
        }

        public float Z
        {
            get => _z;
            set
            {
                _z = value; NotifyPropertyChanged();
            }
        }

        public Position() { }

        public Position(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        private Position(Position translation)
        {
            _x = translation.X;
            _y = translation.Y;
            _z = translation.Z;
        }

        public Position Clone()
        {
            return new Position(this);
        }

        public void Reset()
        {
            X = 0.0f;
            Y = 0.0f;
            Z = 0.0f;
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
