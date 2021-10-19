using TransformationApplication.Scenes.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Base;
using TransformationApplication.SceneObjects;

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

            GL.BufferData(BufferTarget.ArrayBuffer,
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

        public override void Render(int width, int height)
        {
            GL.ClearColor(Color4.Red);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            foreach(SceneObject sceneObject in _sceneObjects)
            {
                Matrix4 identity = Matrix4.Identity;
                SceneObjectDrawer.Draw(sceneObject, identity, identity, identity);
            }
        }
    }
}
