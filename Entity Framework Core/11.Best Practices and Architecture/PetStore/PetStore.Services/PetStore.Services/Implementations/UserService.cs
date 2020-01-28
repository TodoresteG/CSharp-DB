namespace PetStore.Services.Implementations
{
    using Data;
    using Data.Models;
    using PetStore.Services.Models.User;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly PetStoreDbContext data;

        public UserService(PetStoreDbContext data)
        {
            this.data = data;
        }

        public int Register(string name, string email)
        {
            if (name.Length > ModelValidator.UserValidator.NameMaxLength)
            {
                throw new ArgumentException($"User name cannot be more than {ModelValidator.UserValidator.NameMaxLength} characters");
            }

            if (name == null || email == null)
            {
                throw new ArgumentException("User name and email have to be filled");
            }

            if (this.data.Users.Any(u => u.Email == email))
            {
                throw new ArgumentException("User with given email already exists");
            }

            var user = new User()
            {
                Name = name,
                Email = email
            };

            this.data.Users.Add(user);
            this.data.SaveChanges();

            return user.Id;
        }

        public UserListingServiceModel SearchUserByEmail(string email)
        {
            return this.data
                    .Users
                    .Select(u => new UserListingServiceModel
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email
                    })
                    .FirstOrDefault(u => u.Email == email);
        }

        public bool Exists(int userId)
        {
            return this.data.Users.Any(u => u.Id == userId);
        }
    }
}
