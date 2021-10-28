using TransformationApplication.Scenes.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Base;
using TransformationApplication.SceneObjects;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

using System.Collections.Generic;

namespace TransformationApplication.Scenes
{
    public class LeftScene : Scene
    {
        private readonly int _vertexBufferObject;
        private readonly int _vertexArrayObject;

        private float _width;
        private float _height;

        private readonly ViewCamera _camera;

        private readonly List<SceneObject> _sceneObjects = new();

        private float AspectRatio => _width / _height;

        public LeftScene(float width, float height)
        {
            _sceneObjects.Add(new Cube("C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\VertexShader.vert",
                "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\Model\\FragmentShader.frag"));

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer,
                _sceneObjects[0].GetVertices().Length * sizeof(float),
                _sceneObjects[0].GetVertices(), BufferUsageHint.StaticDraw);

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            _width = width;
            _height = height;

            _camera = new(Vector3.UnitZ * 3, AspectRatio);
        }

        public override void Load()
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Render(Transformation cameraTransformation, Transformation modelTransformation)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            const float translationScale = 0.5f;

            Vector3 cameraTranslation = new(cameraTransformation.Translation.TranslationByX * translationScale,
                cameraTransformation.Translation.TranslationByY * translationScale,
                cameraTransformation.Translation.TranslationByZ * translationScale);

            _camera.AspectRatio = AspectRatio;
            _camera.Position = new(cameraTranslation);
            _camera.Pitch = cameraTransformation.Rotation.RotationByX;
            _camera.Yaw = -90.0f + cameraTransformation.Rotation.RotationByY;
            _camera.Roll = cameraTransformation.Rotation.RotationByZ;
            Matrix4 view = _camera.GetViewMatrix();

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver2, AspectRatio, 0.1f, 100f);

            foreach (SceneObject sceneObject in _sceneObjects)
            {
                Vector3 modelTranslation = new(modelTransformation.Translation.TranslationByX * translationScale,
                    modelTransformation.Translation.TranslationByY * translationScale,
                    modelTransformation.Translation.TranslationByZ * translationScale);

                Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(modelTransformation.Rotation.RotationByX));
                model *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(modelTransformation.Rotation.RotationByY));
                model *= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(modelTransformation.Rotation.RotationByZ));
                model *= Matrix4.CreateTranslation(modelTranslation);

                SceneObjectDrawer.Draw(sceneObject, model, view, projection);
            }
        }

        public override void UpdateAspectRatio(float width, float height)
        {
            _width = width;
            _height = height;
        }
    }
}
