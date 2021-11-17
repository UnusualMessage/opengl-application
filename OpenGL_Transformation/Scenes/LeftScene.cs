using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Mathematics;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace TransformationApplication.Scenes
{
    public class LeftScene
    {
        private const float Near = 1.0f;
        private const float Far = 10.1f;

        private readonly ViewCamera _userCamera = new(45.0f);
        private readonly List<IVisible> _visibleObjects;

        public float AspectRatio { get; set; }

        public LeftScene(List<IVisible> objects)
        {
            _visibleObjects = objects;

            GL.Enable(EnableCap.DepthTest);
        }

        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view)
        {
            GL.ClearColor(new Color4(0.1f, 0.1f, 0.1f, 1.0f));
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Transformation cameraTransformationCopy = cameraTransformation.Clone();
            Transformation modelTransformationCopy = modelTransformation.Clone();

            _userCamera.AspectRatio = AspectRatio;
            cameraTransformationCopy.Rotation.Yaw += -90.0f;
            cameraTransformationCopy.Rotation.Pitch = -cameraTransformationCopy.Rotation.Pitch;
            cameraTransformationCopy.Rotation.Roll = -cameraTransformationCopy.Rotation.Roll;

            _userCamera.UpdateTransformation(cameraTransformationCopy);

            Matrix4 model = TransformationMatrix.GetTransformationMatrix(modelTransformationCopy);
            view = _userCamera.GetViewMatrix();
            Matrix4 projection = _userCamera.GetProjectionMatrix(Near, Far);

            foreach (IVisible visibleObject in _visibleObjects)
            {
                visibleObject.Draw(model, view, projection);
            }
        }
    }
}
