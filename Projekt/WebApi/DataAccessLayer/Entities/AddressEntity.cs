using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities
{
    public record AddressEntity : BaseEntity
    {
        public int UserId { get; set; }
        [JsonIgnore]
        public UserEntity User { get; set; }
        public string Street { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }
    }

}
