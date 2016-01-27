using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace w20620
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            SetIsHitTestVisibile(this, false);
        }

        public MainWindow()
        {
            InitializeComponent();

            this.Top = 0;
        }

        private Model Model
        {
            get { return this.DataContext as Model; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.PrimaryScreenWidth - this.ActualWidth / 2;
            Model.IsScreenWatching = !Model.IsScreenWatching;
        }

        public static void SetIsHitTestVisibile(Window window, bool isVisible)
        {
            IntPtr hwnd = new WindowInteropHelper(window).Handle;

            int currentExtendedStyle = Win32.Win32.GetWindowLong(hwnd, GWL_EXSTYLE);
            int newStyle = currentExtendedStyle;

            newStyle = isVisible
                ? newStyle | WS_EX_TRANSPARENT
                : newStyle & ~WS_EX_TRANSPARENT;

            if (newStyle != currentExtendedStyle)
            {
                Win32.Win32.SetWindowLong(hwnd, GWL_EXSTYLE, newStyle);
            }
        }
    }
}