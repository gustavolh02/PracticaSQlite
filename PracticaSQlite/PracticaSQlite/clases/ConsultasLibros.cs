using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace PracticaSQlite.clases
{
    public class Libros
    {
		[PrimaryKey, AutoIncrement]
		public int Id_lib { get; set; }
		public string titulo { get; set; }
		public int año_edicion { get; set; }

		[Indexed]
		public int id_aut { get; set; }

		[Indexed]
		public int id_gen { get; set; }

		[Indexed]
		public int id_edit { get; set; }
	}

	public class ViewLibros
	{
		public int Id_lib { get; set; }
		public string titulo { get; set; }
		public int año_edcion { get; set; }
		public string nombre_autor { get; set; }
		public string apellido_pat { get; set; }
		public string tipo { get; set; }
		public string nombre_editorial { get; set; }
		public string autor { get => nombre_autor + " " + apellido_pat; }

	}
	public class ConsultasLibros
    {
		static SQLiteAsyncConnection Database;

		public static readonly AsyncLazy<ConsultasLibros> Instance =
			new AsyncLazy<ConsultasLibros>(async () =>
			{
				var instance = new ConsultasLibros();
				CreateTableResult result = await Database.CreateTableAsync<Libros>();
				return instance;
			});
		public ConsultasLibros()
		{
			Database = new SQLiteAsyncConnection(Constantes.DatabasePath, Constantes.Flags);
		}
		public Task<List<Libros>> GetLibrosAsync()
		{
			return Database.Table<Libros>().ToListAsync();
		}
		public Task<List<Libros>> GetLibrosNotDoneAsync()
		{
			//SQL queries also possible
			return Database.QueryAsync<Libros>("SELECT * FROM [Libros] WHERE [Done] = 0");
		}
		public Task<Libros> GetLibroAsync(string titulo)
		{
			return Database.Table<Libros>().Where(i => i.titulo == titulo).FirstOrDefaultAsync();
		}
		public Task<int> SaveLibroAsync(Libros libro)
		{
			if (libro.Id_lib != 0)
			{
				return Database.UpdateAsync(libro);
			}
			else
			{
				return Database.InsertAsync(libro);
			}
		}
		public Task<int> DeleteLibroAsync(Libros libro)
		{
			return Database.DeleteAsync(libro);
		}
		public Task<List<ViewLibros>> GetAllLibrosAsync()
		{
			string query = "SELECT Libros.Id_lib, Libros.titulo, Libros.año_edicion, " +
				"Autores.nombre, Autores.apellido_pat, Genero.tipo, Editorial.nombre_editorial " +
				"FROM Libros JOIN Autores ON Autores.Id_Aut=Libros.id_aut " +
				"JOIN Genero ON Genero.Id_gen=Libros.id_gen " +
				"JOIN Editorial ON Editorial.Id_edit=Libros.id_edit";

			return Database.QueryAsync<ViewLibros>(query);
		}
	}
}
