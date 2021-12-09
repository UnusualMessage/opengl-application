namespace TransformationApplication.SceneObjects
{
    public struct Vertex
    {
        public float X { get; }
        public float Y { get; }
        public float Z { get; }

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public static class Vertices
    {
        public static float[] GetLine(Vertex first, Vertex second)
        {
            float[] template =
            {
                first.X, first.Y, first.Z,
                second.X, second.Y, second.Z
            };

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
