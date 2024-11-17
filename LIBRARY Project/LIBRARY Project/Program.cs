using LIBRARY_Project.Models;
using LIBRARY_Project.Repositories;
using static System.Reflection.Metadata.BlobBuilder;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

                            AddUser(userRepository);

                            break;
                        case "4":

                            LoginUser(userRepository);

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
                Console.WriteLine("\n 7- Display All Books and Search Book");
                Console.WriteLine("\n 8- Display All categories and Search Category");
                //Complete from here 
                Console.WriteLine("\n 9- Display All Borroed Books");
                Console.WriteLine("\n 10- Display All users");
         
                Console.WriteLine("\n 11- log out");

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

        
            if (books == null || !books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }
            Console.WriteLine("\nBooks:");
            foreach (var b in books)
            {
                Console.WriteLine($"Book Name: {b.BName} -Author's Name: {b.Author}  -Copies: {b.Copies} - Pirce: {b.Price} -Period:  {b.Period} -Category: {b.CategoryID}");
            }


            Console.WriteLine("\nWould you like to search for a specific book? (yes/no)");
            string searchResponse = Console.ReadLine()?.Trim().ToLower();

            if (searchResponse == "yes")
            {
                Console.WriteLine("Enter the name of the book you want to search for:");
                string bookName = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(bookName))
                {
                    Console.WriteLine("Invalid input. Please provide a book name.");
                    return;
                }

                var book = repository.GetByName(bookName);
                if (book != null)
                {
                    Console.WriteLine("\nBook Details:");
                    Console.WriteLine($"ID: {book.BId}");
                    Console.WriteLine($"Name: {book.BName}");
                    Console.WriteLine($"Author: {book.Author}");
                    Console.WriteLine($"Copies Available: {book.Copies}");
                    Console.WriteLine($"Price: {book.Price:C}");
                    Console.WriteLine($"Category ID: {book.CategoryID}");
                    Console.WriteLine($"Period: {book.Period}");
                }
                else
                {
                    Console.WriteLine("Book not found.");
                }
            }
            else if (searchResponse == "no")
            {
                Console.WriteLine("No search performed. Returning to main menu.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
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

            Console.WriteLine("\nWould you like to search for a specific Category? (yes/no)");
            string searchResponse = Console.ReadLine()?.Trim().ToLower();

            if (searchResponse == "yes")
            {
                Console.Write("Enter Category Name You to search for: ");
                string categoryName = Console.ReadLine();

                var category = repository.GetByName(categoryName);
                if (category != null)
                {
                    Console.WriteLine("\nCategory Details:");
                    Console.WriteLine($"ID: {category.CId}");
                    Console.WriteLine($"Name: {category.CName}");
                    Console.WriteLine($"No Books: {category.NoBooks}");

                }

                else
                {
                    Console.WriteLine("Category not found.");
                }
            }
            else if (searchResponse == "no")
            {
                Console.WriteLine("No search performed. Returning to main menu.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
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

       

       
        static void AddUser(UserRepository repository)
        {
            Console.WriteLine("ENTER YOUR Name");
            string name = Console.ReadLine();

            Console.WriteLine("ENTER YOUR Gender (Male/Female)");
            string genderInput = Console.ReadLine();

       
            if (!Enum.TryParse(typeof(User.GENDER), genderInput, true, out var genderEnum))
            {
                Console.WriteLine("Invalid gender. Please enter 'Male' or 'Female'.");
                return;
            }

            Console.WriteLine("Enter passcode");
            string pass = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(pass))
            {
                Console.WriteLine("Passcode cannot be empty.");
                return;
            }

        
            var user = new User
            {
                UName = name,
                Gender = (User.GENDER)genderEnum,
                Passcode = pass
            };

            repository.iNSERT(user);
        }

        static void LoginUser(UserRepository repository)
        {
            Console.WriteLine("Enter your Name:");
            string userName = Console.ReadLine();

     
            User user = repository.GetByName(userName);

            if (user == null)
            {
                Console.WriteLine("Username not found.");
                return;
            }

            Console.WriteLine("Enter your passcode:");
            string passcode = Console.ReadLine();

            if (user.Passcode == passcode)
            {
                Console.WriteLine($"Welcome back, {user.UName}!");
            }
            else
            {
                Console.WriteLine("Invalid passcode. Please try again.");
            }
        }

        static void UserMenu(UserRepository repository, BooksRepository books, CategoryRepository category) {
            bool ExitFlag = false;
            do
            {

                Console.WriteLine("\n Enter the Number of operation you need :");
     
     
                Console.WriteLine("\n 1- Display All Books and Search");
                Console.WriteLine("\n 2- Display All categories and Search");
                Console.WriteLine("\n 3- Borrow Books");
                Console.WriteLine("\n 4- Return Books");
                Console.WriteLine("\n 5- log out");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":

                        DisplayAllBooks(books);
                        break;

                    case "2":
                        DisplayAllCatego(category);
                        break;
                    case "3":

                 
                        break;
                    case "4":


                        break;
                    case "5":
    
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

       
        static void BorrowBooks(BooksRepository booksRepo, BorrowingRepository borrowingRepo, int userId)
        {


            var bookss = booksRepo.GetAll();


            if (bookss == null || !bookss.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }
            Console.WriteLine("\nBooks:");
            foreach (var b in bookss)
            {
                Console.WriteLine($"Book ID: {b.BId} Book Name: {b.BName} -Author's Name: {b.Author}  -Copies: {b.Copies} - Pirce: {b.Price} -Period:  {b.Period} -Category: {b.CategoryID}");
            }

            Console.WriteLine("Enter the ID of the book you want to borrow:");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid input. Please enter a numeric book ID.");
                return;
            }

            if (!borrowingRepo.IsBookAvailable(bookId))
            {
                Console.WriteLine("The book is not available for borrowing.");
                return;
            }

    
            var book = booksRepo.GetById(bookId);
            if (book == null)
            {
                Console.WriteLine("The book does not exist.");
                return;
            }

            Console.WriteLine("Enter the return date (yyyy-MM-dd):");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime returnDate))
            {
                Console.WriteLine("Invalid date format. Please try again.");
                return;
            }

        
            book.Copies -= 1;
            booksRepo.UpdateByName(book.BName,book.Author,book.Copies);

         
            var borrowing = new Borrowing
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.Now,
                ReturnDate = returnDate,
                IsReturned = false
            };
            borrowingRepo.Add(borrowing);

            Console.WriteLine($"Book with ID {bookId} borrowed successfully. Please return by {returnDate:yyyy-MM-dd}.");
        }


        static void ReturnBooks(BooksRepository booksRepo, BorrowingRepository borrowingRepo, int userId)
        {
            Console.WriteLine("Enter the ID of the book you want to return:");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid input. Please enter a numeric book ID.");
                return;
            }

            var borrowing = borrowingRepo.GetByUserId(userId).FirstOrDefault(b => b.BookId == bookId && !b.IsReturned);
            if (borrowing == null)
            {
                Console.WriteLine("No borrowing record found for this book, or the book has already been returned.");
                return;
            }

       
            borrowingRepo.MarkAsReturned(borrowing.BookId, borrowing.UserId);

          
            var book = booksRepo.GetById(bookId);
            if (book != null)
            {
                book.Copies += 1;
                booksRepo.UpdateByName(book.BName,book.Author,book.Copies);
          
            }

            Console.WriteLine($"Book with ID {bookId} has been successfully returned.");
        }










    }
}
