using TransformationApplication.Mathematics.Base;

namespace TransformationApplication.Mathematics
{
    public class Transformation
    {
        public Rotation Rotation { get; set; }
        public Translation Translation { get; set; }

        public Transformation(Rotation rotation, Translation translation)
        {
            Rotation = rotation;
            Translation = translation;
        }
    }
}
