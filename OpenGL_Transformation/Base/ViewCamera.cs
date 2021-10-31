using OpenTK.Mathematics;

using System;

namespace TransformationApplication.Base
{
    public class ViewCamera
    {
        private Vector3 _front = -Vector3.UnitZ;
        private Vector3 _up = Vector3.UnitY;

        private float _pitch;
        private float _yaw = -MathHelper.PiOver2;
        private float _roll;

        private float _fov = MathHelper.PiOver2;

        public ViewCamera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        public Vector3 Position { get; set; }
        public float AspectRatio { private get; set; }

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

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                float angle = MathHelper.Clamp(value, 1f, 45f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        private void UpdateVectors()
        {
            _up.X = MathF.Sin(_roll);
            _up.Y = MathF.Cos(_roll);
            _up.Z = MathF.Sin(_roll);
            _up = Vector3.Normalize(_up);

            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);
            _front = Vector3.Normalize(_front);
        }
    }
}
