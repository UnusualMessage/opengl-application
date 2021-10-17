using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using TransformationApplication.SceneObjects.Base;

namespace TransformationApplication.Base
{
    public class SceneObjectDrawer
    {
        private readonly SceneObject _sceneObject;

        public SceneObjectDrawer(SceneObject sceneObject)
        {
            _sceneObject = sceneObject;
        }

        public void Draw(Matrix4 modelMatrix, Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            Shader objectShader = _sceneObject.GetShader();
            objectShader.Use();

            objectShader.SetMatrix4("model", modelMatrix);
            objectShader.SetMatrix4("view", viewMatrix);
            objectShader.SetMatrix4("projection", projectionMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, _sceneObject.GetVertices().Length);
        }
    }
}