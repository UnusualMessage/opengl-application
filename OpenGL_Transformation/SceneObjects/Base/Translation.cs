using OpenTK.Mathematics;

namespace TransformationApplication.SceneObjects.Base
{
    public class Translation
    {
        private const float MinBorder = -10.0f;
        private const float MaxBorder = 10.0f;

        private float _translationByX;
        private float _translationByY;
        private float _translationByZ;

        public float TranslationByX
        {
            get => _translationByX;
            set => _translationByX = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float TranslationByY
        {
            get => _translationByY;
            set => _translationByY = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public float TranslationByZ
        {
            get => _translationByZ;
            set => _translationByZ = MathHelper.Clamp(value, MinBorder, MaxBorder);
        }

        public Translation(float x, float y, float z)
        {
            _translationByX = x;
            _translationByY = y;
            _translationByZ = z;
        }
    }
}
