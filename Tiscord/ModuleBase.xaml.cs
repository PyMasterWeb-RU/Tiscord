using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Tiscord
{
    public partial class ModuleBase : UserControl, INotifyPropertyChanged
    {
        private bool isDragging = false;
        private Point clickPosition;
        private string _title;
        private Brush _moduleColor;

        public ModuleBase()
        {
            InitializeComponent();
            this.DataContext = this;
            Title = "Tiscord";
            ModuleColor = Brushes.LightGray;

            this.MouseDown += ModuleBase_MouseDown;
            this.MouseMove += ModuleBase_MouseMove;
            this.MouseUp += ModuleBase_MouseUp;
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public Brush ModuleColor
        {
            get => _moduleColor;
            set
            {
                _moduleColor = value;
                OnPropertyChanged(nameof(ModuleColor));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ModuleBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickPosition = e.GetPosition(this);
            this.CaptureMouse();
        }

        private void ModuleBase_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                var currentPosition = e.GetPosition(Parent as UIElement);
                var transform = this.RenderTransform as TranslateTransform ?? new TranslateTransform();
                transform.X = currentPosition.X - clickPosition.X;
                transform.Y = currentPosition.Y - clickPosition.Y;
                this.RenderTransform = transform;
            }
        }

        private void ModuleBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            this.ReleaseMouseCapture();
        }
    }
}
