using BookStoreModel;
using System.Collections.Generic;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        string AddBook(BookModel book);
        string UpdateBookDetails(BookModel update);
        object RetrieveBookDetails(int bookId);
        string DeleteBook(int bookId);
        List<BookModel> GetAllBooks();
    }
}