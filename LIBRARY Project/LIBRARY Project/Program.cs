using LIBRARY_Project.Models;
using LIBRARY_Project.Repositories;

namespace LIBRARY_Project
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var dbContext = new ApplicationDbContext();

           
            var bookRepository = new BooksRepository(dbContext);
            var adminRepository = new AdminRepository(dbContext);
            var userRepository = new UserRepository(dbContext);
            var categoryRepository = new CategoryRepository(dbContext);
            var borrowingRepository = new BorrowingRepository(dbContext);
           
            bool ExitFlag = false;
      
            try
            {
                do
                {
                    Console.WriteLine("\n   - - - - Welcome to Library - - - -  ");
                    Console.WriteLine("\n Choose: \n 1- New Admin  \n 2- Log in Admin \n 3- New User \n 4- Log in User \n 5- Log out ");


                    var choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":

                            AddAdmin(adminRepository);
                            break;

                        case "2":
                            LogAdmin(adminRepository);
                            AdminMenu(adminRepository);
                            break;
                        case "3":

                       

                            break;
                        case "4":



                            break;
                        case "5":
                            ExitFlag = true;
                            break;
                        default:
                            Console.WriteLine("Sorry your choice was wrong");
                            break;



                    }

                    Console.WriteLine("press any key to continue");
                    string cont = Console.ReadLine();

                    Console.Clear();

                } while (ExitFlag != true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR" + ex.Message);
            }
        }

        static void AddAdmin(AdminRepository repository)
        {
            Console.WriteLine("ENTER YOUR Name");
            string name= Console.ReadLine();
            Console.WriteLine("ENTER YOUR EMAIL");
            string email = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Email cannot be empty.");
                return;
            }
            else
            {
                Console.WriteLine("Enter password");
                string pass = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(pass))
                {
                    Console.WriteLine("Password cannot be empty.");
                    return;
                }
                else
                {
                    var admin = new Admin { AName=name,Email = email,Password=pass };
                    repository.iNSERT(admin);

                }

            }
        }

        static void LogAdmin( AdminRepository repository) {
            Console.Write("\nEnter Email: ");
            string email = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            var admin = repository.GetByEmail(email);
            if (admin != null && admin.Password == password)
            {
                Console.WriteLine($"\nWelcome {admin.AName}!");
              
            }
            else
            {
                Console.WriteLine("Invalid Input.");
            }

        }

        static void AdminMenu(AdminRepository repository,BooksRepository books,CategoryRepository category,UserRepository user) {
            bool ExitFlag = false;
            do
            {

                Console.WriteLine("\n Enter the Number of operation you need :");
                Console.WriteLine("\n ---------------SECTION EDIT DATA---------------");
                Console.WriteLine("\n 1- Add New Book");
                Console.WriteLine("\n 2- Update Book");
                Console.WriteLine("\n 3  Remove Book");
                Console.WriteLine("\n 4- Add New Category");
                Console.WriteLine("\n 5- Update Category");
                Console.WriteLine("\n 6- Remove Category");
                Console.WriteLine("\n ---------------SECTION VIEW DATA-------------");
                Console.WriteLine("\n 7- Display All Books");
                Console.WriteLine("\n 8- Display All categories");
                Console.WriteLine("\n 9- Display All users");
                Console.WriteLine("\n 10- Search Book");
                Console.WriteLine("\n 11- Search Category");
                Console.WriteLine("\n 12- log out");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        AddBooks(books,category);
                    
                        break;

                    case "2":
                       
                        break;

                    case "3":
                     
                        break;
                    case "4":

                        AddCategory(category);

                        break;
                    case "5":
                    


                        break;
                    case "6":
                   
                        break;
                    case "7":

                        break;
                    case "8":

                        break;
                    case "9":

                        break;
                    case "10":

                    case "11":

                       
                        break;
                    case "12":
                        Console.WriteLine("You have succeessfully logged out");
                        ExitFlag = true;
                        break;

                    default:
                        Console.WriteLine("Sorry your choice was wrong");
                        break;



                }

                Console.WriteLine("press any key to continue");
                string cont = Console.ReadLine();

                Console.Clear();
            } while (ExitFlag != true);









        }

        static void AddBooks(BooksRepository repository,CategoryRepository category)
        {
            Console.Write("\nEnter Book's Name: ");
            var BookName = Console.ReadLine();
            Console.Write("Enter Author: ");
            var author = Console.ReadLine();
            Console.Write("Enter Copies: ");
            int copies;
            while (!int.TryParse(Console.ReadLine(), out copies) || copies <= 0)
            {
                Console.Write("Invalid input.");
            }

            Console.Write("Enter Period: ");
            var Period = Console.ReadLine();
            Console.Write("Enter Price: ");
            if (float.TryParse(Console.ReadLine(), out var price))
            {
                var book = new Books { BName = BookName, Author = author, Copies = copies, Price = price };
                repository.iNSERT(book);
                Console.WriteLine("Books added successfully!");
            }
            else
            {
                Console.WriteLine("Invalid price input.");
            }
        }


        static void AddCategory(CategoryRepository category)
        {
            Console.WriteLine("Enter category Name:");
            string CateName=Console.ReadLine();
            var categ = new Category { CName=CateName};
            var CateNames = category.GetByName(CateName);
            if (CateNames.CName != CateName)
            {

                category.iNSERT(categ);
            }
            else {
                Console.WriteLine("Category Already Exists");
            
            }



        }

        static void UpdateBooks(BooksRepository repository,CategoryRepository category)
        {
            Console.WriteLine("");


        }




    }
}
