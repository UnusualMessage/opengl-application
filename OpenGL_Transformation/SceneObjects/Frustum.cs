using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects
{
    public class Frustum : SceneComponent, IVisible
    {
        private readonly SimpleObject _nearPlane;
        private readonly SimpleObject _farPlane;

        private readonly SimpleObject _leftLine;
        private readonly SimpleObject _rightLine;

        private readonly SimpleObject _nearBorder;
        private readonly SimpleObject _farBorder;

        public float Far { get; set; }
        public float Near { get; set; }
        public float Fov { get; set; } = 45.0f;
        public float AspectRatio { get; set; } = 1.55f;

        public Frustum(Shader shader, float near, float far)
        {
            Far = far;
            Near = near;

            _nearPlane = new(shader, GetNearPlane(AspectRatio, Fov, Near));
            _farPlane = new(shader, GetFarPlane(AspectRatio, Fov, Near, Far));
            _leftLine = new(shader, GetLeftLine(AspectRatio, Fov, Near, Far));
            _rightLine = new(shader, GetRightLine(AspectRatio, Fov, Near, Far));
            _nearBorder = new(shader, GetNearPlaneBorder(AspectRatio, Fov, Near, Far));
            _farBorder = new(shader, GetFarPlaneBorder(AspectRatio, Fov, Near, Far));

            _nearPlane.Color = new(0.3f, 0.3f, 0.3f, 0.5f);
            _farPlane.Color = new(0.3f, 0.3f, 0.3f, 0.5f);

            _leftLine.Color = new(0.3f, 0.3f, 0.3f, 1.0f);
            _leftLine.DrawingMode = PrimitiveType.Lines;

            _rightLine.Color = new(0.3f, 0.3f, 0.3f, 1.0f);
            _rightLine.DrawingMode = PrimitiveType.Lines;

            _nearBorder.Color = new(0.3f, 0.3f, 0.3f, 1.0f);
            _nearBorder.DrawingMode = PrimitiveType.LineLoop;

            _farBorder.Color = new(0.3f, 0.3f, 0.3f, 1.0f);
            _farBorder.DrawingMode = PrimitiveType.LineLoop;
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            _leftLine.Draw(model, view, projection);
            _leftLine.Draw(Matrix4.CreateRotationX(-MathHelper.PiOver4) * model, view, projection);

            _rightLine.Draw(model, view, projection);
            _rightLine.Draw(Matrix4.CreateRotationX(-MathHelper.PiOver4) * model, view, projection);

            _nearBorder.Draw(model, view, projection);
            _farBorder.Draw(model, view, projection);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _nearPlane.Draw(Matrix4.CreateTranslation(new(0.0f, 0.0f, -Near)) * model, view, projection);
            _farPlane.Draw(Matrix4.CreateTranslation(new(0.0f, 0.0f, -Far)) * model, view, projection);

            GL.Disable(EnableCap.Blend);
        }

        private static float[] GetNearPlane(float aspectRatio, float fov, float near)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            return GetFrontPlane(nearWidth, nearHeight);
        }

        private static float[] GetFarPlane(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float farHeight = far / near * nearHeight;
            float farWidth = far / near * nearWidth;

            return GetFrontPlane(farWidth, farHeight);
        }

        private static float[] GetFrontPlane(float width, float height)
        {
            float[] template =
            {
                -width, -height, 0,
                 width, -height, 0,
                 width,  height, 0,
                 width,  height, 0,
                -width,  height, 0,
                -width, -height, 0,
            };
            return template;
        }

        private static float[] GetLeftLine(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float farHeight = far / near * nearHeight;
            float farWidth = far / near * nearWidth;

            float[] template =
{
                 0,        0,          0,
                -farWidth, farHeight, -far,
            };
            return template;
        }

        private static float[] GetRightLine(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float farHeight = far / near * nearHeight;
            float farWidth = far / near * nearWidth;

            float[] template =
            {
                 0,        0,          0,
                 farWidth, farHeight, -far,
            };
            return template;
        }

        private static float[] GetNearPlaneBorder(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float[] template =
            {
                 -nearWidth, -nearHeight, -near,
                  nearWidth, -nearHeight, -near,
                  nearWidth,  nearHeight, -near,
                 -nearWidth,  nearHeight, -near,
                 -nearWidth, -nearHeight, -near,
            };
            return template;
        }

        private static float[] GetFarPlaneBorder(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float farHeight = far / near * nearHeight;
            float farWidth = far / near * nearWidth;

            float[] template =
            {
                 -farWidth, -farHeight, -far,
                  farWidth, -farHeight, -far,
                  farWidth,  farHeight, -far,
                 -farWidth,  farHeight, -far,
                 -farWidth, -farHeight, -far,
            };
            return template;
        }
    }
}
