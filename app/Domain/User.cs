using app.Domain;
using System;
using System.Linq;

namespace app
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }

        private readonly PBLContext context = new Domain.PBLContext();


        // crud only
        public User Create(string firstName, string lastName, string email, string password)
        {
            // todo: validation!
            var id = Guid.NewGuid();
            var user = new User()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
            };
            Global.user = user;
            Global.userId = id;

            // save to db
            context.Users.Add(user);
            context.SaveChanges();

            return user;
        }

        public void Delete(Guid id)
        {
            User user = new User { Id = id };
            context.Users.Attach(user);
            context.Users.Remove(user);
            context.SaveChanges();
        }

        public void Update(User updatedUser)
        { 
            var entity = context.Users.Find(updatedUser.Id);
            context.Entry(entity).CurrentValues.SetValues(updatedUser);
            context.SaveChanges();
        } 

    }

}
