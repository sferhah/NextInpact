using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NextInpact.Data
{
    public class ThreadSafeSqlite
    {
       
        static ThreadSafeSqlite _instance;

        public static ThreadSafeSqlite Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ThreadSafeSqlite();
                }
                return _instance;
            }
        }

        public static Object locker = new Object();

        protected SQLiteConnection database;

        private ThreadSafeSqlite()
        {            
            var sqliteFilename = String.Format("{0}.db3", "NextInpact");
            String connetcionString = DependencyService.Get<IStringConnectionProvider>().GetConnectionString(sqliteFilename);
            this.database =  new SQLiteConnection(connetcionString);
        }


        public void Init(Type first, params Type[] types)
        {
            foreach(var t in types.Concat(new[] { first }))
            {
                database.CreateTable(t);
            }
        }

        public async Task<List<T>> GetAllAsync<T>() where T : new()
        {
            return await Task.Run(() => GetAll<T>());
        }

        private List<T> GetAll<T>() where T : new()
        {
            lock (locker)
            {
                return database.Table<T>().ToList();
            }
        }

        public async Task UpdateAsync<T>(T item) where T : new()
        {
            await Task.Run(() => Update(item));
        }

        private void Update<T>(T item) where T : new()
        {
            lock (locker)
            {
                database.Update(item, typeof(T));               
            }
        }


        public async Task<T> GetAsync<T>(Object value) where T : new()
        {
            return await Task.Run(() => Get<T>(value));
        }

        private T Get<T>(Object value) where T : new()
        {
            lock (locker)
            {
                return database.Find<T>(value);               
            }
        }

        public async Task RemoveAsync<T>(Object value) where T : new()
        {
            await Task.Run(() => Remove<T>(value));
        }

        private void Remove<T>(Object value) where T : new()
        {
            lock (locker)
            {   
                database.Delete<T>(value);                              
            }
        }

        public async Task InsertOrReplaceAsync<T>(T item) where T : new()
        {
            await Task.Run(() => InsertOrReplace(item));
        }

        private void InsertOrReplace<T>(T value) where T : new()
        {
            lock (locker)
            {
                database.InsertOrReplace(value, typeof(T));                
            }
        }

    }
   
}
