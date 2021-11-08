using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects
{
    public class Frustum : SceneComponent, IVisible
    {
        public Frustum() { }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            throw new System.NotImplementedException();
        }
    }
}
