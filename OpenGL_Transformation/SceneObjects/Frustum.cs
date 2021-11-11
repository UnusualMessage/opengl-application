using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects
{
    public class Frustum : SceneComponent, IVisible
    {
        private SimpleObject _near;
        private SimpleObject _far;
        private SimpleObject _line;

        public Frustum(Shader shader) 
        {
            _near = new(shader, GetNearPlane(1.55f, 45.0f, 1.0f));
            _far = new(shader, GetFarPlane(1.55f, 45.0f, 1.0f, 10.1f));
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            throw new System.NotImplementedException();
        }

        public static float[] GetNearPlane(float aspectRatio, float fov, float near)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float[] template =
            {
                -nearWidth, -nearHeight, 0,
                 nearWidth, -nearHeight, 0,
                 nearWidth,  nearHeight, 0,
                 nearWidth,  nearHeight, 0,
                -nearWidth,  nearHeight, 0,
                -nearWidth, -nearHeight, 0,
            };
            return template;
        }

        public static float[] GetFarPlane(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float farHeight = far / near * nearHeight;
            float farWidth = far / near * nearWidth;

            float[] template =
            {
                -farWidth, -farHeight, 0,
                 farWidth, -farHeight, 0,
                 farWidth,  farHeight, 0,
                 farWidth,  farHeight, 0,
                -farWidth,  farHeight, 0,
                -farWidth, -farHeight, 0,
            };
            return template;
        }
    }
}
