namespace PetStore.Services
{
    using Models.Toy;

    public interface IToyService
    {
        int BuyFromDistributor(string name, string description, decimal price, int brandId, int categoryId);

        int BuyFromDistributor(AddingToyServiceModel model);

        void SellToyToUser(int toyId, int userId);

        bool Exists(int toyId);
    }
}
