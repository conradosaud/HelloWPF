using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
    /// Lógica interna para AtividadeAPI01.xaml
    /// </summary>
    public partial class AtividadeAPI01 : Window
    {
        public AtividadeAPI01()
        {
            InitializeComponent();
        }

        private async void btnProcurar_Click(object sender, RoutedEventArgs e)
        {
            string pesquisa = txtNome.Text;
            string url = "https://dog.ceo/api/breed/"+pesquisa+"/images";

            HttpClient client = new HttpClient();
            HttpResponseMessage resposta = await client.GetAsync(url);

            if( ! resposta.IsSuccessStatusCode)
            {
                MessageBox.Show("Esta raça não existe!");
                return;
            }

            string conteudo = await resposta.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(conteudo);

            string link1 = json.RootElement.GetProperty("message")[1].GetString();
            string link2 = json.RootElement.GetProperty("message")[2].GetString();
            string link3 = json.RootElement.GetProperty("message")[3].GetString();

            imgCachorro1.Source = new BitmapImage(new Uri(link1));
            imgCachorro2.Source = new BitmapImage(new Uri(link2));
            imgCachorro3.Source = new BitmapImage(new Uri(link3));

        }
    }
}
