using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using PracticaSQlite.clases;

namespace PracticaSQlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Master : ContentPage
    {
        public Master()
        {
            InitializeComponent();
        }

        private async void autorClic(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Autor());
        }
        private async void editorialClic(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Editorial());
        }
        private async void generoClic(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Generos());
        }
        private async void libroClic(object sender, EventArgs e)
        {
            App.MasterDet.IsPresented = false;
            await App.MasterDet.Detail.Navigation.PushAsync(new Libro());
        }

    }
}