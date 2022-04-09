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
    public partial class Autor : ContentPage
    {
        Autores infoautor;
        public Autor()
        {
            InitializeComponent();
            vacia();
            infoautor = null;
        }
        private void vacia()
        {
            txtAp.Text = "";
            txtAm.Text = "";
            txtNom.Text = "";
            txtNacionalidad.Text = "";
            guarda.Text = "Registrar";
        }
        async void guardar(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtNom.Text) && !String.IsNullOrWhiteSpace(txtAp.Text) && !String.IsNullOrWhiteSpace(txtNacionalidad.Text))
            {
                if (guarda.Text == "Registrar")
                {
                    infoautor = new Autores();
                }
                infoautor.nombre_autor = txtNom.Text;
                infoautor.apellido_pat = txtAp.Text;
                infoautor.apellido_mat = txtAm.Text;
                infoautor.nacionalidad = txtNacionalidad.Text;
                // invocar a la base de datos
                ConsultasAutor datos = await ConsultasAutor.Instance;
                //enviar los datos del autor a la base de datos
                await datos.SaveAutorAsync(infoautor);
                vacia();
            }
            else
                await DisplayAlert("Registrando...", "Faltan datos", "OK");
        }
        void mostrar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListarAutores());
        }
        async void buscar(object sender, EventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(txtNom.Text))
            {
                //invocar a la base de datos
                ConsultasAutor datos = await ConsultasAutor.Instance;
                //solicitar los datos del autor a la base de datos
                infoautor = null;
                infoautor = await datos.GetAutorAsync(txtNom.Text);
                if (infoautor != null)
                {
                    txtAp.Text = infoautor.apellido_pat;
                    txtAm.Text = infoautor.apellido_mat;
                    txtNacionalidad.Text = infoautor.nacionalidad;
                    guarda.Text = "Actualizar";
                }
                else
                {
                    //mensaje en caso de no encontrar el nombre
                    await DisplayAlert("Resultado", "Nombre No encontrado", "OK");
                }
            }
        }
        private void limpiar(Object sender, EventArgs e)
        {
            vacia();
        }
    }
}