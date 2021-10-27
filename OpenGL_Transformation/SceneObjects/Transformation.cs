using TransformationApplication.SceneObjects.Base;

namespace TransformationApplication.SceneObjects
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
