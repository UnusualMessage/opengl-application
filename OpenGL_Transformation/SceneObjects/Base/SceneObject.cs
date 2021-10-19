using TransformationApplication.Base;

namespace TransformationApplication.SceneObjects.Base
{
    public abstract class SceneObject
    {
        public abstract float[] GetVertices();
        public abstract Shader GetShader();
    }
}
