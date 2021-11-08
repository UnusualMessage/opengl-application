using TransformationApplication.Mathematics;

using System;

using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public class SceneComponent
    {
        private Vector3 _front = Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;
        private Vector3 _right = Vector3.UnitX;

        protected Vector3 Front => _front;
        protected Vector3 Up => _up;
        protected Vector3 Right => _right;

        private Transformation _transformation = new();

        protected Transformation Transformation
        {
            get => _transformation;
            set
            {
                _transformation = value;
                UpdateVectors();
            }
        }

        public float X
        {
            get => Transformation.Position.X;
            set => Transformation.Position.X = value;
        }

        public float Y
        {
            get => Transformation.Position.Y;
            set => Transformation.Position.Y = value;
        }

        public float Z
        {
            get => Transformation.Position.Z;
            set => Transformation.Position.Z = value;
        }

        public float Pitch
        {
            get => Transformation.Rotation.Pitch;
            set
            {
                Transformation.Rotation.Pitch = value;
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => Transformation.Rotation.Yaw;
            set
            {
                Transformation.Rotation.Yaw = value;
                UpdateVectors();
            }
        }

        public float Roll
        {
            get => Transformation.Rotation.Roll;
            set
            {
                Transformation.Rotation.Roll = value;
                UpdateVectors();
            }
        }

        public void UpdateTransformation(Transformation transformation)
        {
            Transformation = transformation.Clone();
            UpdateVectors();
        }

        public void ResetTransformation()
        {
            Transformation.Reset();
            UpdateVectors();
        }

        private void UpdateVectors()
        {
            ApplyRoll();
            ApplyPitch();
        }

        private void ApplyRoll()
        {
            float roll = MathHelper.DegreesToRadians(Roll);

            _up.X = MathF.Sin(roll);
            _up.Y = MathF.Cos(roll);
            _up.Z = MathF.Sin(roll);
            _up = Vector3.Normalize(_up);
        }

        private void ApplyPitch()
        {
            float pitch = MathHelper.DegreesToRadians(Pitch);
            float yaw = MathHelper.DegreesToRadians(Yaw);

            _front.X = MathF.Cos(pitch) * MathF.Sin(yaw);
            _front.Y = MathF.Sin(pitch);
            _front.Z = MathF.Cos(pitch) * MathF.Cos(yaw);
            _front = Vector3.Normalize(_front);
        }
    }
}
