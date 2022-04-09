using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PracticaSQlite.clases;

namespace PracticaSQlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListarAutores : ContentPage
    {
        public ListarAutores()
        {
            InitializeComponent();
        }
        async void mostarDatos(object sender, EventArgs e)
        {
            ConsultasAutor datos = await ConsultasAutor.Instance;
            ListaAutores.ItemsSource = await datos.GetAutoresAsync();
        }
        void atras(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}