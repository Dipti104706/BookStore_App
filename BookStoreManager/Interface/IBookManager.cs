using BookStoreModel;

namespace BookStoreManager.Interface
{
    public interface IBookManager
    {
        string AddBook(BookModel book);
        string UpdateBookDetails(BookModel update);
        object RetrieveBookDetails(int bookId);
        string DeleteBook(int bookId);
    }
}