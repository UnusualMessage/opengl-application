using TransformationApplication.Mathematics.Base;

using System.Collections.Generic;

using OpenTK.Mathematics;

namespace TransformationApplication.Mathematics
{
    public class TransformationMatrix
    {
        private readonly List<MatrixRow> _rows = new();

        public MatrixRow this[int index] => _rows[index];

        public TransformationMatrix(Matrix4 matrix)
        {
            AddRow(matrix.Column0);
            AddRow(matrix.Column1);
            AddRow(matrix.Column2);
            AddRow(matrix.Column3);
        }

        public static Matrix4 GetTransformationMatrix(Transformation transformation)
        {
            float pitch = MathHelper.DegreesToRadians(transformation.Rotation.Pitch);
            float yaw = MathHelper.DegreesToRadians(transformation.Rotation.Yaw);
            float roll = MathHelper.DegreesToRadians(transformation.Rotation.Roll);

            Vector3 translation = new(transformation.Position.X,
                transformation.Position.Y,
                transformation.Position.Z);

            Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationX(pitch);
            model *= Matrix4.CreateRotationY(yaw);
            model *= Matrix4.CreateRotationZ(roll);
            model *= Matrix4.CreateTranslation(translation);

            return model;
        }

        private void AddRow(Vector4 row)
        {
            _rows.Add(new(row.X, row.Y, row.Z, row.W));
        }
    }
}
