using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace PracticaSQlite.clases
{
    public class Autores
    {
        [PrimaryKey, AutoIncrement]
        public int Id_aut { get; set; }
        public string nombre_autor { get; set; }
        public string apellido_pat { get; set; }
        public string apellido_mat { get; set; }
        public string nacionalidad { get; set; }
        public string nombreCompleto { get => nombre_autor + " " + apellido_pat + " " + apellido_mat; }
    }
    public class ConsultasAutor
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ConsultasAutor> Instance = new AsyncLazy<ConsultasAutor>(async () =>
        {
                var instance = new ConsultasAutor();
                CreateTableResult result = await Database.CreateTableAsync<Autores>();
                return instance;

        });
        public ConsultasAutor()
        {
            Database = new SQLiteAsyncConnection(Constantes.DatabasePath, Constantes.Flags);
        }
        public Task<List<Autores>> GetAutoresAsync()
        {
            return Database.Table<Autores>().ToListAsync();
        }
        public Task<List<Autores>> GetAutoresNotDoneAsync()
        {
            //SQL queries also possible
            return Database.QueryAsync<Autores>("SELECT * FROM [Autores] WHERE [Done] = 0");
        }
        public Task<Autores> GetAutorAsync(string nombre)
        {
            return Database.Table<Autores>().Where(i => i.nombre_autor == nombre).FirstOrDefaultAsync();
        }
        public Task<int> SaveAutorAsync(Autores autor)
        {
            if (autor.Id_aut != 0)
            {
                return Database.UpdateAsync(autor);
            }
            else
            {
                return Database.InsertAsync(autor);
            }
        }
        public Task<int> DeleteAutorAsync(Autores autor)
        {
            return Database.DeleteAsync(autor);
        }
    }
}
