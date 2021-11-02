using TransformationApplication.Base;
using TransformationApplication.Mathematics;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects.Base
{
    public class VisibleObject : SceneObject
    {
        private readonly int _vertexBufferObject;
        private readonly int _vertexArrayObject;

        public Shader Shader { get; }
        public float[] Vertices { get; }

        public VisibleObject(Shader shader, float[] vertices)
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
                BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        }

        public virtual void Draw(Matrix4 view, Matrix4 projection, Vector3 color)
        {
            Shader.Use();

            Shader.SetMatrix4("model", GetModelMatrix());
            Shader.SetMatrix4("view", view);
            Shader.SetMatrix4("projection", projection);

            Shader.SetVector3("inputColor", color);

            GL.DrawArrays(PrimitiveType.Triangles, 0, Vertices.Length / 3);
        }

        private Matrix4 GetModelMatrix()
        {
            return TransformationMatrix.GetTransformationMatrix(Transformation);
        }
    }
}
