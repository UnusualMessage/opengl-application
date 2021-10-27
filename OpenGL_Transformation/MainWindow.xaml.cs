using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;
using TransformationApplication.SceneObjects.Model;

using System;
using System.Windows;

using OpenTK.Wpf;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        private readonly Scene _leftScene;
        private readonly Scene _rightScene;

        private readonly Rotation _modelRotation;
        private readonly Translation _modelTranslation;

        private readonly Rotation _cameraRotation;
        private readonly Translation _cameraTranslation;

        public MainWindow()
        {
            InitializeComponent();
            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true
            };

            _modelRotation = new(0, 0, 0);
            _modelTranslation = new(0, 0, 0);

            _cameraRotation = new(0, 0, 0);
            _cameraTranslation = new(0, 0, 0);

            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _leftScene = new LeftScene();
            _rightScene = new RightScene();

            _leftScene.Load();
            _rightScene.Load();
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.Render((int)LeftGlControl.ActualWidth, (int)LeftGlControl.ActualHeight, _modelRotation, _modelTranslation);
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.Render((int)RightGlControl.ActualWidth, (int)RightGlControl.ActualHeight, _modelRotation, _modelTranslation);
        }

        private void RotXValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelRotation.RotationByX = (float)e.NewValue;
        }

        private void RotYValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelRotation.RotationByY = (float)e.NewValue;
        }

        private void RotZValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelRotation.RotationByZ = (float)e.NewValue;
        }

        private void PosXValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTranslation.TranslationByX = (float)e.NewValue;
        }

        private void PosYValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTranslation.TranslationByY = (float)e.NewValue;
        }

        private void PosZValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTranslation.TranslationByZ = (float)e.NewValue;
        }


        private void CameraXPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTranslation.TranslationByX = (float)e.NewValue;
        }

        private void CameraYPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTranslation.TranslationByY = (float)e.NewValue;
        }

        private void CameraZPosChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraTranslation.TranslationByZ = (float)e.NewValue;
        }

        private void CameraXRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraRotation.RotationByX = (float)e.NewValue;
        }

        private void CameraYRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraRotation.RotationByY = (float)e.NewValue;
        }

        private void CameraZRotChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cameraRotation.RotationByZ = (float)e.NewValue;
        }
    }
}
