using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace PracticaSQlite.clases
{
    public class Editoriales
    {
        [PrimaryKey, AutoIncrement]
        public int Id_edit { get; set; }
        public string nombre_editorial { get; set; }
    }
    public class ConsultaEdit
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<ConsultaEdit> Instance =
            new AsyncLazy<ConsultaEdit>(async () =>
            {
                var instance = new ConsultaEdit();
                CreateTableResult result = await Database.CreateTableAsync<Editoriales>();
                return instance;

            });
        public ConsultaEdit()
        {
            Database = new SQLiteAsyncConnection(Constantes.DatabasePath, Constantes.Flags);
        }
        public Task<List<Editoriales>> GetEditorialesAsync()
        {
            return Database.Table<Editoriales>().ToListAsync();
        }
        public Task<List<Editoriales>> GetEditorialNotDoneAsync()
        {
            //SQL queries also possible
            return Database.QueryAsync<Editoriales>("SELECT * FROM [Editorial] WHERE [Done] = 0");
        }
        public Task<Editoriales> GetEditorialAsync(string nombre_editorial)
        {
            return Database.Table<Editoriales>().Where(i => i.nombre_editorial == nombre_editorial).FirstOrDefaultAsync();
        }
        public Task<int> SaveEditorialAsync(Editoriales editorial)
        {
            if (editorial.Id_edit != 0)
            {
                return Database.UpdateAsync(editorial);
            }
            else
            {
                return Database.InsertAsync(editorial);
            }
        }
        public Task<int> DeleteEditorialAsync(Editorial editorial)
        {
            return Database.DeleteAsync(editorial);
        }
    }
}
