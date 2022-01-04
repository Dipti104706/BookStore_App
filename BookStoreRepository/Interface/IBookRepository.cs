using BookStoreModel;
using Microsoft.Extensions.Configuration;

namespace BookStoreRepository.Interface
{
    public interface IBookRepository
    {
        IConfiguration Configuration { get; }

        string AddBook(BookModel book);
    }
}