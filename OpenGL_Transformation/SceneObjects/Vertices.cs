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
            0.0f, -0.5f, -10.0f,
            0.0f, -0.5f, 10.0f,
        };

        public static float[] GetParallelepiped(float a, float b, float c)
        {
            a = MathHelper.Clamp(a, 0.0f, 1.0f);
            b = MathHelper.Clamp(b, 0.0f, 1.0f);
            c = MathHelper.Clamp(c, 0.0f, 1.0f);

            float[] template =
            {
                -a, -b, -c,
                 a, -b, -c,
                 a,  b, -c,
                 a,  b, -c,
                -a,  b, -c,
                -a, -b, -c,

                -a, -b,  c,
                 a, -b,  c,
                 a,  b,  c,
                 a,  b,  c,
                -a,  b,  c,
                -a, -b,  c,

                -a,  b,  c,
                -a,  b, -c,
                -a, -b, -c,
                -a, -b, -c,
                -a, -b,  c,
                -a,  b,  c,

                 a,  b,  c,
                 a,  b, -c,
                 a, -b, -c,
                 a, -b, -c,
                 a, -b,  c,
                 a,  b,  c,

                -a, -b, -c,
                 a, -b, -c,
                 a, -b,  c,
                 a, -b,  c,
                -a, -b,  c,
                -a, -b, -c,

                -a,  b, -c,
                 a,  b, -c,
                 a,  b,  c,
                 a,  b,  c,
                -a,  b,  c,
                -a,  b, -c,
            };

            return template;
        }
    }
}
