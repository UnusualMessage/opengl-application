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

        protected Transformation Transformation { get; set; } = new();

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
            get => MathHelper.DegreesToRadians(Transformation.Rotation.Pitch);
            set
            {
                Transformation.Rotation.Pitch = value;
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.DegreesToRadians(Transformation.Rotation.Yaw);
            set
            {
                Transformation.Rotation.Yaw = value;
                UpdateVectors();
            }
        }

        public float Roll
        {
            get => MathHelper.DegreesToRadians(Transformation.Rotation.Roll);
            set
            {
                Transformation.Rotation.Roll = value;
                UpdateVectors();
            }
        }

        protected Vector3 Front
        {
            get => _front;
            set => _front = value;
        }

        protected Vector3 Up
        {
            get => _up;
            set => _up = value;
        }

        protected Vector3 Right
        {
            get => _right;
            set => _right = value;
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
            _up.X = MathF.Sin(Roll);
            _up.Y = MathF.Cos(Roll);
            _up.Z = MathF.Sin(Roll);
            _up = Vector3.Normalize(_up);
        }

        private void ApplyPitch()
        {
            _front.X = MathF.Cos(Pitch) * MathF.Cos(Yaw);
            _front.Y = MathF.Sin(Pitch);
            _front.Z = MathF.Cos(Pitch) * MathF.Sin(Yaw);
            _front = Vector3.Normalize(_front);
        }
    }
}
