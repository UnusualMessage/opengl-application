using TransformationApplication.Scenes.Base;
using TransformationApplication.Base;
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

        private readonly ViewCamera _userCamera;

        private readonly List<VisibleObject> _visibleObjects;

        private float AspectRatio => _width / _height;

        public RightScene(ViewCamera userCamera, List<VisibleObject> objects)
        {
            _visibleObjects = objects;
            _userCamera = userCamera;
        }
        
        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view)
        {
            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _userCamera.AspectRatio = AspectRatio;
            _userCamera.Fov = 70.0f;

            float yaw = MathHelper.DegreesToRadians(_userCamera.Yaw);
            float pitch = MathHelper.DegreesToRadians(_userCamera.Pitch);

            view = Matrix4.Identity * Matrix4.CreateRotationY(yaw) * Matrix4.CreateRotationX(pitch) * Matrix4.CreateTranslation(0.0f, 0.0f, -20.0f);
            Matrix4 projection = _userCamera.GetProjectionMatrix(1.0f, 100.0f);

            // model
            _visibleObjects[0].UpdateTransformation(modelTransformationCopy);
            _visibleObjects[0].Draw(view, projection);

            _visibleObjects[2].UpdateTransformation(modelTransformationCopy);
            _visibleObjects[2].Draw(view, projection);

            // camera
            _visibleObjects[1].UpdateTransformation(cameraTransformationCopy);
            _visibleObjects[1].Draw(view, projection);
        }

        public void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
