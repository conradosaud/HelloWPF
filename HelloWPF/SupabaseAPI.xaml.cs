using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
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
    /// Lógica interna para SupabaseAPI.xaml
    /// </summary>
    public partial class SupabaseAPI : Window
    {

        string url = "https://pamtotngllapitpiawog.supabase.co/rest/v1/produtos";
        string apikey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6InBhbXRvdG5nbGxhcGl0cGlhd29nIiwicm9sZSI6ImFub24iLCJpYXQiOjE2ODc3ODc5NzksImV4cCI6MjAwMzM2Mzk3OX0.Yv1Cqa1ln3xah6GOTEcIC9MNG-RoOFhCDghs6UUwNnk";


        public SupabaseAPI()
        {
            InitializeComponent();
        }

        public async Task<string> BuscaTodos()
        {

            string url_request = url + "?apikey=" + apikey;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url_request);
            string content = await response.Content.ReadAsStringAsync();
           
            return content;
        }

        async Task<string> BuscaPorNomeouID(string busca)
        {

            string url_request = url;
            try
            {
                int id = int.Parse(busca);
                url_request += $"?id=eq.{id}&apikey={apikey}";
            }
            catch(Exception e)
            {
                url_request += $"?nome=eq.{busca}&apikey={apikey}";
            }

            string content = "";

            HttpClient client = new HttpClient();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url_request);
                content = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex) {
                MessageBox.Show("Erro ao buscar item");
            }

            return content;

        }

        async Task<bool> Inserir(string nome, decimal preco)
        {
            string url_request = url + "?apikey=" + apikey;

            // Construa os dados que você deseja enviar no corpo da solicitação
            // O ideal seria ter uma classe
            var produto = new Dictionary<string, string>
            {
                { "nome", nome },
                { "preco", preco.ToString() }
            };

            // Serializa (transforma) os dados em formato JSON
            var json = JsonSerializer.Serialize(produto);

            // Invés de "response" agora é o "request", enviando um post e o conteúdo do json
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url_request);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");

            // O resto continua igual
            HttpClient client = new HttpClient();
            // Mas invés de ReadAsStringAsync, aqui é SendAsync V
            HttpResponseMessage response = await client.SendAsync(request);

            return response.IsSuccessStatusCode;

        }

        
        private async void BtnPesquisar_Click(object sender, RoutedEventArgs e)
        {

            string pesquisa = txtPesquisa.Text;
            string content = await BuscaPorNomeouID( pesquisa );

            if( content == "[]")
            {
                MessageBox.Show("Item não encontrado!");
                return;
            }

            JsonDocument json = JsonDocument.Parse(content);

            int id = json.RootElement[0].GetProperty("id").GetInt32();
            string nome = json.RootElement[0].GetProperty("nome").ToString();
            decimal preco = json.RootElement[0].GetProperty("preco").GetDecimal();

            lblID.Text = id.ToString();
            lblNome.Text = nome;
            lblPreco.Text = preco.ToString();

        }

        private async void BtnSalvar_Click(object sender, RoutedEventArgs e)
        {
            string nome = txtNome.Text;
            decimal preco = decimal.Parse(txtPreco.Text);
            bool inserido = await Inserir(nome, preco);
            if( inserido)
            {
                MessageBox.Show("Produto inserido com sucesso!");
            }
            else
            {
                MessageBox.Show("Erro ao inserir produto...");
            }
        }

        private void BtnTodos_Click(object sender, RoutedEventArgs e)
        {
            SupabaseAPIVerTodos novaJanela = new SupabaseAPIVerTodos();
            novaJanela.Show();
        }
    }



}
