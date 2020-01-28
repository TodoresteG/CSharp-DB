namespace PetStore.Services
{
    using Data.Models;
    using Models.Pet;

    using System;
    using System.Collections.Generic;

    public interface IPetService
    {
        void BuyPet(Gender gender, DateTime dateOfBirth, decimal price, string description, int breedId, int categoryId);

        void SellPet(int petId, int userId);

        bool Exists(int petId);

        IEnumerable<PetListingServiceModel> All(int page = 1);

        int Total();

        PetDetailsServiceModel Details(int id);

        bool Delete(int id);
    }
}
