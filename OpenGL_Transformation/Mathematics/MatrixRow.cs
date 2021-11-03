using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TransformationApplication.Mathematics
{
    public class MatrixRow : INotifyPropertyChanged
    {
        private const int Digits = 2;

        private float _first;
        private float _second;
        private float _third;
        private float _fourth;

        public event PropertyChangedEventHandler PropertyChanged;

        public MatrixRow(float first, float second, float third, float fourth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public MatrixRow() { }

        public float First
        {
            get => MathF.Round(_first, Digits);
            set { _first = value; NotifyPropertyChanged(); }
        }

        public float Second
        {
            get => MathF.Round(_second, Digits);
            set { _second = value; NotifyPropertyChanged(); }
        }

        public float Third
        {
            get => MathF.Round(_third, Digits);
            set { _third = value; NotifyPropertyChanged(); }
        }

        public float Fourth
        {
            get => MathF.Round(_fourth, Digits);
            set { _fourth = value; NotifyPropertyChanged(); }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
