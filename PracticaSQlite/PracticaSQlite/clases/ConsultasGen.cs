using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace PracticaSQlite.clases
{
    public class Genero
    {
        [PrimaryKey, AutoIncrement]
        public int Id_gen { get; set; }
        public string tipo { get; set; }
    }

    public class AsyncLazy<T> : Lazy<Task<T>>
    {
        readonly Lazy<Task<T>> instance;
        public AsyncLazy(Func<T> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }
        public AsyncLazy(Func<Task<T>> factory)
        {
            instance = new Lazy<Task<T>>(() => Task.Run(factory));
        }
        public TaskAwaiter<T> GetAwaiter()
        {
            return instance.Value.GetAwaiter();
        }
    }
    public class ConsultasGen
    {
        static SQLiteAsyncConnection Database;
        public static readonly AsyncLazy<ConsultasGen> Instance = new
        AsyncLazy<ConsultasGen>(async () =>
        {
            var instance = new ConsultasGen();
            CreateTableResult result = await Database.CreateTableAsync<Genero>();
            return instance;
        });
        public ConsultasGen()
        {
            Database = new SQLiteAsyncConnection(Constantes.DatabasePath,
            Constantes.Flags);
        }
        public Task<List<Genero>> GetGenerosAsync()
        {
            return Database.Table<Genero>().ToListAsync();
        }
        public Task<List<Genero>> GetGenerosNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Genero>("SELECT * FROM [Genero] WHERE [Done] = 0");
        }
        public Task<Genero> GetGeneroAsync(string tipo)
        {
            return Database.Table<Genero>().Where(i => i.tipo ==
            tipo).FirstOrDefaultAsync();
        }
        public Task<int> SaveGeneroAsync(Genero genero)
        {
            if (genero.Id_gen != 0)
            {
                return Database.UpdateAsync(genero);
            }
            else
            {
                return Database.InsertAsync(genero);
            }
        }
        public Task<int> DeleteGeneroAsync(Genero genero)
        {
            return Database.DeleteAsync(genero);
        }
    }
}
