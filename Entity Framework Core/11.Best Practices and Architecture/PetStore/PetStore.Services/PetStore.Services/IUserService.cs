namespace PetStore.Services
{
    using Models.User;

    public interface IUserService
    {
        int Register(string name, string email);

        UserListingServiceModel SearchUserByEmail(string email);

        bool Exists(int userId);
    }
}
