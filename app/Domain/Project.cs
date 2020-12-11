using app.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace app
{
    public class Project
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public float Rating { get; set;  }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public Guid UserId { get; set; }

        public Guid MentorId { get; set; }

        PBLContext context = new PBLContext();


        // crud only
        public void Create(string projectTitle, string projectDescription, string mentorFirstName, string mentorLastName)
        {
            var project = new Project()
            {
                Title = projectTitle,
                Description = projectDescription,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Global.userId,
                Rating = 5.0F,
            };

            context.Projects.Add(project);
            context.SaveChanges();
        }

        public void Delete(Project project)
        {
            context.Projects.Attach(project);
            context.Projects.Remove(project);
            context.SaveChanges();
        }

        public void Update(Project updatedProject )
        {
            var entity = context.Projects.Find(updatedProject.Id);
            context.Entry(entity).CurrentValues.SetValues(updatedProject);
            context.SaveChanges();
        }

    }

}
