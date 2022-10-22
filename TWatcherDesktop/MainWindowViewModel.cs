using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

using TWatcher.Services;

namespace TWatcherDesktop
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _info;

        public string Info
        {
            get => _info;
            set { _info = value; OnPropertyChanged(); }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected internal void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public MainWindowViewModel()
        {
            SystemWatcher watcher = new SystemWatcher();

            watcher.Updated += (t) =>
            {

                Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    Info = t.ToString();
                }));
            };

            watcher.Start();
        }
    }
}
