using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

using OpenTK.Mathematics;

namespace TransformationApplication.Base
{
    public class ViewCamera : SceneComponent
    {
        private float _fov = MathHelper.PiOver4;

        public ViewCamera(Transformation transformation)
        {
            Transformation = transformation.Clone();
            Front = -Vector3.UnitZ;
        }

        public ViewCamera()
        {
            Front = -Vector3.UnitZ;
        }

        public float AspectRatio { private get; set; } = 1.0f;

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                float angle = MathHelper.Clamp(value, 1.0f, 90.0f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        public Matrix4 GetViewMatrix()
        {
            Vector3 position = new(Transformation.Position.X,
                Transformation.Position.Y,
                Transformation.Position.Z);

            return Matrix4.LookAt(position, position + Front, Up);
        }

        public Matrix4 GetProjectionMatrix(float near, float far)
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, near, far);
        }
    }
}
