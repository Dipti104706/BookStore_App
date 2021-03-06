using BookStoreModel;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BookStoreRepository.Interface
{
    public interface IBookRepository
    {
        IConfiguration Configuration { get; }

        string AddBook(BookModel book);
        string UpdateBookDetails(BookModel update);
        object RetrieveBookDetails(int bookId);
        string DeleteBook(int bookId);
        List<BookModel> GetAllBooks();

    }
}