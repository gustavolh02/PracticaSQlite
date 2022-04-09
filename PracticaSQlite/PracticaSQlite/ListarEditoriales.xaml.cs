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
    public partial class ListarEditoriales : ContentPage
    {
        public ListarEditoriales()
        {
            InitializeComponent();
        }
        async void mostarDatos(object sender, EventArgs e)
        {
            ConsultaEdit datos = await ConsultaEdit.Instance;
            ListaEditoriales.ItemsSource = await datos.GetEditorialesAsync();
        }
        void atras(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}