using LIBRARY_Project.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LIBRARY_Project.Repositories
{
    public class BooksRepository
    {

        
            private readonly ApplicationDbContext _context;

            public BooksRepository(ApplicationDbContext context)
            {
                _context = context;
            }
        public IEnumerable<Books> GetAll()
        {
            return _context.Books.ToList();
        }
        public Books GetByName(string bookName)
        {
            return _context.Books.FirstOrDefault(b => b.BName == bookName);
        }

        public Books GetById(int ID)
        {
            return _context.Books.Find(ID);
        }
        public void iNSERT(Books books)
        {
            _context.Books.Add(books);
            _context.SaveChanges();
        }

        public void UpdateByName(string name)
        {
            var book = GetByName(name);
            if (book != null)
            {
       
                _context.Books.Update(book);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        public void UpdateBook(Books book)
        {
            var Book = GetById(book.BId);
            if (Book != null)
            {
                Book.BName = book.BName;
                Book.Author = book.Author;
                Book.Copies = book.Copies;
                _context.Books.Update(Book);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }

        }
            public void DeleteById(int BID)
        {
            var Book = GetById(BID);
            if (Book != null)
            {
                _context.Books.Remove(Book);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }

        public float GetTotalPrice()
        {
            return _context.Books.Sum(book => book.Price);
        }


        public float getMaxPrice()
        {
            return _context.Books.Max(book => book.Price);  
        }

        public int getTotalBorrowedBooks()
        {

            return _context.Books.Sum(book => book.Copies);
        }
        public int getTotalBooksPerCategoryName(string name)
        {

            return _context.Books.Count(b => b.BName == name);

        }


    }
}