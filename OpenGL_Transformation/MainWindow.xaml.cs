using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;
using TransformationApplication.Mathematics;
using TransformationApplication.Mathematics.Base;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using OpenTK.Wpf;
using OpenTK.Mathematics;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        private readonly List<double> _initialCameraTransformation = new() { 0.0f, 0.0f, 10.0f, 0.0f, 0.0f, 0.0f };
        private readonly List<double> _initialModelTransformation = new() { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        private readonly IRenderable _leftScene;
        private readonly IRenderable _rightScene;

        private readonly Transformation _modelTransformation;
        private readonly Transformation _cameraTransformation;

        private readonly ObservableCollection<MatrixRow> _modelMatrixGrid;
        private readonly ObservableCollection<MatrixRow> _viewMatrixGrid;
        private readonly ObservableCollection<MatrixRow> _modelViewMatrixGrid;

        private Vector2 _lastMousePosition;
        private bool _firstMove;
        private bool _mouseDown;

        public MainWindow()
        {
            InitializeComponent();
            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true
            };

            _modelTransformation = new(new Rotation(), new Translation());
            _cameraTransformation = new(new Rotation(), new Translation());

            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _leftScene = new LeftScene(_cameraTransformation);
            _rightScene = new RightScene();

            cameraZPosSlider.Value = _initialCameraTransformation[2];

            // ListView Init
            _modelMatrixGrid = new();
            _viewMatrixGrid = new();
            _modelViewMatrixGrid = new();

            int rowsCount = 4;
            for (int i = 0; i < rowsCount; ++i)
            {
                _modelMatrixGrid.Add(new MatrixRow(0, 0, 0, 0));
                _viewMatrixGrid.Add(new MatrixRow(0, 0, 0, 0));
                _modelViewMatrixGrid.Add(new MatrixRow(0, 0, 0, 0));
            }

            ModelMatrix.ItemsSource = _modelMatrixGrid;
            ViewMatrix.ItemsSource = _viewMatrixGrid;
            ViewModelMatrix.ItemsSource = _modelViewMatrixGrid;
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.UpdateAspectRatio((float)LeftGlControl.ActualWidth, (float)LeftGlControl.ActualHeight);
            Matrix4 view = _leftScene.Render(_cameraTransformation.Clone(), _modelTransformation.Clone());
            Matrix4 model = TransformationMatrix.GetTransformationMatrix(_modelTransformation);

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

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.UpdateAspectRatio((float)RightGlControl.ActualWidth, (float)RightGlControl.ActualHeight);
            _ = _rightScene.Render(_cameraTransformation, _modelTransformation);
        }

        private void ModelXRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.Pitch = (float)e.NewValue;
        }

        private void ModelYRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.Yaw = (float)e.NewValue;
        }

        private void ModelZRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.Roll = (float)e.NewValue;
        }

        private void ModelXPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.X = (float)e.NewValue;
        }

        private void ModelYPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.Y = (float)e.NewValue;
        }

        private void ModelZPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.Z = (float)e.NewValue;
        }

        private void CameraXPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Translation.X = (float)e.NewValue;
        }

        private void CameraYPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Translation.Y = (float)e.NewValue;
        }

        private void CameraZPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Translation.Z = (float)e.NewValue;
        }

        private void CameraXRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Rotation.Pitch = (float)e.NewValue;
        }

        private void CameraYRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Rotation.Yaw = (float)e.NewValue;
        }

        private void CameraZRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Rotation.Roll = (float)e.NewValue;
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
            int i = 0;
            foreach(Slider slider in modelSliders.Children)
            {
                slider.Value = _initialModelTransformation[i];
                ++i;
            }
        }

        private void CameraResetClick(object sender, RoutedEventArgs e)
        {
            int i = 0;
            foreach (Slider slider in cameraSliders.Children)
            {
                slider.Value = _initialCameraTransformation[i];
                ++i;
            }
        }
    }
}
