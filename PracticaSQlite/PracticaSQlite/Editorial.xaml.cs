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
    public partial class Editorial : ContentPage
    {
        Editoriales editorial;
        public Editorial()
        {
            InitializeComponent();
            vacia();
        }
        private void vacia()
        {
            txtNombre.Text = "";
            guarda.Text = "Registrar";
        }
        async void guardar(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text) )
            {
                if (guarda.Text == "Registrar")
                {
                    editorial = new Editoriales();
                }
                editorial.nombre_editorial = txtNombre.Text;
                ConsultaEdit datos = await ConsultaEdit.Instance;
                await datos.SaveEditorialAsync(editorial);
                vacia();
            }
            else
                await DisplayAlert("Registrando...", "Faltan datos", "OK");
        }
        void mostar1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListarEditoriales());
        }
        async void buscar(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                ConsultaEdit datos = await ConsultaEdit.Instance;

                editorial = null;
                editorial = await datos.GetEditorialAsync(txtNombre.Text);
                if (editorial != null)
                {
                    txtNombre.Text = editorial.nombre_editorial;
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