namespace TransformationApplication.SceneObjects.Base
{
    public class Translation
    {
        public float TranslationByX { get; set; }
        public float TranslationByY { get; set; }
        public float TranslationByZ { get; set; }

        public Translation(float x, float y, float z)
        {
            TranslationByX = x;
            TranslationByY = y;
            TranslationByZ = z;
        }
    }
}
