using System.Windows;

namespace HelloWPF
{
    /// <summary>
    /// Lógica interna para Atividade02.xaml
    /// </summary>
    public partial class Atividade02 : Window
    {

        string usuario = "conradito";
        string senha = "123123";
        public string nome = "Conrado";
        bool contaPoupanca = false;

        bool lembrar = false;

        public Atividade02()
        {
            InitializeComponent();
        }

        private void btnConectar_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string senha = txtSenha.Password;

            if (this.usuario != usuario)
                lblErroUsuario.Visibility = Visibility.Visible;
            //else
            //    lblErroUsuario.Visibility = Visibility.Collapsed;
            if (this.senha != senha)
                lblErroSenha.Visibility = Visibility.Visible;
            //else
            //    lblErroSenha.Visibility = Visibility.Collapsed;

            if (this.usuario == usuario && this.senha == senha)
            {
                panelLogin.Visibility = Visibility.Collapsed;
                panelDashboard.Visibility = Visibility.Visible;
                lblTitulo.Text = "Bem-vindo ao Dashboard";

                if (lembrar == false)
                {
                    txtUsuario.Clear();
                    txtSenha.Clear();
                }

                lblErroUsuario.Visibility = Visibility.Collapsed;
                lblErroSenha.Visibility = Visibility.Collapsed;

            }

        }

        private void cboxLembrese_Click(object sender, RoutedEventArgs e)
        {
            lembrar = cboxLembrese.IsChecked == true;
        }

        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            panelLogin.Visibility = Visibility.Visible;
            panelDashboard.Visibility = Visibility.Collapsed;
            lblTitulo.Text = "Conecte-se para acessar o sistema";
        }

        private void btnAlterarDados_Click(object sender, RoutedEventArgs e)
        {
            btnAlterarDados.Visibility = Visibility.Collapsed;
            panelAlterarDados.Visibility = Visibility.Visible;
        }

        private void btnSalvarAlteracoes_Click(object sender, RoutedEventArgs e)
        {
            this.nome = txtNome.Text;
            this.contaPoupanca = radioPoupanca.IsChecked == true;
            //runNome.Text = nome;
            runConta.Text = contaPoupanca ? "Poupança" : "Corrente";
            fecharAlteracoes();
        }

        private void btnCancelarAlteracoes_Click(object sender, RoutedEventArgs e)
        {
            fecharAlteracoes();
        }

        void fecharAlteracoes()
        {
            panelAlterarDados.Visibility = Visibility.Collapsed;
            btnAlterarDados.Visibility = Visibility.Visible;
            txtNome.Clear();
        }

    }
}
