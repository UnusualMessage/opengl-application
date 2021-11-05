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

        private readonly ViewCamera _userCamera;

        private readonly List<VisibleObject> _visibleObjects;

        private float AspectRatio => _width / _height;

        public LeftScene(Transformation cameraTransformation, List<VisibleObject> objects)
        {
            _visibleObjects = objects;
            _userCamera = new(cameraTransformation, AspectRatio);
            GL.Enable(EnableCap.DepthTest);
        }

        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view)
        {
            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _userCamera.AspectRatio = AspectRatio;
            cameraTransformationCopy.Rotation.Yaw += -90.0f;
            cameraTransformationCopy.Position.Y -= 0.5f;

            _userCamera.UpdateTransformation(cameraTransformationCopy);
            view = _userCamera.GetViewMatrix();
            Matrix4 projection = _userCamera.GetProjectionMatrix();

            foreach (VisibleObject visibleObject in _visibleObjects)
            {
                visibleObject.UpdateTransformation(modelTransformationCopy);
                visibleObject.Draw(view, projection, new(1.0f, 1.0f, 1.0f));
            }

            view.M42 -= 0.5f;
        }

        public void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
