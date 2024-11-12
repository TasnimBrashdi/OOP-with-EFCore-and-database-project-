using LIBRARY_Project.Models;
using System;
using System.Collections.Generic;
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

        public void UpdateByName(string Namebooks)
        {

            var book = GetByName(Namebooks);
            if (book != null)
            {
                _context.Books.Update(book);
                _context.SaveChanges();
            }
         
        }

        public void Delete(int BID)
        {
            var Book = GetById(BID);
            if (Book != null)
            {
                _context.Books.Remove(Book);
                _context.SaveChanges();
            }
        }
    }
}