using System.Windows;

namespace HelloWPF
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

        private void MostraMensagem(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Aoba");
        }

        void OtraMessage(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("tapoura");
        }

    }
}
