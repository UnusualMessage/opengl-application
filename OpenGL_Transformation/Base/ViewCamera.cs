using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

using OpenTK.Mathematics;

namespace TransformationApplication.Base
{
    public class ViewCamera : SceneComponent
    {
        private float _fov = MathHelper.PiOver4;

        public ViewCamera(Transformation transformation, float fov) : this(fov)
        {
            Transformation = transformation.Clone();
        }

        public ViewCamera(float fov) 
        {
            _fov = MathHelper.DegreesToRadians(fov);
            Front = -Vector3.UnitZ;
        }

        public float AspectRatio { private get; set; } = 1.55f;

        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                float minFov = 1.0f;
                float maxFov = 90.0f;

                float angle = MathHelper.Clamp(value, minFov, maxFov);
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
