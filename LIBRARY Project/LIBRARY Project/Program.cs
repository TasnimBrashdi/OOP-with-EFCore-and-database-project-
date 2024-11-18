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
                    Console.ForegroundColor = ConsoleColor.Cyan;
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
                            AdminMenu(adminRepository, bookRepository,categoryRepository,userRepository, borrowingRepository);
                            break;
                        case "3":

                            AddUser(userRepository);

                            break;
                        case "4":

                   

                            int? userId = LoginUser(userRepository);

                            if (userId.HasValue)  
                            {
                                UserMenu(userRepository, bookRepository, categoryRepository, borrowingRepository, userId.Value);
                            }
                            else
                            {
                                Console.WriteLine("Login failed. Please try again.");
                            }
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
                Console.WriteLine("ERROR " + ex.Message);
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

        static void AdminMenu(AdminRepository repository, BooksRepository books, CategoryRepository category, UserRepository user,BorrowingRepository borrowing) {
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
                Console.WriteLine("\n 7- Add New User");
                Console.WriteLine("\n 8- Update User");
                Console.WriteLine("\n 9- Remove User");

                Console.WriteLine("\n ---------------SECTION VIEW DATA---------------");
                Console.WriteLine("\n 10- Display All Books and Search Book");
                Console.WriteLine("\n 11- Display All Categories and Search Category");
                Console.WriteLine("\n 12- Display All Borrowed Books");
                Console.WriteLine("\n 13- Display All Users");
         
                Console.WriteLine("\n 14- Log out");

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
                        AddUser(user);
                        break;
                    case "8":
                        UpdateUser(user);
                        break;
                    case "9":
                        RemoveUser(user);
                        break;
                    case "10":
                        DisplayAllBooks(books);
                        break;
                    case "11":
                        DisplayAllCatego(category);
                        break;
                        case "12":
                            DisplayAllBorrowed(borrowing);
                        break;  
                    case "13":
                        DisplayAllUsers(user);
                        break;

 
              
                    case "14":
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
            int categoryId;
            while (!int.TryParse(Console.ReadLine(), out categoryId) || categoryId <= 0)
            {
                Console.Write("Invalid input. Please enter a positive number for Category ID: ");
            }

          
            var categ = category.GetById(categoryId);
            if (categ == null)
            {
                Console.WriteLine("Category not found. Please add the category first.");
                return;
            }
            Console.Write("Enter Copies: ");
            int copies;
            while (!int.TryParse(Console.ReadLine(), out copies) || copies <= 0)
            {
                Console.Write("Invalid input.");
            }

            Console.Write("Enter Period: ");
            int period;
            while (!int.TryParse(Console.ReadLine(), out period) || period <= 0)
            {
                Console.Write("Invalid input.");
            }
            Console.Write("Enter Price: ");
            if (float.TryParse(Console.ReadLine(), out var price))
            {
                var book = new Books { BName = BookName, Author = author, Copies = copies, Price = price,Period=period ,CategoryID= categoryId };
                repository.iNSERT(book);
                category.UpdateByName(BookName);

                categ.NoBooks += 1;
                category.Update(categ);
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

            Console.Write("\nEnter Book ID to Update: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId) || bookId <= 0)
            {
                Console.Write("Invalid input. Enter a valid book ID: ");
            }
            var book = repository.GetById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found. No action taken.");
                return;
            }
            else 
            { 
            Console.Write("\nEnter Book's Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Author: ");
            string author = Console.ReadLine();
            Console.Write("Enter number to add Copies: ");
            int copies;
            while (!int.TryParse(Console.ReadLine(), out copies) || copies <= 0)
            {
                Console.Write("Invalid input.");
            }
                book.BName = name;
                book.Author = author;
                book.Copies += copies;   
               repository.UpdateBook(book);
            Console.WriteLine("Books Upadte successfully!");



                }

        }

        static void RemoveBooks(BooksRepository books, CategoryRepository category)
        {
            Console.Write("\nEnter Book ID to remove: ");
            int bookId;
            while (!int.TryParse(Console.ReadLine(), out bookId) || bookId <= 0)
            {
                Console.Write("Invalid input. Enter a valid book ID: ");
            }
            var book = books.GetById(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found. No action taken.");
                return;
            }

            books.DeleteById(bookId);
            Console.WriteLine("Book removed successfully.");

            if (book.CategoryID == null || book.CategoryID <= 0)
            {
                Console.WriteLine("Book does not have a valid category ID. No category updates will be made.");
                return;
            }

            int categoryId = book.CategoryID.Value;

            var cate = category.GetById(categoryId);
            if (cate != null)
            {
                cate.NoBooks = Math.Max(0, cate.NoBooks - 1);
                category.Update(cate); 
                Console.WriteLine($"Category '{cate.CName}' updated. Remaining books: {cate.NoBooks}");
            }
            else
            {
                Console.WriteLine("No category found for this book. No category updates will be made.");
            }
        }

        static void UpdateCategory(CategoryRepository category) 
        {
            Console.Write("\nEnter Category Name: ");
            string name = Console.ReadLine();


            var cate = category.GetByName(name);

            if (cate == null)
            {
                Console.WriteLine("Category not found.");
                return;
            }

            Console.WriteLine($"Updating Info for category: {cate.CName}");

            Console.Write("Enter new category name: ");
            string newName = Console.ReadLine();

    
            if (!string.IsNullOrEmpty(newName))
            {
                cate.CName = newName;
            }

            category.UpdateByName(cate.CName);
            Console.WriteLine("Category updated successfully!");

        }
  
        static void UpdateUser(UserRepository userRepository)
        {
            Console.Write("\nEnter User Name to Update: ");
            string name = Console.ReadLine();

    
            var user = userRepository.GetByName(name);

            if (user == null)
            {
                Console.WriteLine("User not found.");
                return;
            }

            Console.WriteLine($"Updating Info for: {user.UName}");

            Console.Write("Enter new name: ");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                user.UName = newName;
            }

            Console.Write("Enter new passcode: ");
            string newPasscode = Console.ReadLine();
            if (!string.IsNullOrEmpty(newPasscode))
            {
                user.Passcode = newPasscode;
            }

        
            userRepository.UpdateByName(user.UName);
            Console.WriteLine("User details updated successfully!");
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
        static void RemoveUser(UserRepository user)
        {
            Console.Write("\nEnter user ID: ");
            int Id;
            while (!int.TryParse(Console.ReadLine(), out Id) || Id <= 0)
            {
                Console.Write("Invalid input. Enter a valid  ID: ");
            }

            user.DeleteById(Id);
            Console.WriteLine("user removed successfully, if it existed.");

        }

        static void DisplayAllBooks(BooksRepository repository)
        { 
            var books = repository.GetAll();

        
            if (books == null || !books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }
            Console.WriteLine("\n------------ Books List ------------");
            foreach (var b in books)
            { 
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"Book ID: {b.BId,-5} | Book Name: {b.BName,-15} | Author's Name: {b.Author,-10} | Copies: {b.Copies,-5} | Price: {b.Price,-10:C} | Period: {b.Period,-10} | Category: {b.CategoryID,-10}");
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

        static void DisplayAllBorrowed(BorrowingRepository borrowing)
        {
            var borrowings = borrowing.GetAll();
            Console.WriteLine("\nBorrowed Books:");
            foreach (var b in borrowings)
            {
                Console.WriteLine($"User Borrowed: {b.UserId} Book ID: {b.BId} Borrowed Date: {b.BorrowDate} R  Return Date: {b.ReturnDate} Is Returned ? {b.IsReturned}");

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

        static int? LoginUser(UserRepository repository)
        {
            Console.WriteLine("Enter your Name:");
            string userName = Console.ReadLine();

     
            User user = repository.GetByName(userName);

            if (user == null)
            {
                Console.WriteLine("Username not found.");
                return null;
            }

            Console.WriteLine("Enter your passcode:");
            string passcode = Console.ReadLine();

            if (user.Passcode == passcode)
            {
                Console.WriteLine($"Welcome back, {user.UName}!");
                return user.UID;
            }
            else
            {
                Console.WriteLine("Invalid passcode. Please try again.");
                return null;
            }
        }

        static void UserMenu(UserRepository repository, BooksRepository books, CategoryRepository category,BorrowingRepository borrowing, int userId) {
            bool ExitFlag = false;
            do
            {

                Console.WriteLine("\n Enter the Number of operation you need :");
     
     
                Console.WriteLine("\n 1- Display All Books and Search");
                Console.WriteLine("\n 2- Display All categories and Search");
                Console.WriteLine("\n 3- Borrow Books");
                Console.WriteLine("\n 4- Return Books");
                Console.WriteLine("\n 5- Over due Borrowings");
                Console.WriteLine("\n 6- log out");

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
                        BorrowBooks(books,borrowing,userId);


                        break;
                    case "4":
                        ReturnBooks(books, borrowing,userId);

                        break;
                    case "5":
                        GetOverdueBorrowings(borrowing,userId);
                        break;
                    case "6":
    
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
            var userBorrowings = borrowingRepo.GetByUserId(userId);
            if (userBorrowings.Any(b => !b.IsReturned))
            {
                Console.WriteLine("You already have an active borrowed book. Please return it before borrowing another.");
                return;
            }

            var bookss = booksRepo.GetAll();


            if (bookss == null || !bookss.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }
            Console.WriteLine("\n------------ Books List ------------");
            foreach (var b in bookss)
            {
                Console.WriteLine($"Book ID: {b.BId,-5} | Book Name: {b.BName,-25} | Author's Name: {b.Author,-20} | Copies: {b.Copies,-5} | Price: {b.Price,-10:C} | Period: {b.Period,-10} | Category: {b.CategoryID,-10}");
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
            if (book.Copies <= 0)
            {
                Console.WriteLine("No copies available to borrow.");
                return;
            }

            DateTime returnDate = DateTime.Now.AddDays(book.Period);

            book.Copies -= 1;
            booksRepo.GetByName(book.BName);


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

            borrowing.IsReturned = true;
            borrowing.ActualDate = DateTime.Now;

            borrowingRepo.Update(borrowing);
            borrowingRepo.MarkAsReturned(borrowing.BookId,userId);
            var book = booksRepo.GetById(bookId);
            if (book != null)
            {
                book.Copies += 1;

                booksRepo.GetByName(book.BName);
          
            }

            Console.WriteLine($"Book with ID {bookId} has been successfully returned.");
        }

        static void GetOverdueBorrowings(BorrowingRepository borrowing,int userId)
        {

            Console.WriteLine("Fetching overdue borrowings...");

       
            var overdueBorrowings = borrowing.GetOverdueBorrowings().Where(b => b.UserId == userId)
                                                 .ToList();

            if (!overdueBorrowings.Any())
            {
                Console.WriteLine("You have no overdue borrowings.");
                return;
            }

            Console.WriteLine("\nOverdue Borrowings:");
            foreach (var b in overdueBorrowings)
            {
                Console.WriteLine($"- Book ID: {b.BookId}, Due Date: {b.ReturnDate:yyyy-MM-dd}, Borrowed On: {b.BorrowDate:yyyy-MM-dd}");
            }

        }









    }
}
