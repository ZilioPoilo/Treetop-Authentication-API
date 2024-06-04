using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace Authentication_API.Models
{
    public class Guest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        public string Number { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [Required]
        public string PasswordSalt { get; set; } = null!;

        public string Role { get; set; } = "User";

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public void CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            this.PasswordSalt = Convert.ToBase64String(hmac.Key);
            this.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
        }
    }
}
