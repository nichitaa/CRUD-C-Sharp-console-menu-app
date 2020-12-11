using app.Domain;
using app.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace app
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool menu = true;
            while (menu)
            {
                menu = StartMenu();
            }
        }

       

        private static bool StartMenu()
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.ShowMainMenu();

            switch (Console.ReadLine())
            {
                case "1": 
                    return UserMenu();
                case "2": 
                    return ProjectsMenu();
                case "3": // exit
                    return false;
                default:
                    mainMenu.ShowInvalidAction();
                    return StartMenu();
            }
        }

        private static bool UserMenu()
        {
           // User user = new User();

            UserMenu userMenu = new UserMenu();
            userMenu.ShowUserMenu();

            if (Global.IsLoggedIn()) // for logged in user
            {
                userMenu.LoggedInMenu(); // -> display menu for logged in user

                switch(Console.ReadLine())
                {
                    case "1": // log out
                        {
                            userMenu.DisplayLogout();
                            return StartMenu();
                        }

                    case "2": // main menu
                        return StartMenu();

                    case "3": // delete user
                        userMenu.DisplayDelete();
                        return StartMenu();

                    case "4": // edit user credentials
                        userMenu.DisplayEdit();
                        return StartMenu();

                    default:
                        userMenu.InvalidAction();
                        return UserMenu();
                }
            }

            else // if user is not logged in
            {
                userMenu.StartMenu(); // display default user menu

                switch (Console.ReadLine())
                {
                    case "1": // log in
                        {
                            if (userMenu.DisplayLogin()) // if log in went ok
                            {
                                return StartMenu();
                            }
                            return UserMenu();                            
                        }

                    case "2": // sign up
                        {
                            userMenu.DisplaySignup();
                            return UserMenu();
                        }

                    case "3": // main menu
                        return StartMenu();

                    default:
                        userMenu.InvalidAction();
                        return UserMenu();
                }
            }
            
        }

        private static bool ProjectsMenu()
        {
            Project project = new Project();
            ProjectsMenu projectsMenu = new ProjectsMenu();

            projectsMenu.ShowProjectsMenu();

            if (Global.IsLoggedIn()) // for logged in user
            {
                projectsMenu.LoggedInMenu();

                switch (Console.ReadLine())
                {
                    case "1": // new project 
                        {
                            projectsMenu.DisplayCreateNewproject();
                            return ProjectsMenu();
                        }
                        
                    case "2": // display only users projects
                        {
                            projectsMenu.DisplayUsersProjects();
                            return ProjectsMenu();
                        }

                    case "3": // display all projects
                        {
                            projectsMenu.DisplayAllProjects();
                            return ProjectsMenu();
                        }

                    case "4": // display edit a project 
                        {
                            projectsMenu.DisplayEditProject();
                            return ProjectsMenu();
                            
                        }
                    case "5": // display delete a project
                        {
                            projectsMenu.DisplayDeleteProject();
                            return ProjectsMenu();
                        }
                    case "6":
                        {
                            return StartMenu();
                        }
                    case "7":
                        {
                            projectsMenu.DisplaySearchForProjects();
                            return ProjectsMenu();
                        }

                    default:
                        {
                            projectsMenu.InvalidAction();
                            return ProjectsMenu();
                        }
                }
            }  

            else // if user is not logged in
            {
                projectsMenu.StartMenu();
                
                switch (Console.ReadLine())
                {
                    case "1": // display all projects
                        {
                            projectsMenu.DisplayAllProjects();
                            return ProjectsMenu();
                        }

                    case "2":
                        {
                            return StartMenu();
                        }

                    case "3":
                        {
                            projectsMenu.DisplaySearchForProjects();
                            return ProjectsMenu();
                        }

                    default:
                        {
                            projectsMenu.InvalidAction();
                            return ProjectsMenu();
                        }
                }
            }
           
        }
    }

}
