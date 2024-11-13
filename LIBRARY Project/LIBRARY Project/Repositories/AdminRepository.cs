using LIBRARY_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Repositories
{
    public class AdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Admin> GetAll()
        {
            return _context.Admins.ToList();
        }
        public Admin GetByName(string Name)
        {
            return _context.Admins.FirstOrDefault(a => a.AName == Name);
        }

        public Admin GetById(int ID)
        {
            return _context.Admins.Find(ID);
        }
        public void iNSERT(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }

        public void UpdateByName(string Name)
        {

            var admin = GetByName(Name);
            if (admin!= null)
            {
                _context.Admins.Update(admin);
                _context.SaveChanges();
            }

        }

        public void DeleteById(int UID)
        {
            var admin = GetById(UID);
            if (admin != null)
            {
                _context.Admins.Remove(admin);
                _context.SaveChanges();
            }
        }
    }
}
