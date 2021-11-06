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

        public RightScene(Transformation cameraTransformation, List<VisibleObject> objects)
        {
            _visibleObjects = objects;
            _userCamera = new(cameraTransformation, AspectRatio);
        }
        
        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view)
        {
            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            view = Matrix4.Identity * Matrix4.CreateTranslation(0.0f, -5.0f, -10.0f);
            view *= Matrix4.CreateRotationX(MathHelper.PiOver4);
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

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
