using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;
using TransformationApplication.SceneObjects;
using TransformationApplication.SceneObjects.Base;

using System;
using System.Windows;

using OpenTK.Wpf;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        private readonly Scene _leftScene;
        private readonly Scene _rightScene;

        private readonly Transformation _modelTransformation;
        private readonly Transformation _cameraTransformation;

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

            _leftScene = new LeftScene((float)LeftGlControl.Width, (float)LeftGlControl.Height);
            _rightScene = new RightScene((float)LeftGlControl.Width, (float)LeftGlControl.Height);

            _leftScene.Load();
            _rightScene.Load();
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

        private void RotXValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.RotationByX = (float)e.NewValue;
        }

        private void RotYValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.RotationByY = (float)e.NewValue;
        }

        private void RotZValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Rotation.RotationByZ = (float)e.NewValue;
        }

        private void PosXValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.TranslationByX = (float)e.NewValue;
        }

        private void PosYValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _modelTransformation.Translation.TranslationByY = (float)e.NewValue;
        }

        private void PosZValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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
    }
}
