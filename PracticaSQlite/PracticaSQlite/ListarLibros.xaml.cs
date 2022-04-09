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
    public partial class ListarLibros : ContentPage
    {
        public ListarLibros()
        {
            InitializeComponent();
        }
        async private void mostrarDatos(object sender, EventArgs e)
        {
            ConsultasLibros datos = await ConsultasLibros.Instance;
            ListaLibros.ItemsSource = await datos.GetAllLibrosAsync();
        }
        void atras(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}