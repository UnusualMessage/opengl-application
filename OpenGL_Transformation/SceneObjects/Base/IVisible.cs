using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public interface IVisible
    {
        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection);
    }
}
