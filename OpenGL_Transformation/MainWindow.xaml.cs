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

        public MainWindow()
        {
            InitializeComponent();
            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true
            };

            _leftScene = new LeftScene();
            _rightScene = new RightScene();

            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _leftScene.Load();
            _rightScene.Load();
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            _leftScene.Render();
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.Render();
        }
    }
}
