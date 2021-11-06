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

        public static float[] GetParallelepiped(float a, float b, float c)
        {
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
