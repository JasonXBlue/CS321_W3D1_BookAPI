using System;
using System.Collections.Generic;
using CS321_W3D1_BookAPI.Models;

namespace CS321_W3D1_BookAPI.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book Get(int bookId);
        Book Post(Book newBook);
        Book Update(Book updatedBook);
        void Remove(Book deletedBook);
    }
}
