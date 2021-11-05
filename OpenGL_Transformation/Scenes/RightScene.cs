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

        private readonly ViewCamera _userCamera;

        private readonly List<VisibleObject> _visibleObjects;

        private float AspectRatio => _width / _height;

        public RightScene(Transformation cameraTransformation, List<VisibleObject> objects)
        {
            _visibleObjects = objects;
            _userCamera = new(cameraTransformation, AspectRatio);
        }
        
        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view)
        {
            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            GL.ClearColor(new Color4(0.6f, 0.6f, 0.6f, 1.0f));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            view = Matrix4.Identity;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

            // model
            _visibleObjects[0].UpdateTransformation(modelTransformationCopy);
            _visibleObjects[0].Draw(view, projection, new(1.0f, 1.0f, 1.0f));

            _visibleObjects[2].UpdateTransformation(modelTransformationCopy);
            _visibleObjects[2].Draw(view, projection, new(1.0f, 1.0f, 1.0f));

            // camera
            cameraTransformationCopy.Position.Y -= 0.5f;
            _visibleObjects[1].UpdateTransformation(cameraTransformationCopy);
            _visibleObjects[1].Draw(view, projection, new(1.0f, 1.0f, 0.0f));
        }

        public void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
