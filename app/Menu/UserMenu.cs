using app.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app.Menu
{
    class UserMenu
    {
        private readonly PBLContext context = new PBLContext();
        private User utils = new User();

        // main Menu
        public void ShowUserMenu()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("    USER MENU    \n");
            Console.ResetColor();
        }

        public void LoggedInMenu()
        {
            Console.WriteLine("You are already logged in");
            Console.WriteLine("1 → Log me out");
            Console.WriteLine("2 → Main Menu");
            Console.WriteLine("3 → Delete me from the database !!!");
            Console.WriteLine("4 → Edit my credentials");
            Console.Write("# → ");
        }

        public void StartMenu()
        {
            Console.WriteLine("1 → LogIn");
            Console.WriteLine("2 → SignUp");
            Console.WriteLine("3 → Main Menu");
            Console.Write("# → ");
        }

        public void InvalidAction()
        {
            Console.WriteLine("Please select a valid operation!");
            Console.ReadLine();
        }


        // sub Menu
        public bool DisplayLogin()
        {
            Console.WriteLine("\nLogIn credentials: \n");
            Console.Write("Your email: ");
            var email = Console.ReadLine();
            Console.Write("Your password: ");
            var password = Console.ReadLine();


            var exists = context.Users
                                .Where(i => i.Email == email & i.Password == password)
                                .FirstOrDefault();
            if (exists != null)
            {
                Global.userId = exists.Id; // update the global userId
                Global.user = exists;
                Console.WriteLine("Wellcome back, {0}", Global.user.FirstName);
                Console.WriteLine("Press enter to go to the Main Menu");
                Console.ReadLine();
                return true;
            }

            else
            {
                Global.userId = Guid.Empty;
                Global.user = null;
                Console.WriteLine("Such user does not exists!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                return false;
            }
        }

        public void DisplaySignup()
        {
            Console.WriteLine("\nSignUp details: \n");
            Console.Write("First Name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            var lastName = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Create a secure passoword: ");
            var password = Console.ReadLine();   

            // save to db
            utils.Create(firstName, lastName, email, password);

            // success message
            Console.WriteLine("Congradulations, now you are a legit user!");
            Console.ReadLine();
        }

        public void DisplayLogout()
        {
            Global.userId = Guid.Empty; // reset globals
            Global.user = null;
            Console.WriteLine("Successfuly logged out!");
            Console.WriteLine("Press enter to go to Main Menu");
            Console.ReadLine();
        }

        public void DisplayEdit()
        {
            Console.Clear();
            Console.WriteLine("Edit Auth credentials Form: ");

            Console.Write("New First Name: ");
            var newFirstName = Console.ReadLine();
            Console.Write("New Last Name: ");
            var newLastName = Console.ReadLine();
            Console.Write("New Email: ");
            var newEmail = Console.ReadLine();
            Console.Write("New Password: ");
            var newPassword = Console.ReadLine();

            User updatedUser = new User()
            {
                Id = Global.userId,
                FirstName = newFirstName,
                LastName = newLastName,
                Email = newEmail,
                Password = newPassword,
            };

            utils.Update(updatedUser);

            Global.user = updatedUser;

            Console.WriteLine("New credentials was updated!");
            Console.Write("Press enter to continue!");
            Console.ReadLine();
        }

        public void DisplayDelete()
        {
            Console.Clear();
            Console.WriteLine("Are you sure that you wanna be deleted?");
            Console.Write("y - yes/ n - no: ");
            var response = Console.ReadLine();
            if (response == "y")
            {
                Global.userId = Guid.Empty;
                Global.user = null;
                utils.Delete(Global.userId);
                Console.WriteLine("Successfuly removed from DB");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }

            else if (response == "n")
            {
                Console.Clear();
                Console.WriteLine("Nice to keep you !");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Please enter y or n !");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
                DisplayDelete();
            }
        }
    }
}
