using TransformationApplication.Scenes.Base;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace TransformationApplication.Scenes
{
    public class RightScene : Scene
    {
        public RightScene()
        {

        }

        public override void Load()
        {
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Render()
        {
            GL.ClearColor(Color4.Red);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
