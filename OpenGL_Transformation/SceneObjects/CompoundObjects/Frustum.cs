using TransformationApplication.Base;
using TransformationApplication.SceneObjects.Base;

using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

namespace TransformationApplication.SceneObjects.CompoundObjects
{
    public class Frustum : SceneComponent, IVisible
    {
        struct Planes
        {
            public float FarWidth { get; }
            public float FarHeight { get; }
            public float NearWidth { get; }
            public float NearHeight { get; }

            public Planes(float farWidth, float farHeight, float nearWidth, float nearHeight)
            {
                FarWidth = farWidth;
                FarHeight = farHeight;
                NearWidth = nearWidth;
                NearHeight = nearHeight;
            }
        }

        private readonly Color4 planeColor = new(0.3f, 0.3f, 0.3f, 0.5f);
        private readonly Color4 planeBorderColor = new(0.3f, 0.3f, 0.3f, 1.0f);

        private readonly SimpleObject _nearPlane;
        private readonly SimpleObject _farPlane;

        private readonly SimpleObject _leftLine;
        private readonly SimpleObject _rightLine;

        private readonly SimpleObject _nearBorder;
        private readonly SimpleObject _farBorder;

        public float Far { get; set; } = 10.1f;
        public float Near { get; set; } = 1.0f;
        public float Fov { get; set; } = 45.0f;
        public float AspectRatio { get; set; } = 1.55f;

        public Frustum(Shader shader)
        {
            _nearPlane = new(shader, GetNearPlane());
            _farPlane = new(shader, GetFarPlane());
            _leftLine = new(shader, GetLeftLine());
            _rightLine = new(shader, GetRightLine());
            _nearBorder = new(shader, GetNearPlaneBorder());
            _farBorder = new(shader, GetFarPlaneBorder());

            _nearPlane.Color = planeColor;
            _farPlane.Color = planeColor;

            _leftLine.Color = planeBorderColor;
            _leftLine.DrawingMode = PrimitiveType.Lines;

            _rightLine.Color = planeBorderColor;
            _rightLine.DrawingMode = PrimitiveType.Lines;

            _nearBorder.Color = planeBorderColor;
            _nearBorder.DrawingMode = PrimitiveType.LineLoop;

            _farBorder.Color = planeBorderColor;
            _farBorder.DrawingMode = PrimitiveType.LineLoop;
        }

        public void Draw(Matrix4 model, Matrix4 view, Matrix4 projection)
        {
            _leftLine.Draw(model, view, projection);
            _leftLine.Draw(Matrix4.CreateRotationX(-MathHelper.PiOver4) * model, view, projection);

            _rightLine.Draw(model, view, projection);
            _rightLine.Draw(Matrix4.CreateRotationX(-MathHelper.PiOver4) * model, view, projection);

            _nearBorder.Draw(model, view, projection);
            _farBorder.Draw(model, view, projection);

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            _nearPlane.Draw(Matrix4.CreateTranslation(new(0.0f, 0.0f, -Near)) * model, view, projection);
            _farPlane.Draw(Matrix4.CreateTranslation(new(0.0f, 0.0f, -Far)) * model, view, projection);

            GL.Disable(EnableCap.Blend);
        }

        private Planes GetFrustumAttributes()
        {
            float tangent = (float)MathHelper.Tan(MathHelper.DegreesToRadians(Fov / 2));
            float nearHeight = Near * tangent;
            float nearWidth = nearHeight * AspectRatio;

            float farHeight = Far / Near * nearHeight;
            float farWidth = Far / Near * nearWidth;

            return new Planes(farWidth, farHeight, nearWidth, nearHeight);
        }

        private float[] GetNearPlane()
        {
            Planes planes = GetFrustumAttributes();
            return GetPlane(planes.NearWidth, planes.NearHeight);
        }

        private float[] GetFarPlane()
        {
            Planes planes = GetFrustumAttributes();
            return GetPlane(planes.FarWidth, planes.FarHeight);
        }

        private static float[] GetPlane(float width, float height)
        {
            float[] template =
            {
                -width, -height, 0,
                 width, -height, 0,
                 width,  height, 0,
                 width,  height, 0,
                -width,  height, 0,
                -width, -height, 0,
            };
            return template;
        }

        private float[] GetLeftLine()
        {
            Planes planes = GetFrustumAttributes();

            float[] template =
{
                 0,               0,                 0,
                -planes.FarWidth, planes.FarHeight, -Far,
            };
            return template;
        }

        private float[] GetRightLine()
        {
            Planes planes = GetFrustumAttributes();

            float[] template =
            {
                 0,               0,                 0,
                 planes.FarWidth, planes.FarHeight, -Far,
            };
            return template;
        }

        private float[] GetNearPlaneBorder()
        {
            Planes planes = GetFrustumAttributes();

            float[] template =
            {
                 -planes.NearWidth, -planes.NearHeight, -Near,
                  planes.NearWidth, -planes.NearHeight, -Near,
                  planes.NearWidth,  planes.NearHeight, -Near,
                 -planes.NearWidth,  planes.NearHeight, -Near,
                 -planes.NearWidth, -planes.NearHeight, -Near,
            };
            return template;
        }

        private float[] GetFarPlaneBorder()
        {
            Planes planes = GetFrustumAttributes();

            float[] template =
            {
                 -planes.FarWidth, -planes.FarHeight, -Far,
                  planes.FarWidth, -planes.FarHeight, -Far,
                  planes.FarWidth,  planes.FarHeight, -Far,
                 -planes.FarWidth,  planes.FarHeight, -Far,
                 -planes.FarWidth, -planes.FarHeight, -Far,
            };
            return template;
        }
    }
}
