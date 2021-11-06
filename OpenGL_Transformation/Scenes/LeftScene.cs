using TransformationApplication.Scenes.Base;
using TransformationApplication.Base;
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

        private readonly ViewCamera _userCamera = new();

        private readonly List<VisibleObject> _visibleObjects;

        private float AspectRatio => _width / _height;

        public LeftScene(List<VisibleObject> objects)
        {
            _visibleObjects = objects;

            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
        }

        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view)
        {
            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            GL.ClearColor(new Color4(0.1f, 0.1f, 0.1f, 1.0f));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _userCamera.AspectRatio = AspectRatio;
            cameraTransformationCopy.Rotation.Yaw += -90.0f;

            _userCamera.UpdateTransformation(cameraTransformationCopy);
            view = _userCamera.GetViewMatrix();
            Matrix4 projection = _userCamera.GetProjectionMatrix(1.0f, 10.1f);

            foreach (VisibleObject visibleObject in _visibleObjects)
            {
                visibleObject.UpdateTransformation(modelTransformationCopy);
                visibleObject.Draw(view, projection);
            }
        }

        public void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
