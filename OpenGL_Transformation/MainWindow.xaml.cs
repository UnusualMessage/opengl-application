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
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.UpdateAspectRatio((float)LeftGlControl.ActualWidth, (float)LeftGlControl.ActualHeight);
            Matrix4 view = _leftScene.Render(_cameraTransformation.Clone(), _modelTransformation.Clone());

            ObservableCollection<MatrixRow> modelRecords = new();
            ModelMatrix.ItemsSource = modelRecords;

            TransformationMatrix matrix = new(TransformationMatrix.GetTransformationMatrix(_modelTransformation));

            const int rowsCount = 4;
            for (int i = 0; i < rowsCount; ++i)
            {
                modelRecords.Add(matrix[i]);
            }

            ObservableCollection<MatrixRow> viewRecords = new();
            ViewMatrix.ItemsSource = viewRecords;

            matrix = new(view);

            for (int i = 0; i < rowsCount; ++i)
            {
                viewRecords.Add(matrix[i]);
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
