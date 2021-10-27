using TransformationApplication.Scenes.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Base;
using TransformationApplication.SceneObjects;
using TransformationApplication.SceneObjects.Model;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace TransformationApplication.Scenes
{
    public class RightScene : Scene
    {
        private readonly int _vertexBufferObject;
        private readonly int _vertexArrayObject;
        
        private readonly List<SceneObject> _sceneObjects = new();

        public RightScene()
        {
            _sceneObjects.Add(new Cube("C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\VertexShader.vert",
                "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\FragmentShader.frag"));

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(
                BufferTarget.ArrayBuffer,
                _sceneObjects[0].GetVertices().Length * sizeof(float),
                _sceneObjects[0].GetVertices(), BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
        }

        public override void Load()
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Render(int width, int height, Rotation modelRotation, Translation modelTranslation)
        {
            float basicTranslation = -3.0f;

            GL.ClearColor(Color4.Red);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            foreach (SceneObject sceneObject in _sceneObjects)
            {
                Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(modelRotation.RotationByX));
                model *= Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(modelRotation.RotationByY));
                model *= Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(modelRotation.RotationByZ));
                model *= Matrix4.CreateTranslation(new Vector3(
                    modelTranslation.TranslationByX,
                    modelTranslation.TranslationByY,
                    basicTranslation + modelTranslation.TranslationByZ));

                Matrix4 view = Matrix4.Identity;

                Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, width / (float)height, 0.1f, 100f);

                SceneObjectDrawer.Draw(sceneObject, model, view, projection);
            }
        }
    }
}
