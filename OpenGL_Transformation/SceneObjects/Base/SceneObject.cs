using TransformationApplication.Mathematics;

using System;

using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public class SceneObject
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;

        public Transformation Transformation { get; set; }

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

        public Vector3 Front
        {
            get => _front;
            set => _front = value;
        }

        public Vector3 Up
        {
            get => _up;
            set => _up = value;
        }

        public void UpdateTransformation(Transformation transformation)
        {
            Transformation = transformation.Clone();
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
            _front.X = MathF.Cos(pitch) * MathF.Cos(yaw);
            _front.Y = MathF.Sin(pitch);
            _front.Z = MathF.Cos(pitch) * MathF.Sin(yaw);
            _front = Vector3.Normalize(_front);
        }
    }
}
