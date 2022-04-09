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
    public partial class Generos : ContentPage
    {
        Genero genero;
        public Generos()
        {
            InitializeComponent();
            vacia();
        }
        private void vacia()
        {
            txtGenero.Text = "";
            guarda.Text = "Registrar";
        }
        async void guardar(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGenero.Text))
            {
                if (guarda.Text == "Registrar")
                {
                    genero = new Genero();
                }
                genero.tipo = txtGenero.Text;
                ConsultasGen datos = await ConsultasGen.Instance;
                await datos.SaveGeneroAsync(genero);
                vacia();
            }
            else
                await DisplayAlert("Registrando...", "Faltan datos", "OK");
        }
        void mostar2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListarGeneros());
        }
        async void buscar(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtGenero.Text))
            {
                ConsultasGen datos = await ConsultasGen.Instance;

                genero = null;
                genero = await datos.GetGeneroAsync(txtGenero.Text);
                if (genero != null)
                {
                    txtGenero.Text = genero.tipo;
                    guarda.Text = "Actualizar";
                }
                else
                {
                    // mensaje en caso de no encontrar el nombre
                    await DisplayAlert("Resultado", "Nombre No encontrado", "OK");
                }
            }
        }
        private void limpiar(object sender, EventArgs e)
        {
            vacia();
        }
    }
}