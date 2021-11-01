using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.Scenes.Base
{
    public class VisibleObject : SceneObject
    {
        public Shader Shader { get; }
        public float[] Vertices { get; }

        public VisibleObject(Shader shader, float[] vertices)
        {
            Shader = shader;
            Vertices = vertices;
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
            Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Yaw));
            model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Pitch));
            model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Roll));
            model *= Matrix4.CreateTranslation(Position);

            return model;
        }
    }
}
