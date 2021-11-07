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
