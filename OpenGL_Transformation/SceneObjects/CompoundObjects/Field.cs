using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.CompoundObjects
{
    public class Field : SceneComponent, IVisible
    {
        private readonly Color4 _commonLineColor = new(0.2f, 0.2f, 0.2f, 1.0f);
        private readonly Color4 _redLineColor = new(0.4f, 0.0f, 0.0f, 1.0f);
        private readonly Color4 _blueLineColor = new(0.0f, 0.0f, 0.4f, 1.0f);

        private readonly SimpleObject _line;

        public Field(Shader shader, float[] vertices)
        {
            _line = new(shader, vertices);
            _line.DrawingMode = OpenTK.Graphics.OpenGL4.PrimitiveType.Lines;
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            const float leftBorder = -10.0f;
            const float rightBorder = 10.0f;

            _line.ResetTransformation();
            _line.Color = _commonLineColor;
            for (float i = leftBorder; i <= rightBorder; ++i)
            {
                _line.X = i;
                if (i != 0.0f)
                {
                    _line.Draw(view, projection);
                }
            }
            _line.X = 0.0f;
            _line.Color = _blueLineColor;
            _line.Draw(view, projection);

            _line.ResetTransformation();
            _line.Yaw = 90.0f;
            _line.Color = _commonLineColor;
            for (float i = leftBorder; i <= rightBorder; ++i)
            {
                _line.Z = i;
                if (i != 0.0f)
                {
                    _line.Draw(view, projection);
                }
            }
            _line.Z = 0.0f;
            _line.Color = _redLineColor;
            _line.Draw(view, projection);
        }
    }
}
