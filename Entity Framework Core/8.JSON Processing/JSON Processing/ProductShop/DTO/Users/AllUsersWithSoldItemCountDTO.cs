namespace ProductShop.DTO.Users
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class AllUsersWithSoldItemCountDTO
    {
        [JsonProperty("usersCount")]
        public int UsersCount { get; set; }

        [JsonProperty("users")]
        public ICollection<UsersWithSoldItemInfoDTO> Users { get; set; }
    }
}
