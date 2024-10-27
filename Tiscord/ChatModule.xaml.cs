using System.ComponentModel;
using System.Security.Cryptography.Xml;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Tiscord
{
    public partial class ChatModule : UserControl, INotifyPropertyChanged
    {
        private string _title = "Чат";
        private bool _isDragging = false;
        private Point _clickPosition;
        private TranslateTransform _transform = new TranslateTransform();
        private Point _startPosition;

        public ChatModule()
        {
            InitializeComponent();
            this.DataContext = this;
            this.RenderTransform = _transform;
            this.MouseDown += ChatModule_MouseDown;
            this.MouseMove += ChatModule_MouseMove;
            this.MouseUp += ChatModule_MouseUp;
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(MessageTextBox.Text))
            {
                MessagesListBox.Items.Add(MessageTextBox.Text);
                MessageTextBox.Clear();
            }
        }

        private void ChatModule_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                _isDragging = true;
                _startPosition = e.GetPosition(this.Parent as UIElement); // Начальная позиция относительно родителя
                this.CaptureMouse();
            }
        }

        private void ChatModule_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                var currentPosition = e.GetPosition(this.Parent as UIElement);
                _transform.X += currentPosition.X - _startPosition.X; // Изменение только на смещение
                _transform.Y += currentPosition.Y - _startPosition.Y;
                _startPosition = currentPosition; // Обновляем начальную позицию
            }
        }

        private void ChatModule_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isDragging = false;
            this.ReleaseMouseCapture();
        }

        // Логика для изменения размера
        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Width + e.HorizontalChange > MinWidth)
                Width += e.HorizontalChange;
            if (Height + e.VerticalChange > MinHeight)
                Height += e.VerticalChange;
        }
    }
}
