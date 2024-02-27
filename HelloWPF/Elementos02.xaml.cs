using System.Windows;
using System.Windows.Input;

namespace HelloWPF
{
    /// <summary>
    /// Lógica interna para Elementos02.xaml
    /// </summary>
    public partial class Elementos02 : Window
    {
        public Elementos02()
        {
            InitializeComponent();
        }

        private void MostrarMensagem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Olá, WPF");
        }

        private void MostrarMensagem_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Opa, passou o mouse em cima");
        }

        private void SalvarNome_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("O nome é: " + txtName.Text);
        }
    }
}
