using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// Lógica interna para SupabaseAPIVerTodos.xaml
    /// </summary>
    public partial class SupabaseAPIVerTodos : Window
    {
        public SupabaseAPIVerTodos()
        {
            InitializeComponent();
            MostrarItens();
        }

        async void MostrarItens()
        {

           
            SupabaseAPI supabaseAPI = new SupabaseAPI();
            string content = await supabaseAPI.BuscaTodos();
            

            JsonDocument json = JsonDocument.Parse(content);
            List<object> itens = new List<object>();
            for (int i = 0; i < json.RootElement.GetArrayLength(); i++)
            {
                object item = new {
                    lblID = json.RootElement[i].GetProperty("id"),
                    lblNome = json.RootElement[i].GetProperty("nome"),
                    lblPreco = json.RootElement[i].GetProperty("preco"),
                };
                itens.Add(item);
            }

            listagemProdutos.ItemsSource = itens;
        }

    }
}
