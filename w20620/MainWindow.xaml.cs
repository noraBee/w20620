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

		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

		protected override void OnSourceInitialized(EventArgs e)
		{
			base.OnSourceInitialized(e);

			// Get this window's handle
			IntPtr hwnd = new WindowInteropHelper(this).Handle;

			// Change the extended window style to include WS_EX_TRANSPARENT
			int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
			SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
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
	}
}