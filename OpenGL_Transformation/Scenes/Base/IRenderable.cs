using TransformationApplication.Mathematics;

using OpenTK.Mathematics;

namespace TransformationApplication.Scenes.Base
{
    public interface IRenderable
    {
        public void Render(Transformation cameraTransformation, Transformation modelTransformation, out Matrix4 view);
        public void UpdateAspectRatio(float width, float height);
    }
}
