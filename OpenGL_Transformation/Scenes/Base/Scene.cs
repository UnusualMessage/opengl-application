using TransformationApplication.Mathematics;

using OpenTK.Mathematics;

namespace TransformationApplication.Scenes.Base
{
    public interface IRenderable
    {
        public Matrix4 Render(Transformation cameraTransformation, Transformation modelTransformation);
        public void UpdateAspectRatio(float width, float height);
    }
}
