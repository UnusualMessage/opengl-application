using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;
using TransformationApplication.SceneObjects;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.Base;

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Generic;

using OpenTK.Wpf;
using OpenTK.Mathematics;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        private readonly List<double> _initialCameraTransformation = new() { 0.0f, 0.0f, 10.0f, 0.0f, 0.0f, 0.0f };
        private readonly List<double> _initialModelTransformation = new() { 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f };

        private readonly Scene _leftScene;
        private readonly Scene _rightScene;

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

            _modelTransformation = new(new Rotation(0, 0, 0), new Translation(0, 0, 0));
            _cameraTransformation = new(new Rotation(0, 0, 0), new Translation(0, 0, 0));

            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _leftScene = new LeftScene();
            _rightScene = new RightScene();

            cameraZPosSlider.Value = _initialCameraTransformation[2];
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.UpdateAspectRatio((float)LeftGlControl.ActualWidth, (float)LeftGlControl.ActualHeight);
            _leftScene.Render(_cameraTransformation, _modelTransformation);
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.UpdateAspectRatio((float)RightGlControl.ActualWidth, (float)RightGlControl.ActualHeight);
            _rightScene.Render(_cameraTransformation, _modelTransformation);
        }

        private void ModelXRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.RotationByX = (float)e.NewValue;
        }

        private void ModelYRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.RotationByY = (float)e.NewValue;
        }

        private void ModelZRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.RotationByZ = (float)e.NewValue;
        }

        private void ModelXPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.TranslationByX = (float)e.NewValue;
        }

        private void ModelYPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.TranslationByY = (float)e.NewValue;
        }

        private void ModelZPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.TranslationByZ = (float)e.NewValue;
        }

        private void CameraXPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Translation.TranslationByX = (float)e.NewValue;
        }

        private void CameraYPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Translation.TranslationByY = (float)e.NewValue;
        }

        private void CameraZPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Translation.TranslationByZ = (float)e.NewValue;
        }

        private void CameraXRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Rotation.RotationByX = (float)e.NewValue;
        }

        private void CameraYRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Rotation.RotationByY = (float)e.NewValue;
        }

        private void CameraZRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTransformation.Rotation.RotationByZ = (float)e.NewValue;
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
