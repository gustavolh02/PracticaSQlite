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
    public partial class Libro : ContentPage
    {
        Libros libro;
        public Libro()
        {
            InitializeComponent();
            vacia();
            llenarPickers();
        }
        async void llenarPickers()
        {
            ConsultasAutor datos = await ConsultasAutor.Instance;
            List<Autores> listaA = await datos.GetAutoresAsync();
            pkAutor.ItemsSource = listaA;
            pkAutor.ItemDisplayBinding = new Binding("nombreCompleto");

            ConsultasGen datosG = await ConsultasGen.Instance;
            List<clases.Genero> listaG = await datosG.GetGenerosAsync();
            pkGen.ItemsSource = listaG;
            pkGen.ItemDisplayBinding = new Binding("tipo");

            ConsultaEdit datosE = await ConsultaEdit.Instance;
            List<clases.Editoriales> listaE = await datosE.GetEditorialesAsync();
            pkEdit.ItemsSource = listaE;
            pkEdit.ItemDisplayBinding = new Binding("nombre_editorial");
        }
        private void vacia()
        {
            txtTitulo.Text = "";
            txtEdi.Text = "";
            pkAutor.SelectedItem = 0;
            guarda.Text = "Registrar";
            pkAutor.SelectedIndex = -1;
            pkGen.SelectedIndex = -1;
            pkEdit.SelectedIndex = -1;
            pkAutor.Title = "Selecciona un autor";
            pkGen.Title = "Selecciona un género";
            pkEdit.Title = "Selecciona una editorial";
        }

        async void guardar(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTitulo.Text) && !string.IsNullOrWhiteSpace(txtEdi.Text))
            {
                if (guarda.Text == "Registrar")
                {
                    libro = new Libros();
                }
                libro.titulo = txtTitulo.Text;
                libro.año_edicion = int.Parse(txtEdi.Text);
                Autores aut = (Autores)pkAutor.SelectedItem;
                libro.id_aut = aut.Id_aut;
                clases.Genero gen = (clases.Genero)pkGen.SelectedItem;
                libro.id_gen = gen.Id_gen;
                clases.Editoriales edit = (clases.Editoriales)pkEdit.SelectedItem;
                libro.id_edit = edit.Id_edit;
                ConsultasLibros datos = await ConsultasLibros.Instance;
                await datos.SaveLibroAsync(libro);
                vacia();
            }
            else
                await DisplayAlert("Registrando...", "Faltan datos", "OK");
        }
        void mostar(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListarLibros());
        }
        async void buscar (object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTitulo.Text))
            {
                ConsultasLibros datos = await ConsultasLibros.Instance;

                libro = null;
                libro = await datos.GetLibroAsync(txtTitulo.Text);
                if(libro != null)
                {
                    txtTitulo.Text = libro.titulo;
                    txtEdi.Text = libro.año_edicion.ToString();
                    pkAutor.SelectedIndex = libro.id_aut - 1;
                    pkEdit.SelectedIndex = libro.id_edit - 1;
                    pkGen.SelectedIndex = libro.id_gen - 1;
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