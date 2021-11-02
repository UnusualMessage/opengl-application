using System;

namespace TransformationApplication.Mathematics
{
    public class MatrixRow
    {
        private const int Digits = 2;

        private readonly float _first;
        private readonly float _second;
        private readonly float _third;
        private readonly float _fourth;

        public MatrixRow(float first, float second, float third, float fourth)
        {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
        }

        public float First => MathF.Round(_first, Digits);
        public float Second => MathF.Round(_second, Digits);
        public float Third => MathF.Round(_third, Digits);
        public float Fourth => MathF.Round(_fourth, Digits);
    }
}
