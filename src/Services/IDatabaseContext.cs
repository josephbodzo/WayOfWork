using System.Linq;
using System.Threading.Tasks;
using WayOfWork.Domain;

namespace WayOfWork.Services
{
    public interface IBookService : IDatabaseContext
    {
    }

    // This would normally be a generic interface i.e. Insert<T>, etc.
    // For this demo we just forcing it to Book
    public interface IDatabaseContext
    {
        Task<IQueryable<Book>> LoadAll();
        Task<Book> Get(int id);
        Task<int> Insert(Book entity);
        Task<int> Update(Book entity);
        Task<int> Delete(int id);
    }
}
