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
    public partial class ListarGeneros : ContentPage
    {
        public ListarGeneros()
        {
            InitializeComponent();
        }
        async void mostarDatos(object sender, EventArgs e)
        {
            ConsultasGen datos = await ConsultasGen.Instance;
            ListaGeneros.ItemsSource = await datos.GetGenerosAsync();
        }
        void atras(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}