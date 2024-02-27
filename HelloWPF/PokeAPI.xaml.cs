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
using static System.Net.WebRequestMethods;

namespace HelloWPF
{
    /// <summary>
    /// Lógica interna para PokeAPI.xaml
    /// </summary>
    public partial class PokeAPI : Window
    {

        HttpClient client = new HttpClient();
        string url = "https://pokeapi.co/api/v2/pokemon/pikachu";

        public PokeAPI()
        {
            InitializeComponent();

            //ConsultaSimples();
            //ConsultaComMaisDados();
            //ConsultaComVerificacaoSucesso();
            //ConsultaComTiposDeErro();
            //MostrarItens();
        }

        async void MostrarItens()
        {
            string url_nova = "https://pokeapi.co/api/v2/pokemon?limit=10";
            string url_img = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/versions/generation-v/black-white/animated/";
            HttpResponseMessage resposta = await client.GetAsync(url_nova); // faz a requisição
            string conteudo = await resposta.Content.ReadAsStringAsync(); // pega o conteúdo
            JsonDocument json = JsonDocument.Parse(conteudo); // transforma em json

            List<object> lista = new List<object>();

            JsonElement elementos = json.RootElement.GetProperty("results"); // busca dentro do json
            for (int i = 0; i < elementos.GetArrayLength(); i++)
            {
                lista.Add(new { nome = elementos[i].GetProperty("name").GetString(), imagem = url_img+(i+1)+".gif", botao = "pokemon_"+ elementos[i].GetProperty("name").GetString() });
            }
            //listaDeNomes.ItemsSource = lista;
        }

        async void ConsultaSimples()
        {
            HttpResponseMessage resposta = await client.GetAsync( url ); // faz a requisição
            string conteudo = await resposta.Content.ReadAsStringAsync(); // pega o conteúdo
            JsonDocument json = JsonDocument.Parse(conteudo); // transforma em json
            string nome = json.RootElement.GetProperty("name").GetString(); // busca dentro do json
            Debug.WriteLine(nome);
            client.Dispose(); // fecha o client para nao consumir memória
        }

        async void ConsultaComMaisDados()
        {
            HttpResponseMessage resposta = await client.GetAsync(url);
            string conteudo = await resposta.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(conteudo);
            // JsonElement root = json.RootElement; // possibilidade de redução de código

            string nome = json.RootElement.GetProperty("name").GetString();
            string imagem = json.RootElement.GetProperty("sprites").GetProperty("front_default").GetString();
            string imagem_home = json.RootElement.GetProperty("sprites").GetProperty("other").GetProperty("home").GetProperty("front_default").GetString();
            string poder1 = json.RootElement.GetProperty("abilities")[0].GetProperty("ability").GetProperty("name").GetString();

            Debug.WriteLine($"Este é o Pokemon chamado {nome}. Ele tem a habilidade {poder1}. Link da imagem normal: {imagem} | E Imagem bonita: {imagem_home}");

            client.Dispose();
        }

        async void ConsultaComVerificacaoSucesso()
        {

            string url_errada = "https://pokeapi.co/api/v2/pokemon/pikachuuuuuuu";
            HttpResponseMessage resposta = await client.GetAsync(url_errada);

            // Antes de ler, verificar se deu certo ou não a requisição:
            //if( (int) resposta.StatusCode != 200)
            if (!resposta.IsSuccessStatusCode)
            {
                Debug.WriteLine("Ocorreu um problema na consulta");
                client.Dispose(); // precisa fechar o client também!
                return;
            }

            string conteudo = await resposta.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(conteudo);
            // JsonElement root = json.RootElement; // possibilidade de redução de código

            string nome = json.RootElement.GetProperty("name").GetString();
            string imagem = json.RootElement.GetProperty("sprites").GetProperty("front_default").GetString();
            string imagem_home = json.RootElement.GetProperty("sprites").GetProperty("other").GetProperty("home").GetProperty("front_default").GetString();
            string poder1 = json.RootElement.GetProperty("abilities")[0].GetProperty("ability").GetProperty("name").GetString();

            Debug.WriteLine($"Este é o Pokemon chamado {nome}. Ele tem a habilidade {poder1}. Link da imagem normal: {imagem} | E Imagem bonita: {imagem_home}");

            client.Dispose();
        }

