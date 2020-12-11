using app.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace app.Menu
{
    class ProjectsMenu
    {

        Project utils = new Project();
        PBLContext context = new PBLContext();

        // main Menu
        public void ShowProjectsMenu()
        {
            Console.Clear();
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("    PROEJECTS MENU    \n");
            Console.ResetColor();
        }

        public void LoggedInMenu()
        {
            Console.WriteLine("1 → Add new project");
            Console.WriteLine("2 → Display my projects");
            Console.WriteLine("3 → Display all projects");
            Console.WriteLine("4 → Edit a project");
            Console.WriteLine("5 → Delete a project");
            Console.WriteLine("6 → Main Menu");
            Console.WriteLine("7 → Search for a project");
            Console.Write("# → ");
        }

        public void StartMenu()
        {
            Console.WriteLine("1 → Display all projects");
            Console.WriteLine("2 → Main Menu");
            Console.WriteLine("3 → Search for a project");
            Console.Write("# → ");
        }

        public void InvalidAction()
        {
            Console.WriteLine("Please select a valid operation!");
            Console.ReadLine();
        }

            

        // sub Meniu
        public void DisplayCreateNewproject()
        {
            Console.Clear();
            Console.WriteLine("New Project Form\n");

            Console.Write("Project Title: ");
            var projectTitle = Console.ReadLine();
            Console.Write("Project Description: ");
            var projectDescription = Console.ReadLine();
            Console.Write("Mentor's first name: ");
            var mentorFirstName = Console.ReadLine();
            Console.Write("Mentor's last name: ");
            var mentorLastName = Console.ReadLine();

            while (true)
            {
                Console.Write("Save (y/n): ");
                var save = Console.ReadLine();
                if (save == "y")
                {
                    utils.Create(projectTitle, projectDescription, mentorFirstName, mentorLastName);
                    Console.WriteLine("The project was successfulty uploaded! \n" + "Press Enter to continue");
                    Console.ReadLine();
                    break;
                }

                else if (save == "n")
                {
                    Console.WriteLine("The project was not uploaded! \n" + "Press Enter to continue");
                    Console.ReadLine();
                    break;
                }

                else
                {
                    Console.WriteLine("Please select y - yes , n - no! \n" + "Press Enter to continue");
                    Console.ReadLine();
                }
            }

        }

        public void DisplayUsersProjects()
        {
            var usersProjects = context.Projects
                                .Where(project => project.UserId == Global.userId)
                                .ToList();
            DisplayPagination(usersProjects);

        }

        public void DisplayAllProjects()
        {
            Console.Clear();
            Console.WriteLine("Order By ?");
            Console.WriteLine("1. Title");
            Console.WriteLine("2. Rating");
            Console.WriteLine("3. CreatedAt");
            Console.Write("# → ");
            switch(Console.ReadLine())
            {
                case "1":
                    {
                        var ordered = context.Projects.OrderBy(p => p.Title).ToList();
                        DisplayPagination(ordered);
                        break;
                    }
                case "2":
                    {
                        var ordered = context.Projects.OrderBy(p => p.Rating).ToList();
                        DisplayPagination(ordered);
                        break;
                    }
                case "3":
                    {
                        var ordered = context.Projects.OrderBy(p => p.CreatedAt).ToList();
                        DisplayPagination(ordered);
                        break;
                    }
                default:
                    {
                        InvalidAction();
                        DisplayAllProjects();
                        break;
                    }
            }
        }

        public void DisplaySearchForProjects()
        {
            Console.Clear();
            Console.WriteLine("Search a project by title:");
            Console.Write("Search for: ");
            var search = Console.ReadLine();
            List<Project> searchProjects = context.Projects.Where(proj => proj.Title.Contains(search)).ToList();
            DisplayProjects(searchProjects);

            Console.WriteLine("Those are similar projects for `{0}` ! \n" + "Press Enter to continue", search);
            Console.ReadLine();

        }

        public void DisplayEditProject()
        {
            List<Project> usersProjects = context.Projects
                                .Where(project => project.UserId == Global.userId)
                                .ToList();
            DisplayProjects(usersProjects, true); // true -> onlyTitle display

            Console.WriteLine("You are allowed to EDIT only this projects\n");
            Console.Write("Enter the project Title you wanna EDIT: ");

            var editProjectTitle = Console.ReadLine();
            var exists = context.Projects
                                .Where(project => project.UserId == Global.userId & project.Title == editProjectTitle)
                                .FirstOrDefault();
            if (exists != null)
            {
                Console.Clear();
                Console.WriteLine("Edit Project Form:");
                Console.Write("New Title: ");
                var updatedTitle = Console.ReadLine();
                Console.Write("New Description: ");
                var updatedDescription = Console.ReadLine();

                Project updatedProject = new Project()
                {
                    Id = exists.Id,
                    Title = updatedTitle,
                    Description = updatedDescription,
                    CreatedAt = exists.CreatedAt,
                    UpdatedAt = DateTime.Now,
                    UserId = Global.userId,
                    Rating = 5.0F,
                };

                utils.Update(updatedProject);

                Console.WriteLine("The project has been updated!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();

            }

            else
            {
                Console.WriteLine("Such a project does not exist or you are not allowed to edit it!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }

        }

        public void DisplayDeleteProject()
        {
            List<Project> usersProjects = context.Projects
                                .Where(project => project.UserId == Global.userId)
                                .ToList();
            DisplayProjects(usersProjects, true); // true -> onlyTitle display

            Console.WriteLine("You can DELETE only from this projects\n");
            Console.Write("Enter the project Title you wanna DELETE: ");

            var deleteProjectTitle = Console.ReadLine();
            var exists = context.Projects
                                .Where(project => project.UserId == Global.userId & project.Title == deleteProjectTitle)
                                .FirstOrDefault();
            if (exists != null)
            {

                utils.Delete(exists);

                Console.WriteLine("The project has been successfuly deleted!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();

            }

            else
            {
                Console.WriteLine("Such a project does not exist or you are not allowed to delete it!");
                Console.WriteLine("Press enter to continue");
                Console.ReadLine();
            }
        }

        public void DisplayPagination(List<Project> list)
        {
            var take = 3;
            var skip = 0;
            var condition = true;
            var page = 1;
            while (condition)
            {
                var projects = list.Skip(skip).Take(take).ToList();
              
                if (projects.Count >= 1)
                {
                    DisplayProjects(projects);
                    Console.WriteLine("Page number {0}\n", page);
                    Console.WriteLine("Use arrow keys to navigate between pages ");
                    Console.WriteLine("ESC - exit ");
                    var ch = Console.ReadKey(false).Key;
                    switch (ch)
                    {
                        case ConsoleKey.RightArrow:
                            {
                                page++;
                                skip += 3;
                                break;
                            }
                        case ConsoleKey.LeftArrow:
                            {
                                if (skip >= 3)
                                {
                                    page--;
                                    skip -= 3;
                                }
                                else
                                {
                                    Console.WriteLine("You are already on the first page!\nPress enter to continue");
                                    Console.ReadLine();
                                }
                                break;
                            }
                        case ConsoleKey.Escape:
                            {
                                condition = false;
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Invalid action\npress enter to continue");
                                Console.ReadLine();
                                break;
                            }
                    }
                }

                else
                {
                    Console.WriteLine("This is the last Page\nNo more projects exists!");
                    Console.WriteLine("Press enter to continue");
                    Console.ReadLine();
                    page--;
                    skip -= 3;
                }

            }
        }

        public void DisplayProjects(List<Project> projects, bool onlyTitle = false)
        {
            Console.Clear();
            Console.WriteLine("Projects:\n");

            if (onlyTitle)
            {
                var i = 0;
                Console.WriteLine(new string('-', 40));
                Console.WriteLine("| {0, -3} | {1, -30} |", "NR.", "TITLE");
                Console.WriteLine("|" + new string('-', 38) + "|");
                projects.ForEach(item =>
                {
                    Console.WriteLine("| {0, -3} | {1, -30} |", i++, item.Title);
                    //Console.WriteLine("Title: " + item.Title + "\n");

                });
                Console.WriteLine(new string('-', 40));
            }

            else
            {
                var i = 0;
                Console.WriteLine(new string('-', 107));
                Console.WriteLine("| {0, -3} | {1, -30} | {2, -30} | {3, -8} | {4, -20} |", "NR.", "TITLE", "DESCRIPTION", "RATING", "CREATEDAT");
                Console.WriteLine("|" + new string('-', 105) + "|");
                Console.WriteLine("|" + new string('-', 105) + "|");
                projects.ForEach((item) => {
                    Console.WriteLine("| {0, -3} | {1, -30} | {2, -30} | {3, -8} | {4, -20} |", i++, item.Title, item.Description, item.Rating, item.CreatedAt);

                });
                Console.WriteLine(new string('-', 107) + "\n");
            }
        }
    }
}
