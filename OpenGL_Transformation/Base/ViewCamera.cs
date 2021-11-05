using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

using OpenTK.Mathematics;

namespace TransformationApplication.Base
{
    public class ViewCamera : SceneObject
    {
        private float _fov = MathHelper.PiOver2;

        public ViewCamera(Transformation transformation, float aspectRatio)
        {
            Transformation = transformation.Clone();
            AspectRatio = aspectRatio;
        }

        public float AspectRatio { private get; set; }

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
            Vector3 position = new(Transformation.Position.X,
                Transformation.Position.Y,
                Transformation.Position.Z);

            return Matrix4.LookAt(position, position + Front, Up);
        }

        public Matrix4 GetProjectionMatrix()
        {
            const float near = 0.1f;
            const float far = 10.0f;
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, near, far);
        }
    }
}
