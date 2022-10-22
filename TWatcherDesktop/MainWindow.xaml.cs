using System.Windows;

namespace TWatcherDesktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Minimize(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;

        private void Exit(object sender, RoutedEventArgs e) => this.Close();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;
        }
    }
}
