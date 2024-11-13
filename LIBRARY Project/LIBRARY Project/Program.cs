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
                            AdminMenu(adminRepository, bookRepository,categoryRepository,userRepository);
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
            string name = Console.ReadLine();
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
                    var admin = new Admin { AName = name, Email = email, Password = pass };
                    repository.iNSERT(admin);

                }

            }
        }

        static void LogAdmin(AdminRepository repository) {
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

        static void AdminMenu(AdminRepository repository, BooksRepository books, CategoryRepository category, UserRepository user) {
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

                        AddBooks(books, category);

                        break;

                    case "2":
                        UpdateBooks(books, category);
                        break;

                    case "3":
                        RemoveBooks(books,category);
                        break;
                    case "4":

                        AddCategory(category);

                        break;
                    case "5":
                        UpdateCategory(category);


                        break;
                    case "6":
                        RemoveCategory(category);
                        break;
                    case "7":
                        DisplayAllBooks(books);
                        break;
                    case "8":
                        DisplayAllCatego(category);
                        break;
                    case "9":
                        DisplayAllUsers(user);
                        break;

                    case "10":
                        SeachBooks(books);
                        break;
                    case "11":
                        SearchCate(category);

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

        static void AddBooks(BooksRepository repository, CategoryRepository category)
        {
            Console.Write("\nEnter Book's Name: ");
            var BookName = Console.ReadLine();
            Console.Write("Enter Author: ");
            var author = Console.ReadLine();
            Console.Write("Enter Category ID: ");
            int cate;
            while (!int.TryParse(Console.ReadLine(), out cate) || cate <= 0)
            {
                Console.Write("Invalid input.");
            }
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
                var book = new Books { BName = BookName, Author = author, Copies = copies, Price = price,CategoryID= cate };
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
            string CateName = Console.ReadLine();
            var categ = new Category { CName = CateName };
            var CateNames = category.GetByName(CateName);
            if (CateNames.CName != CateName)
            {

                category.iNSERT(categ);
            }
            else {
                Console.WriteLine("Category Already Exists");

            }



        }

        static void UpdateBooks(BooksRepository repository, CategoryRepository category)
        {
            Console.Write("\nEnter Book's Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter Copies: ");
            int copies;
            while (!int.TryParse(Console.ReadLine(), out copies) || copies <= 0)
            {
                Console.Write("Invalid input.");
            }

            repository.UpdateByName(name, author, copies);
            Console.WriteLine("Books Upadte successfully!");





        }

        static void RemoveBooks(BooksRepository books, CategoryRepository category)
        {
            Console.Write("\nEnter Book ID to remove: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId) || bookId <= 0)
            {
                Console.Write("Invalid input. Enter a valid book ID: ");
            }

            books.DeleteById(bookId);
            Console.WriteLine("Book removed successfully, if it existed.");
        }

        static void UpdateCategory(CategoryRepository category)
        {
            Console.Write("\nEnter Category Name: ");
            string name = Console.ReadLine();


            category.UpdateByName(name);
            Console.WriteLine("Books category successfully!");

        }
        static void RemoveCategory(CategoryRepository category)
        {
            Console.Write("\nEnter Category ID: ");
            int Id;
            while (!int.TryParse(Console.ReadLine(), out Id) ||Id <= 0)
            {
                Console.Write("Invalid input. Enter a valid  ID: ");
            }

            category.DeleteById(Id);
            Console.WriteLine("category removed successfully, if it existed.");
        }

        static void DisplayAllBooks(BooksRepository repository)
        {
            var books = repository.GetAll();
            Console.WriteLine("\nBooks:");
            foreach (var b in books)
            {
                Console.WriteLine($"Book Name: {b.BName} -Author's Name: {b.Author}  -Copies: {b.Copies} - Pirce: {b.Price} -Period:  {b.Period} -Category: {b.CategoryID}");
            }
        }
        static void DisplayAllCatego(CategoryRepository repository)
        {
            var Cate = repository.GetAll();
            Console.WriteLine("\nCategories:");
            foreach (var c in Cate)
            {
                Console.WriteLine($"Category ID: {c.CId}Category Name: {c.CName}");

            }
        }
        static void DisplayAllUsers(UserRepository repository)
        {
            var user = repository.GetAll();
            Console.WriteLine("\nUsers:");
            foreach (var u in user)
            {
                Console.WriteLine($"User ID: {u.UID}  User Name: {u.UName} Gender {u.Gender}");

            }
        }

        static void SeachBooks(BooksRepository repository)
        {
            Console.WriteLine("Enter Book Name");
            string bookName = Console.ReadLine();

            var book = repository.GetByName(bookName);
            if (book != null)
            {
                Console.WriteLine("\nBook Details:");
                Console.WriteLine($"ID: {book.BId}");
                Console.WriteLine($"Name: {book.BName}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"Copies Available: {book.Copies}");
                Console.WriteLine($"Price: {book.Price}");
                Console.WriteLine($"Category ID: {book.CategoryID}");
                Console.WriteLine($"Period: {book.Period}");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }

        }

        static void SearchCate(CategoryRepository repository) {
            Console.Write("Enter Category Name: ");
            string categoryName = Console.ReadLine();

            var category = repository.GetByName(categoryName);
            if (category != null)
            {
                Console.WriteLine("\nCategory Details:");
                Console.WriteLine($"ID: {category.CId}");
                Console.WriteLine($"Name: {category.CName}");
              
            }
            else
            {
                Console.WriteLine("Category not found.");
            }

        }





    }
}
