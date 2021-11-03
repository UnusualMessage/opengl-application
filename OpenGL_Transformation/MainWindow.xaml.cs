using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;
using TransformationApplication.Mathematics;
using TransformationApplication.Mathematics.Base;

using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;

using OpenTK.Wpf;
using OpenTK.Mathematics;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        public Transformation ModelTransformation { get; }
        public Transformation CameraTransformation { get; }

        private readonly IRenderable _leftScene;
        private readonly IRenderable _rightScene;

        private readonly ObservableCollection<MatrixRow> _modelMatrixGrid;
        private readonly ObservableCollection<MatrixRow> _viewMatrixGrid;
        private readonly ObservableCollection<MatrixRow> _modelViewMatrixGrid;

        private Vector2 _lastMousePosition;
        private bool _firstMove;
        private bool _mouseDown;

        public MainWindow()
        {
            ModelTransformation = new(new Rotation(), new Translation());
            CameraTransformation = new(new Rotation(), new Translation(0, 0, 10.0f));

            InitializeComponent();

            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true
            };
            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _leftScene = new LeftScene(CameraTransformation);
            _rightScene = new RightScene();

            _modelMatrixGrid = new();
            _viewMatrixGrid = new();
            _modelViewMatrixGrid = new();

            BindMatrices();
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.UpdateAspectRatio((float)LeftGlControl.ActualWidth, (float)LeftGlControl.ActualHeight);
            Matrix4 view = _leftScene.Render(CameraTransformation.Clone(), ModelTransformation.Clone());
            Matrix4 model = TransformationMatrix.GetTransformationMatrix(ModelTransformation);

            UpdateGrid(_modelMatrixGrid, new(model));
            UpdateGrid(_viewMatrixGrid, new(view));
            UpdateGrid(_modelViewMatrixGrid, new(model * view));
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

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.UpdateAspectRatio((float)RightGlControl.ActualWidth, (float)RightGlControl.ActualHeight);
            _ = _rightScene.Render(CameraTransformation, ModelTransformation);
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
            }
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
        }

        private void CameraResetClick(object sender, RoutedEventArgs e)
        {
            CameraTransformation.Reset();
            CameraTransformation.Translation.Z = 10.0f;
        }
    }
}
