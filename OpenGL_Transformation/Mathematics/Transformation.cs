using TransformationApplication.Mathematics.Base;

namespace TransformationApplication.Mathematics
{
    public class Transformation
    {
        public Rotation Rotation { get; }
        public Position Position { get; }

        public Transformation()
        {
            Rotation = new Rotation();
            Position = new Position();
        }

        public Transformation(Rotation rotation, Position translation)
        {
            Rotation = rotation;
            Position = translation;
        }

        private Transformation(Transformation transformation)
        {
            Rotation = transformation.Rotation.Clone();
            Position = transformation.Position.Clone();
        }

        public Transformation Clone()
        {
            return new Transformation(this);
        }

        public void ResetRotation()
        {
            Rotation.Reset();
        }

        public void ResetTranslation()
        {
            Position.Reset();
        }

        public void Reset()
        {
            ResetRotation();
            ResetTranslation();
        }
    }
}
