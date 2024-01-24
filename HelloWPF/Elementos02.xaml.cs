using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            MessageBox.Show("O nome é: "+txtName.Text);
        }
    }
}
