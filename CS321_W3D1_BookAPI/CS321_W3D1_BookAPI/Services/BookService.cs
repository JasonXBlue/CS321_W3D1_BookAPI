using System;
using System.Collections.Generic;
using System.Linq;
using CS321_W3D1_BookAPI.Data;
using CS321_W3D1_BookAPI.Models;

namespace CS321_W3D1_BookAPI.Services
{
    public class BookService : IBookService
    {
        private readonly BookContext _bookContext;

        public BookService(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public void Remove(Book deletedBook)
        {
            _bookContext.Books.Remove(deletedBook);
            _bookContext.SaveChanges();
           
        }

        public Book Get(int bookId)
        {
            return _bookContext.Books.FirstOrDefault(b => b.Id == bookId);
        }

        public IEnumerable<Book> GetAll()
        {
            return _bookContext.Books.ToList();
        }

        public Book Post(Book newBook)
        {
            // store in the list of books
            _bookContext.Books.Add(newBook);
            _bookContext.SaveChanges();
            // return the new book with Id filled in
            return newBook;
        }

        public Book Update(Book updatedBook)
        {
            // get the book object in the current list with this id 
            var currentBook = _bookContext.Books.Find(updatedBook.Id);    

            // return null if book to update isn't found
            if (currentBook == null) return null;

            // copy the property values from the changed book into the
            // one in the db. NOTE that this is much simpler than individually
            // copying each property.
            _bookContext.Entry(currentBook)
                .CurrentValues
                .SetValues(updatedBook);

            // update the book and save
            _bookContext.Books.Update(currentBook);
            _bookContext.SaveChanges();
            return currentBook;
        }
    }
}
