using TransformationApplication.Scenes.Base;
using TransformationApplication.Base;
using TransformationApplication.SceneObjects;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace TransformationApplication.Scenes
{
    public class LeftScene : Scene
    {
        private float _width;
        private float _height;

        private readonly ViewCamera _camera;

        private readonly List<VisibleObject> _visibleObjects = new();

        private float AspectRatio => _width / _height;

        public LeftScene()
        {
            _visibleObjects.Add(new VisibleObject(new Shader("C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\VertexShader.vert",
                "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\FragmentShader.frag"), Cube.Vertices));

            _camera = new(Vector3.UnitZ * 3, AspectRatio);
        }

        public override void Render(Transformation cameraTransformation, Transformation modelTransformation)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Vector3 cameraTranslation = new(cameraTransformation.Translation.TranslationByX,
                cameraTransformation.Translation.TranslationByY,
                cameraTransformation.Translation.TranslationByZ);

            _camera.AspectRatio = AspectRatio;
            _camera.Position = new(cameraTranslation);
            _camera.Pitch = cameraTransformation.Rotation.RotationByX;
            _camera.Yaw = MathHelper.RadiansToDegrees(-MathHelper.PiOver2) + cameraTransformation.Rotation.RotationByY;
            _camera.Roll = cameraTransformation.Rotation.RotationByZ;

            Matrix4 view = _camera.GetViewMatrix();
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

            foreach (VisibleObject visibleObject in _visibleObjects)
            {
                Vector3 modelTranslation = new(modelTransformation.Translation.TranslationByX,
                    modelTransformation.Translation.TranslationByY,
                    modelTransformation.Translation.TranslationByZ);

                visibleObject.Position = modelTranslation;
                visibleObject.Yaw = modelTransformation.Rotation.RotationByX;
                visibleObject.Pitch = modelTransformation.Rotation.RotationByY;
                visibleObject.Roll = modelTransformation.Rotation.RotationByZ;

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
