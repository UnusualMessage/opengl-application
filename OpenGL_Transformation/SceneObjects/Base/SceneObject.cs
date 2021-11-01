using System;

using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public class SceneObject
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;
        private float _roll;

        public Vector3 Position { get; set; }

        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                float angle = MathHelper.Clamp(value, -180.0f, 180.0f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                float angle = MathHelper.Clamp(value, -180.0f, 180.0f);
                _yaw = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        public float Roll
        {
            get => MathHelper.RadiansToDegrees(_roll);
            set
            {
                float angle = MathHelper.Clamp(value, -180.0f, 180.0f);
                _roll = MathHelper.DegreesToRadians(angle);
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
            Position = new
            (
                transformation.Translation.X,
                transformation.Translation.Y,
                transformation.Translation.Z
            );

            Pitch = transformation.Rotation.Pitch;
            Yaw = transformation.Rotation.Yaw;
            Roll = transformation.Rotation.Roll;
        }

        private void UpdateVectors()
        {
            ApplyRoll();
            ApplyPitch();
        }

        private void ApplyRoll()
        {
            _up.X = MathF.Sin(_roll);
            _up.Y = MathF.Cos(_roll);
            _up.Z = MathF.Sin(_roll);
            _up = Vector3.Normalize(_up);
        }

        private void ApplyPitch()
        {
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);
            _front = Vector3.Normalize(_front);
        }
    }
}
