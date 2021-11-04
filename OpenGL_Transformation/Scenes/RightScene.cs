using TransformationApplication.Scenes.Base;
using TransformationApplication.Base;
using TransformationApplication.SceneObjects;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace TransformationApplication.Scenes
{
    public class RightScene : IRenderable
    {
        private float _width;
        private float _height;

        private readonly List<VisibleObject> _visibleObjects = new();

        private float AspectRatio => _width / _height;

        public RightScene()
        {
            _visibleObjects.Add(new VisibleObject(new Shader("C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\VertexShader.vert",
                "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\FragmentShader.frag"), Vertices.Cube));

            GL.Enable(EnableCap.DepthTest);
        }
        
        public Matrix4 Render(Transformation cameraTransformation, Transformation modelTransformation)
        {
            GL.ClearColor(Color4.Gray);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 view = Matrix4.Identity * Matrix4.CreateTranslation(new(0.0f, -5.0f, -10.0f));
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

            foreach (VisibleObject visibleObject in _visibleObjects)
            {
                visibleObject.Bind();
                visibleObject.UpdateTransformation(modelTransformation);
                visibleObject.Draw(view, projection, new(1.0f, 1.0f, 1.0f));

                visibleObject.UpdateTransformation(cameraTransformation);
                visibleObject.Draw(view, projection, new(1.0f, 1.0f, 0.0f));
            }

            return view;
        }

        public void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
