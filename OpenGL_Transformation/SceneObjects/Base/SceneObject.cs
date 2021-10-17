using TransformationApplication.Base;

using System;

namespace TransformationApplication.SceneObjects.Base
{
    public abstract class SceneObject : IDisposable
    {
        public abstract float[] GetVertices();
        public abstract Shader GetShader();
        public abstract int GetLocation(string target);

        public abstract void Dispose();
    }
}
