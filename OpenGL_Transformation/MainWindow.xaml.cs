using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;

using System;
using System.Windows;

using OpenTK.Wpf;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        private readonly Scene _leftScene;
        private readonly Scene _rightScene;

        private float _xRotation;
        private float _yRotation;
        private float _zRotation;

        public MainWindow()
        {
            InitializeComponent();
            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true
            };

            _xRotation = (float)xRotSlider.Value;
            _yRotation = (float)yRotSlider.Value;
            _zRotation = (float)zRotSlider.Value;

            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _leftScene = new LeftScene();
            _rightScene = new RightScene();

            _leftScene.Load();
            _rightScene.Load();
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.Render((int)LeftGlControl.ActualWidth, (int)LeftGlControl.ActualHeight, _xRotation, _yRotation, _zRotation);
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.Render((int)RightGlControl.ActualWidth, (int)RightGlControl.ActualHeight, _xRotation, _yRotation, _zRotation);
        }

        private void RotXValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _xRotation = (float)xRotSlider.Value;
        }

        private void RotYValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _yRotation = (float)yRotSlider.Value;
        }

        private void RotZValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _zRotation = (float)zRotSlider.Value;
        }
    }
}
