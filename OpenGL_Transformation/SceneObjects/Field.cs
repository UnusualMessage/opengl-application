using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

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
            _cell.Transformation.Reset();
            for (float i = -10.0f; i <= 10.0f; ++i)
            {
                _cell.Transformation.Position.X = i;
                _cell.Draw(TransformationMatrix.GetTransformationMatrix(_cell.Transformation), view, projection, new(0.2f, 0.2f, 0.2f));
            }
            _cell.Transformation.Reset();

            _cell.Transformation.Rotation.Yaw = 90.0f;
            for (float i = -10.0f; i <= 10.0f; ++i)
            {

                _cell.Transformation.Position.Z = i;
                _cell.Draw(TransformationMatrix.GetTransformationMatrix(_cell.Transformation), view, projection, new(0.2f, 0.2f, 0.2f));
            }
        }
    }
}
