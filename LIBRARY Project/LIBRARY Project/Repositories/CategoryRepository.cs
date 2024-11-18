using LIBRARY_Project.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Repositories
{
    public class CategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.Include(b => b.Books).ToList();
        }
        public Category GetByName(string Name)
        {
            return _context.Categories.FirstOrDefault(C => C.CName == Name);
        }

        public Category GetById(int ID)
        {
            return _context.Categories.Find(ID);
        }
        public void iNSERT(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void UpdateByName(string Name)
        {

            var category = GetByName(Name);
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }

        }

        public void Update( Category category)
        {
            if (category != null)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
        }


        public void DeleteById(int UID)
        {
            var category = GetById(UID);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
