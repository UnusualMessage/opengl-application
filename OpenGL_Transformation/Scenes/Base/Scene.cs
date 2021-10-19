namespace TransformationApplication.Scenes.Base
{
    public abstract class Scene
    {
        public abstract void Load();
        public abstract void Render(int width, int height);
    }
}
