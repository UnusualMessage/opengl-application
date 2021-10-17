using System;
using System.Windows;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Wpf;

namespace TransformationApplication
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GLWpfControlSettings settings = new()
            {
                MajorVersion = 4,
                MinorVersion = 1,
                RenderContinuously = true
            };

            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);
        }

        private void LeftGlControlOnRender(TimeSpan delta)
        {
            GL.ClearColor(Color4.Black);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            GL.ClearColor(Color4.Red);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
