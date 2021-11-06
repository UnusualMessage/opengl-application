using TransformationApplication.Scenes.Base;
using TransformationApplication.Scenes;
using TransformationApplication.Mathematics;
using TransformationApplication.Mathematics.Base;
using TransformationApplication.SceneObjects.Base;
using TransformationApplication.SceneObjects;
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
        private const string VertexShaderPath = "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\VertexShader.vert";
        private const string FragmentShaderPath = "C:\\dev\\TermWork\\OpenGL_Transformation\\OpenGL_Transformation\\Shaders\\FragmentShader.frag";
        private const float DefaultModelY = 1.0f;
        private const float DefaultCameraZ = 10.0f;

        public Transformation ModelTransformation { get; }
        public Transformation CameraTransformation { get; }

        private readonly List<VisibleObject> _visibleObjects = new();

        private readonly IRenderable _leftScene;
        private readonly IRenderable _rightScene;

        private readonly ObservableCollection<MatrixRow> _modelMatrixGrid;
        private readonly ObservableCollection<MatrixRow> _viewMatrixGrid;
        private readonly ObservableCollection<MatrixRow> _modelViewMatrixGrid;

        private readonly ViewCamera _userCamera;
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
                RenderContinuously = true
            };
            LeftGlControl.Start(settings);
            RightGlControl.Start(settings);

            _visibleObjects.Add(new VisibleObject(new Shader(VertexShaderPath, FragmentShaderPath), Vertices.GetParallelepiped(1.0f, 1.0f, 1.0f)));
            _visibleObjects.Add(new VisibleObject(new Shader(VertexShaderPath, FragmentShaderPath), Vertices.GetParallelepiped(0.5f, 0.3f, 0.2f)));
            _visibleObjects.Add(new Field(new Shader(VertexShaderPath, FragmentShaderPath), Vertices.FieldLine));

            _userCamera = new();

            _leftScene = new LeftScene(_visibleObjects);
            _rightScene = new RightScene(_userCamera, _visibleObjects);

            _modelMatrixGrid = new();
            _viewMatrixGrid = new();
            _modelViewMatrixGrid = new();

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
            _leftScene.UpdateAspectRatio((float)LeftGlControl.ActualWidth, (float)LeftGlControl.ActualHeight);
            _leftScene.Render(CameraTransformation, ModelTransformation, out Matrix4 view);
            Matrix4 model = TransformationMatrix.GetTransformationMatrix(ModelTransformation);

            UpdateGrid(_modelMatrixGrid, new(model));
            UpdateGrid(_viewMatrixGrid, new(view));
            UpdateGrid(_modelViewMatrixGrid, new(model * view));
        }

        private void RightGlControlOnRender(TimeSpan delta)
        {
            _rightScene.UpdateAspectRatio((float)RightGlControl.ActualWidth, (float)RightGlControl.ActualHeight);
            _rightScene.Render(CameraTransformation, ModelTransformation, out _);
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

                _userCamera.Yaw += xDelta * 0.2f;
                _userCamera.Pitch += yDelta * 0.2f;
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
            ModelTransformation.Position.Y = DefaultModelY;
        }

        private void CameraResetClick(object sender, RoutedEventArgs e)
        {
            CameraTransformation.Reset();
            CameraTransformation.Position.Z = DefaultCameraZ;
        }
    }
}
