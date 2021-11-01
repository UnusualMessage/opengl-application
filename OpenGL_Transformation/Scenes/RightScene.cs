using TransformationApplication.Scenes.Base;
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

        private float _width;
        private float _height;

        private readonly List<VisibleObject> _visibleObjects = new();

        private float AspectRatio => _width / _height;

        public RightScene()
        {
            _visibleObjects.Add(new VisibleObject(new Shader("C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\VertexShader.vert",
                "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\FragmentShader.frag"), Cube.Vertices));

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer,
                _visibleObjects[0].Vertices.Length * sizeof(float),
                _visibleObjects[0].Vertices, BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.Enable(EnableCap.DepthTest);
        }
        
        public override void Render(Transformation cameraTransformation, Transformation modelTransformation)
        {
            GL.ClearColor(Color4.Gray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = Matrix4.Identity * Matrix4.CreateTranslation(new(0.0f, -5.0f, -10.0f));
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

            foreach (VisibleObject visibleObject in _visibleObjects)
            {
                visibleObject.UpdateTransformation(modelTransformation);
                visibleObject.Draw(view, projection);

                visibleObject.UpdateTransformation(cameraTransformation);
                visibleObject.Draw(view, projection);
            }
        }

        public override void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