        async void ConsultaComTiposDeErro()
        {

            string url_errada = "https://pokeapi.co/api/v2/pokemon/pikachuuuuuuu";
            HttpResponseMessage resposta = await client.GetAsync(url_errada);

            // Antes de ler, verificar se deu certo ou não a requisição:
            //if( (int) resposta.StatusCode != 200)
            if (!resposta.IsSuccessStatusCode)
            {
                Debug.WriteLine("Ocorreu um problema na consulta");
                Debug.WriteLine(resposta.StatusCode);
                return;
            }

            string conteudo = await resposta.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(conteudo);
            // JsonElement root = json.RootElement; // possibilidade de redução de código

            string nome = json.RootElement.GetProperty("name").GetString();
            string imagem = json.RootElement.GetProperty("sprites").GetProperty("front_default").GetString();
            string imagem_home = json.RootElement.GetProperty("sprites").GetProperty("other").GetProperty("home").GetProperty("front_default").GetString();
            string poder1 = json.RootElement.GetProperty("abilities")[0].GetProperty("ability").GetProperty("name").GetString();

            Debug.WriteLine($"Este é o Pokemon chamado {nome}. Ele tem a habilidade {poder1}. Link da imagem normal: {imagem} | E Imagem bonita: {imagem_home}");

            client.Dispose();
        }

        async void Consultar()
        {

            string pokemon = "pikachus";
            HttpResponseMessage response = await client.GetAsync("https://pokeapi.co/api/v2/pokemon/"+ pokemon);

            if ( (int) response.StatusCode == 200 )
            {
                Debug.WriteLine("AEEEE");
            }
            else
            {
                Debug.WriteLine("deu bosta");
            }

            string resposta = await response.Content.ReadAsStringAsync();
            //Debug.WriteLine( resposta );

            JsonDocument jsonDocument = JsonDocument.Parse(resposta);
            JsonElement abilities = jsonDocument.RootElement.GetProperty("abilities");

            //Debug.WriteLine(abilities[0].GetProperty("ability").GetProperty("nome").GetString() );
            
            //Debug.WriteLine()
            

        }

        private void btnFechar_Click(object sender, RoutedEventArgs e)
        {
            panelPokemon.Visibility = Visibility.Collapsed;
        }

        private async void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            string nova_url = "https://pokeapi.co/api/v2/pokemon/"+txtNome.Text;

            HttpResponseMessage resposta = await client.GetAsync(nova_url);

            if (!resposta.IsSuccessStatusCode)
            {
                MessageBox.Show("Este Pokémon não existe!");
                return;
            }

            string conteudo = await resposta.Content.ReadAsStringAsync();
            JsonDocument json = JsonDocument.Parse(conteudo);

            string nome = json.RootElement.GetProperty("name").GetString();
            string imagem = json.RootElement.GetProperty("sprites").GetProperty("front_default").GetString();
            string imagem_home = json.RootElement.GetProperty("sprites").GetProperty("other").GetProperty("home").GetProperty("front_default").GetString();
            string poder1 = json.RootElement.GetProperty("abilities")[0].GetProperty("ability").GetProperty("name").GetString();

            panelPokemon.Visibility = Visibility.Visible;
            lblNome.Text = nome;
            spanPoder.Text = poder1;

            Debug.WriteLine(imagem);
            BitmapImage source = new BitmapImage();
            source.BeginInit();
            source.UriSource = new Uri(imagem);
            source.EndInit();
            imgPokemon.Source = source;

        }

        private void btnTabPesquisa_Click(object sender, RoutedEventArgs e)
        {
            panelPesquisa.Visibility = Visibility.Visible;
            panelListagem.Visibility = Visibility.Collapsed;
        }

        private void btnTabListagem_Click(object sender, RoutedEventArgs e)
        {
            panelListagem.Visibility = Visibility.Visible;
            panelPesquisa.Visibility = Visibility.Collapsed;
        }
    }
}
