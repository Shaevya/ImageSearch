using ImageSearchServer.MVVM.ViewModel;
using Serilog;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Input;


namespace ImageSearchServer
{
    public partial class MainWindow : Window
    {
        private readonly IMainViewModel _mainViewModel;
        private readonly ILogger _logger;

        public MainWindow(IMainViewModel mainViewModel, ILogger logger)
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            _mainViewModel = mainViewModel;
            this.DataContext = mainViewModel;
            _logger = logger;
            _logger.Information("App Started. info");
        }


        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) { 
                DragMove();
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private void ShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void SeachButton_Click(object sender, RoutedEventArgs e)
        {
            _mainViewModel.SearchImages();
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            var tb = sender as TextBox;
            if (e.Key == Key.Enter && tb != null) {
                ImageWrapperScrollBar.ScrollToTop();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                {
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                }
                else
                {
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
                }

            }
        }

        private void ImagesScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollbar = sender as ScrollViewer;

            if (e.ExtentHeight != 0 && e.VerticalOffset + e.ViewportHeight >= e.ExtentHeight - 50) {
                _mainViewModel.GetNextImages();
            }
        }

        private void SearchBarTB_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBarTB.Width = 400;
        }

        private void SearchBarTB_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchBarTB.Width = 300;
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            var processInfo = new ProcessStartInfo(e.Uri.AbsoluteUri);
            processInfo.UseShellExecute = true;
            Process.Start(processInfo);
            e.Handled = true;
        }
    }
}
