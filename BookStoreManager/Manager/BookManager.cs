using BookStoreManager.Interface;
using BookStoreModel;
using BookStoreRepository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStoreManager.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository bookRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserManager"/> class
        /// </summary>
        /// <param name="repository">taking repository as parameter</param>
        public BookManager(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        /// <summary>
        /// Register method for manager class
        /// </summary>
        /// <param name="userData">passing register model</param>
        /// <returns>Returns string if Registration is successful</returns>
        public string AddBook(BookModel book)
        {
            try
            {
                return this.bookRepository.AddBook(book);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string UpdateBookDetails(BookModel update)
        {
            try
            {
                return this.bookRepository.UpdateBookDetails(update);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object RetrieveBookDetails(int bookId)
        {
            try
            {
                return this.bookRepository.RetrieveBookDetails(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeleteBook(int bookId)
        {
            try
            {
                return this.bookRepository.DeleteBook(bookId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<BookModel> GetAllBooks()
        {
            try
            {
                return this.bookRepository.GetAllBooks();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
