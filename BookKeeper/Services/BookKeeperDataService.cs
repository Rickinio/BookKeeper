using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookKeeper.Services
{
    public class BookKeeperDataService
    {
        const string DatabaseFilename = "BookKeeperSQLite.db3";

        const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

        SQLiteAsyncConnection Database;

        public BookKeeperDataService()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(DatabasePath, Flags);
            var result = await Database.CreateTableAsync<BookItem>();
        }

        public async Task<List<BookItem>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<BookItem>().ToListAsync();
        }

        //public async Task<List<BookItem>> GetItemsNotDoneAsync()
        //{
        //    await Init();
        //    return await Database.Table<BookItem>().Where(t => t.Done).ToListAsync();

        //    // SQL queries are also possible
        //    //return await Database.QueryAsync<BookItem>("SELECT * FROM [BookItem] WHERE [Done] = 0");
        //}

        public async Task<BookItem> GetItemAsync(string isbn)
        {
            await Init();
            return await Database.Table<BookItem>().Where(i => i.ISBN == isbn).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(BookItem item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(BookItem item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
