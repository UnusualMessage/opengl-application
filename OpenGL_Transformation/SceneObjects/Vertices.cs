using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects
{
    public static class Vertices
    {
        public static readonly float[] Quad =
        {
            -0.5f, -0.5f, -0.5f,
             0.5f, -0.5f, -0.5f,
             0.5f, -0.5f,  0.5f,
             0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f,  0.5f,
            -0.5f, -0.5f, -0.5f,
        };

        public static readonly float[] FieldLine =
        {
            0.0f, 0.0f, -10.0f,
            0.0f, 0.0f, 10.0f,
        };

        public static float[] GetNearPlane(float aspectRatio, float fov, float near)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float[] template = 
            {
                -nearWidth, -nearHeight, 0,
                 nearWidth, -nearHeight, 0,
                 nearWidth,  nearHeight, 0,
                 nearWidth,  nearHeight, 0,
                -nearWidth,  nearHeight, 0,
                -nearWidth, -nearHeight, 0,
            };
            return template;
        }

        public static float[] GetFarPlane(float aspectRatio, float fov, float near, float far)
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(fov / 2));
            float nearHeight = near * tangent;
            float nearWidth = nearHeight * aspectRatio;

            float farHeight = nearHeight;
            float farWidth = nearWidth;

            float[] template = { };
            return template;
        }

        public static float[] GetParallelepiped(float x, float y, float z)
        {
            float[] template =
            {
                -x, -y, -z,
                 x, -y, -z,
                 x,  y, -z,
                 x,  y, -z,
                -x,  y, -z,
                -x, -y, -z,

                -x, -y,  z,
                 x, -y,  z,
                 x,  y,  z,
                 x,  y,  z,
                -x,  y,  z,
                -x, -y,  z,

                -x,  y,  z,
                -x,  y, -z,
                -x, -y, -z,
                -x, -y, -z,
                -x, -y,  z,
                -x,  y,  z,

                 x,  y,  z,
                 x,  y, -z,
                 x, -y, -z,
                 x, -y, -z,
                 x, -y,  z,
                 x,  y,  z,

                -x, -y, -z,
                 x, -y, -z,
                 x, -y,  z,
                 x, -y,  z,
                -x, -y,  z,
                -x, -y, -z,

                -x,  y, -z,
                 x,  y, -z,
                 x,  y,  z,
                 x,  y,  z,
                -x,  y,  z,
                -x,  y, -z,
            };

            return template;
        }
    }
}
