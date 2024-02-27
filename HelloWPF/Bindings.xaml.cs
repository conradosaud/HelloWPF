using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HelloWPF
{
    /// <summary>
    /// Lógica interna para Bindings.xaml
    /// </summary>
    public partial class Bindings : Window
    {

        public string nome { get; set; }
        public int idade { get; set; }
        public int sexo { get; set; }

        public Bindings()
        {
            InitializeComponent();
            // Passando essa classe como contexto para a tela
            nome = "Conraditoo";
            this.DataContext = this;

            // --- Ligar o txtValor com o lblValor ao iniciar a classe
            // Criar o binding com a propriedade que será ligada
            Binding binding = new Binding("Text");
            // Definir quem será o componente de onde vai vir o valor
            binding.Source = txtValor;
            // Adicionar o binding criado no componente que vai recebê-lo
            lblValor.SetBinding(TextBlock.TextProperty, binding);


        }

        private void btnAtivarBinding_Click(object sender, RoutedEventArgs e)
        {
            // Mesma coisa do código acima
            // porém só cria o binding quando clica neste botão
            Binding binding = new Binding("Text");
            binding.Source = txtValor;
            lblValor.SetBinding(TextBlock.TextProperty, binding);
        }

        private void btnDesligarBinding_Click(object sender, RoutedEventArgs e)
        {
            // --- Também é possível desligar/remover um binding
            // Salva o binding antes de desligar
            string textoDoBinding = txtValor.Text;
            // Remove o binding
            BindingOperations.ClearBinding(lblValor, TextBlock.TextProperty);
            // Retornar o texto salvo
            lblValor.Text = textoDoBinding;
        }


    }
}
