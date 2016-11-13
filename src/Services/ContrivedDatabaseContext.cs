using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using WayOfWork.Domain;

namespace WayOfWork.Services
{
    // Really contrived. We also forcing async so we can have async handlers.
    // You would normally have a real Db or some other persistence service.
    public class ContrivedDatabaseContext : IDatabaseContext
    {
        private static readonly ConcurrentDictionary<int, Book> BooksDb = new ConcurrentDictionary<int, Book>();

        // Hacky way of quickly adding seed data for this dempo
        static ContrivedDatabaseContext()
        {
            BooksDb.TryAdd(1, new Book {Id = 1, Author = "Peter Pan", Price = 100.01m, Name = "Awesome Book"});
            BooksDb.TryAdd(2, new Book {Id = 2, Author = "Captain Hook", Price = 132.73m, Name = "Why I do not like Peter Pan"});
        }

        public async Task<IQueryable<Book>> LoadAll()
        {
            var result = new EnumerableQuery<Book>(BooksDb.Select(b => b.Value).ToList());
            return await Task.FromResult(result);
        }

        public async Task<Book> Get(int id)
        {
            Book output;
            if (!BooksDb.TryGetValue(id, out output))
                return null;

            return await Task.FromResult(output);
        }

        public async Task<int> Insert(Book entity)
        {
            // So this is definitely not thread safe :) Just for demo purposes
            entity.Id = BooksDb.Keys.Max() + 1;
            if (!BooksDb.ContainsKey(entity.Id))
                BooksDb.TryAdd(entity.Id, entity);
            else
                return 0;

            return await Task.FromResult(entity.Id);
        }

        public async Task<int> Update(Book entity)
        {
            Book book;
            if (!BooksDb.TryGetValue(entity.Id, out book))
                return 0;

            BooksDb.TryUpdate(entity.Id, entity, book);

            return await Task.FromResult(entity.Id);
        }

        public async Task<int> Delete(int id)
        {
            Book book;
            BooksDb.TryRemove(id, out book);
            if (book == null)
                id = 0;
            return await Task.FromResult(id);
        }
    }
}
