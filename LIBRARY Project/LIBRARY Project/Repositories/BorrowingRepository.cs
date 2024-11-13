using LIBRARY_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Repositories
{
    public class BorrowingRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Borrowing> GetAll()
        {
            return _context.Borrowings.ToList();
        }
        public Borrowing GetById(int ID1,int ID2)
        {
            return _context.Borrowings.Find(ID1,ID2);
        }
        public void Add(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }

        public void Update(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            _context.SaveChanges();
        }

        public void Delete(int ID1, int ID2)
        {
            var borrowing = GetById(ID1, ID2);
            if (borrowing != null)
            {
                _context.Borrowings.Remove(borrowing);
                _context.SaveChanges();
            }
        }
        public bool IsBookAvailable(int bookId)
        {
            var book = _context.Books.Find(bookId);
            return book != null && book.Copies > 0;
        }
        public void MarkAsReturned(int ID1, int ID2)
        {
            var borrowing = GetById(ID1, ID2);
            if (borrowing != null && !borrowing.IsReturned)
            {
                borrowing.IsReturned = true;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Borrowing> GetByUserId(int userId)
        {
            return _context.Borrowings.Where(b => b.UserId == userId)
                          .ToList();
        }

        public IEnumerable<Borrowing> GetOverdueBorrowings()
        {
            var today = DateTime.Today;
            return _context.Borrowings.Where(b => b.ReturnDate < today && !b.IsReturned)
                          .ToList();
        }

    }
}
