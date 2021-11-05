using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects
{
    public class Field : VisibleObject
    {
        private readonly VisibleObject _cell;

        public Field(Shader shader, float[] vertices) : base(shader, vertices)
        {
            _cell = new(shader, vertices);
            _cell.DrawingMode = OpenTK.Graphics.OpenGL4.PrimitiveType.Lines;
        }

        public override void Draw(Matrix4 view, Matrix4 projection, Vector3 color)
        {
            _cell.ResetTransformation();
            for (float i = -10.0f; i <= 10.0f; ++i)
            {
                _cell.X = i;
                if (i != 0.0f)
                {
                    _cell.Draw(view, projection, new(0.2f, 0.2f, 0.2f));
                }
            }
            _cell.X = 0.0f;
            _cell.Draw(view, projection, new(0.0f, 0.0f, 0.4f));

            _cell.ResetTransformation();

            _cell.Yaw = 90.0f;
            for (float i = -10.0f; i <= 10.0f; ++i)
            {
                _cell.Z = i;
                if (i != 0.0f)
                {
                    _cell.Draw(view, projection, new(0.2f, 0.2f, 0.2f));
                }
            }
            _cell.Z = 0.0f;
            _cell.Draw(view, projection, new(0.4f, 0.0f, 0.0f));
        }
    }
}
