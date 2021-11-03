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
