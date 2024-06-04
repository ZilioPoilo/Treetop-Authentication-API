using System.ComponentModel.DataAnnotations;

namespace Authentication_API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string Number { get; set; } = null!;

        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        public string? FirstName { get; set; }

        public string? LastName { get; set; }
    }
}
