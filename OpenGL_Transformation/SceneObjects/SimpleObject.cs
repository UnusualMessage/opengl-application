using TransformationApplication.Base;
using TransformationApplication.Mathematics;

using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects
{
    public class SimpleObject : SceneComponent, IVisible
    {
        public Color4 Color { private get; set; } = new(1.0f, 1.0f, 1.0f, 1.0f);
        public PrimitiveType DrawingMode { private get; set; } = PrimitiveType.Triangles;

        private readonly int _vertexBufferObject;
        private readonly int _vertexArrayObject;

        public Shader Shader { get; }
        public float[] Vertices { get; private set; }

        public SimpleObject(Shader shader, float[] vertices)
        {
            Transformation = new();
            Shader = shader;
            Vertices = vertices;

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                Vertices.Length * sizeof(float),
                Vertices,
                BufferUsageHint.DynamicDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void SetVertices(float[] vertices)
        {
            Vertices = vertices;

            Bind();
            GL.BufferData(
                BufferTarget.ArrayBuffer,
                Vertices.Length * sizeof(float),
                Vertices,
                BufferUsageHint.DynamicDraw);
        }

        private void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BindVertexArray(_vertexArrayObject);
        }

        public void Draw(Matrix4 view, Matrix4 projection)
        {
            Matrix4 model = GetModelMatrix();
            Draw(model, view, projection);
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            Shader.Use();

            Shader.SetMatrix4("model", model);
            Shader.SetMatrix4("view", view);
            Shader.SetMatrix4("projection", projection);

            Vector4 color = new(Color.R, Color.G, Color.B, Color.A);
            Shader.SetVector4("inputColor", color);

            Bind();
            GL.DrawArrays(DrawingMode, 0, Vertices.Length / 3);
        }

        private Matrix4 GetModelMatrix()
        {
            return TransformationMatrix.GetTransformationMatrix(Transformation);
        }
    }
}
