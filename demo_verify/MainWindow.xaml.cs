using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;

namespace demo_verify
{
    public sealed partial class MainWindow : Window
    {
        private DispatcherTimer _timer;
        private ScreeenCaptureHelper _captureHelper;
        private CommandExecutor _commandExecutor;
        private bool _isListening = false;

        public MainWindow()
        {
            this.InitializeComponent();
            _captureHelper = new ScreeenCaptureHelper();
            _commandExecutor = new CommandExecutor();
            this.Closed += MainWindow_Closed;
        }
        private void MainWindow_Closed(object sender, WindowEventArgs e)
        {
            GlobalHookHelper.Stop();
        }
        private void StartRecordingEvents_Click(object sender,RoutedEventArgs e)
        {
            if (_isListening==false)
            {
                GlobalHookHelper.Start();
                StartStopListeningButton.Content = "Stop Recording Events";
            }
            else
            {
                GlobalHookHelper.Stop();
                StartStopListeningButton.Content = "Start Recording Events";
            }
            _isListening = !_isListening;
        }
        private void StartScreenCapture_Click(object sender,RoutedEventArgs e)
        {
            StartCapture();
        }
        private void StartCapture()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += async (s, e) => await CaptureScreen();
            _timer.Start();
        }
        private async Task CaptureScreen()
        {
            await _captureHelper.CaptureScreenAsync();
        }
        private async void ExecuteCommandsFromCSV_Click(object sender,RoutedEventArgs e)
        {
            var filePath = @"D:\Recoder\key_mouse_events.csv";
            await _commandExecutor.ExecuteCommandsFromCSV(filePath);
        }
    }
}
