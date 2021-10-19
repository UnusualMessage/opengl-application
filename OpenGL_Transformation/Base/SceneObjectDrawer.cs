using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using TransformationApplication.SceneObjects.Base;

namespace TransformationApplication.Base
{
    public static class SceneObjectDrawer
    {
        public static void Draw(SceneObject sceneObject, Matrix4 modelMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            Shader objectShader = sceneObject.GetShader();
            objectShader.Use();

            objectShader.SetMatrix4("model", modelMatrix);
            objectShader.SetMatrix4("view", viewMatrix);
            objectShader.SetMatrix4("projection", projectionMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, sceneObject.GetVertices().Length / 3);
        }
    }
}