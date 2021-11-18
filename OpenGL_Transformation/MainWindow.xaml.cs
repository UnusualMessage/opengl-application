using TransformationApplication.Scenes;
using TransformationApplication.Mathematics;
using TransformationApplication.Mathematics.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.SceneObjects;
using TransformationApplication.SceneObjects.CompoundObjects;
using TransformationApplication.Base;

using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using OpenTK.Wpf;
using OpenTK.Mathematics;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        private const float DefaultModelY = 1.0f;
        private const float DefaultCameraZ = 10.0f;

        public Transformation ModelTransformation { get; }
        public Transformation CameraTransformation { get; }

        private readonly List<IVisible> _leftSceneVisibleObjects = new();
        private readonly List<IVisible> _rightScenevisibleObjects = new();

        private readonly LeftScene _leftScene;
        private readonly RightScene _rightScene;

        private readonly ObservableCollection<MatrixRow> _modelMatrixGrid = new();
        private readonly ObservableCollection<MatrixRow> _viewMatrixGrid = new();
        private readonly ObservableCollection<MatrixRow> _modelViewMatrixGrid = new();

        private readonly ViewCamera _rightSceneCamera = new(60.0f);
        private Vector2 _lastMousePosition;
        private bool _firstMove = true;
        private bool _mouseDown = false;

        public MainWindow()
        {
            ModelTransformation = new(new Rotation(), new Position(0, DefaultModelY, 0));
            CameraTransformation = new(new Rotation(), new Position(0, 0, DefaultCameraZ));

            InitializeComponent();

            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true,
            };
            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            string vertexShaderSource = Properties.Resources.VertexShader;
            string fragmentShaderSource = Properties.Resources.FragmentShader;

            Shader common = new(vertexShaderSource, fragmentShaderSource);
            IVisible field = new Field(common, Vertices.FieldLine);
            IVisible model = new SimpleObject(common, Vertices.GetParallelepiped(1.0f, 1.0f, 1.0f));
            IVisible camera = new SimpleObject(common, Vertices.GetParallelepiped(0.5f, 0.4f, 0.15f));
            IVisible modelAxis = new Axis(common, 3.0f);
            IVisible cameraAxis = new Axis(common, 1.0f);
            IVisible cameraFrustum = new Frustum(common);

            _leftSceneVisibleObjects.Add(field);
            _leftSceneVisibleObjects.Add(model);
            _leftSceneVisibleObjects.Add(modelAxis);

            _rightScenevisibleObjects.Add(field);
            _rightScenevisibleObjects.Add(camera);
            _rightScenevisibleObjects.Add(model);
            _rightScenevisibleObjects.Add(modelAxis);
            _rightScenevisibleObjects.Add(cameraAxis);
            _rightScenevisibleObjects.Add(cameraFrustum);

            _rightSceneCamera.Pitch = 45.0f;
            _rightSceneCamera.Z = 20.0f;

            _leftScene = new LeftScene(_leftSceneVisibleObjects);
            _rightScene = new RightScene(_rightSceneCamera, _rightScenevisibleObjects);

            BindMatrices();
        }

        private void BindMatrices()
        {
            int rowsCount = 4;
            for (int i = 0; i < rowsCount; ++i)
            {
                _modelMatrixGrid.Add(new MatrixRow());
                _viewMatrixGrid.Add(new MatrixRow());
                _modelViewMatrixGrid.Add(new MatrixRow());
            }

            modelMatrix.ItemsSource = _modelMatrixGrid;
            viewMatrix.ItemsSource = _viewMatrixGrid;
            modelViewMatrix.ItemsSource = _modelViewMatrixGrid;
        }

        private static void UpdateGrid(ObservableCollection<MatrixRow> grid, TransformationMatrix matrix)
        {
            int rowsCount = 4;
            for (int i = 0; i < rowsCount; ++i)
            {
                grid[i].First = matrix[i].First;
                grid[i].Second = matrix[i].Second;
                grid[i].Third = matrix[i].Third;
                grid[i].Fourth = matrix[i].Fourth;
            }
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.AspectRatio = (float)LeftGlControl.ActualWidth / (float)LeftGlControl.ActualHeight;
            _leftScene.Render(CameraTransformation, ModelTransformation, out Matrix4 view);
            Matrix4 model = TransformationMatrix.GetTransformationMatrix(ModelTransformation);

            UpdateGrid(_modelMatrixGrid, new(model));
            UpdateGrid(_viewMatrixGrid, new(view));
            UpdateGrid(_modelViewMatrixGrid, new(model * view));
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.AspectRatio = (float)RightGlControl.ActualWidth / (float)RightGlControl.ActualHeight;
            _rightScene.Render(CameraTransformation, ModelTransformation);
        }

        private void RightGlControlMouseMove(object sender, MouseEventArgs e)
        {
            if (!_mouseDown)
            {
                return;
            }

            Point point = e.GetPosition(RightGlControl);
            float mouseX = (float)point.X;
            float mouseY = (float)point.Y;

            if (_firstMove)
            {
                _lastMousePosition = new Vector2(mouseX, mouseY);
                _firstMove = false;
            }
            else
            {
                float xDelta = mouseX - _lastMousePosition.X;
                float yDelta = mouseY - _lastMousePosition.Y;

                _lastMousePosition = new Vector2(mouseX, mouseY);

                float sensitivity = 0.3f;
                _rightSceneCamera.Yaw += xDelta * sensitivity;
                _rightSceneCamera.Pitch += yDelta * sensitivity;
            }
        }

        private void RightGlControlMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void RightGlControlMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mouseDown = e.LeftButton == MouseButtonState.Pressed;
            _firstMove = true;
        }

        private void RightGlControlMouseUp(object sender, MouseButtonEventArgs e)
        {
            _mouseDown = e.LeftButton == MouseButtonState.Pressed;
            _firstMove = true;
        }

        private void RightGlControlMouseLeave(object sender, MouseEventArgs e)
        {
            _firstMove = true;
            _mouseDown = false;
        }

        private void ModelResetClick(object sender, RoutedEventArgs e)
        {
            ModelTransformation.Reset();
            ModelTransformation.Position.Y = DefaultModelY;
        }

        private void CameraResetClick(object sender, RoutedEventArgs e)
        {
            CameraTransformation.Reset();
            CameraTransformation.Position.Z = DefaultCameraZ;
        }
    }
}
