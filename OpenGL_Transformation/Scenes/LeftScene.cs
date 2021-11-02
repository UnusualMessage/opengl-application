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
    public class LeftScene : IRenderable
    {
        private float _width;
        private float _height;

        private readonly ViewCamera _camera;

        private readonly List<VisibleObject> _visibleObjects = new();

        private float AspectRatio => _width / _height;

        public LeftScene(Transformation cameraTransformation)
        {
            _visibleObjects.Add(new VisibleObject(new Shader("C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\VertexShader.vert",
                "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\FragmentShader.frag"), Cube.Vertices));

            _camera = new(cameraTransformation, AspectRatio);
        }

        public Matrix4 Render(Transformation cameraTransformation, Transformation modelTransformation)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _camera.AspectRatio = AspectRatio;
            cameraTransformation.Rotation.Yaw += -90.0f;
            _camera.UpdateTransformation(cameraTransformation);

            Matrix4 view = _camera.GetViewMatrix();
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

            foreach (VisibleObject visibleObject in _visibleObjects)
            {
                visibleObject.Bind();
                visibleObject.UpdateTransformation(modelTransformation);
                visibleObject.Draw(view, projection, new(1.0f, 1.0f, 1.0f));
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
