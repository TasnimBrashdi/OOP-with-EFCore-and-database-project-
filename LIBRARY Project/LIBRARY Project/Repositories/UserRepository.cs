using LIBRARY_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIBRARY_Project.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public User GetByName(string uName)
        {
            return _context.Users.FirstOrDefault(u =>u.UName  == uName);
        }

        public User GetById(int ID)
        {
            return _context.Users.Find(ID);
        }
        public void iNSERT(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateByName(string Name)
        {

            var user = GetByName(Name);
            if (user != null)
            {
                _context.Users.Update(user);
                _context.SaveChanges();
            }

        }

        public void DeleteById(int UID)
        {
            var user = GetById(UID);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public void CountByGender(List<User> users)
        {
            var genderCounts = users.GroupBy(user => user.Gender).Select(group => new { Gender = group.Key, Count = group.Count() });

            foreach (var c in genderCounts)
            {
                Console.WriteLine($"{c.Gender}: {c.Count}");
            }
        }


    }
}
