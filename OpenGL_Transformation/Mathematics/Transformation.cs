using TransformationApplication.Mathematics.Base;

namespace TransformationApplication.Mathematics
{
    public class Transformation
    {
        public Rotation Rotation { get; set; }
        public Translation Translation { get; set; }

        public Transformation()
        {
            Rotation = new Rotation();
            Translation = new Translation();
        }

        public Transformation(Rotation rotation, Translation translation)
        {
            Rotation = rotation;
            Translation = translation;
        }

        private Transformation(Transformation transformation)
        {
            Rotation = transformation.Rotation.Clone();
            Translation = transformation.Translation.Clone();
        }

        public Transformation Clone()
        {
            return new Transformation(this);
        }
    }
}
