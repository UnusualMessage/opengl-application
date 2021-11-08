using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects
{
    public class Axis : SceneComponent, IVisible
    {
        private readonly SimpleObject _axisLine;

        public Axis(Shader shader, float length)
        {
            _axisLine = new(shader, GetAxisLine(length));
            _axisLine.DrawingMode = PrimitiveType.Lines;
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            GL.Enable(EnableCap.LineSmooth);
            _axisLine.Color = Color4.Blue;
            _axisLine.Draw(model, view, projection);

            _axisLine.Color = Color4.Red;
            _axisLine.Draw(Matrix4.CreateRotationY(MathHelper.PiOver2) * model, view, projection);

            _axisLine.Color = Color4.Green;
            _axisLine.Draw(Matrix4.CreateRotationX(-MathHelper.PiOver2) * model, view, projection);
            GL.Disable(EnableCap.LineSmooth);
        }

        private static float[] GetAxisLine(float length)
        {
            float[] template =
            {
                0.0f, 0.0f, 0.0f,
                0.0f, 0.0f, length
            };

            return template;
        }
    }
}
