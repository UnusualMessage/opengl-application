using TransformationApplication.SceneObjects;

namespace TransformationApplication.Scenes.Base
{
    public abstract class Scene
    {
        public abstract void Render(Transformation cameraTransformation, Transformation modelTransformation);
        public abstract void UpdateAspectRatio(float width, float height);
    }
}
