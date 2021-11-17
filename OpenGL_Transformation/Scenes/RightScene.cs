using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace TransformationApplication.Scenes
{
    public class RightScene
    {
        private readonly ViewCamera _userCamera;
        private readonly List<IVisible> _visibleObjects;

        public float AspectRatio { get; set; }

        public RightScene(ViewCamera userCamera, List<IVisible> objects)
        {
            _visibleObjects = objects;
            _userCamera = userCamera;
        }
        
        public void Render(Transformation cameraTransformation, Transformation modelTransformation)
        {
            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            _userCamera.AspectRatio = AspectRatio;
            float yaw = MathHelper.DegreesToRadians(_userCamera.Yaw);
            float pitch = MathHelper.DegreesToRadians(_userCamera.Pitch);
            Vector3 userCameraPosition = new(_userCamera.X, _userCamera.Y, -_userCamera.Z);

            Matrix4 model = TransformationMatrix.GetTransformationMatrix(modelTransformationCopy);
            Matrix4 view = Matrix4.Identity * Matrix4.CreateRotationY(yaw) * Matrix4.CreateRotationX(pitch) * Matrix4.CreateTranslation(userCameraPosition);
            Matrix4 projection = _userCamera.GetProjectionMatrix(1.0f, 100.0f);

            // model
            _visibleObjects[3].Draw(model, view, projection);
            _visibleObjects[2].Draw(model, view, projection);
            _visibleObjects[0].Draw(model, view, projection);

            // camera
            cameraTransformationCopy.Rotation.Pitch = -cameraTransformationCopy.Rotation.Pitch;
            cameraTransformationCopy.Rotation.Roll = -cameraTransformationCopy.Rotation.Roll;
            model = TransformationMatrix.GetTransformationMatrix(cameraTransformationCopy);
            _visibleObjects[4].Draw(Matrix4.CreateRotationY(MathHelper.Pi) * model, view, projection);
            _visibleObjects[1].Draw(model, view, projection);
            _visibleObjects[5].Draw(model, view, projection);
        }
    }
}
