using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects
{
    public class Field : SceneComponent, IVisible
    {
        private readonly SimpleObject _line;

        public Field(Shader shader, float[] vertices)
        {
            _line = new(shader, vertices);
            _line.DrawingMode = OpenTK.Graphics.OpenGL4.PrimitiveType.Lines;
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            _line.ResetTransformation();
            _line.Color = new(0.2f, 0.2f, 0.2f, 1.0f);
            for (float i = -10.0f; i <= 10.0f; ++i)
            {
                _line.X = i;
                if (i != 0.0f)
                {
                    _line.Draw(view, projection);
                }
            }
            _line.X = 0.0f;
            _line.Color = new(0.0f, 0.0f, 0.4f, 1.0f);
            _line.Draw(view, projection);

            _line.ResetTransformation();

            _line.Yaw = 90.0f;
            _line.Color = new(0.2f, 0.2f, 0.2f, 1.0f);
            for (float i = -10.0f; i <= 10.0f; ++i)
            {
                _line.Z = i;
                if (i != 0.0f)
                {
                    _line.Draw(view, projection);
                }
            }
            _line.Z = 0.0f;
            _line.Color = new(0.4f, 0.0f, 0.0f, 1.0f);
            _line.Draw(view, projection);
        }
    }
}
