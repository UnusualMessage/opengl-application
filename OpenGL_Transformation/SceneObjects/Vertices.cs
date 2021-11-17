namespace TransformationApplication.SceneObjects
{
    public static class Vertices
    {
        public struct Point
        {
            private readonly float _x;
            private readonly float _y;
            private readonly float _z;

            public float X => _x;
            public float Y => _y;
            public float Z => _z;

            public Point(float x, float y, float z)
            {
                _x = x;
                _y = y;
                _z = z;
            }
        }

        public static readonly float[] FieldLine =
        {
            0.0f, 0.0f, -10.0f,
            0.0f, 0.0f, 10.0f,
        };

        public static float[] GetLine(Point first, Point second)
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
