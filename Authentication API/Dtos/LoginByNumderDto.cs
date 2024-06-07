using System.ComponentModel.DataAnnotations;

namespace Authentication_API.Dtos
{
    public class LoginByNumderDto
    {
        [Required]
        public string Number { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;  

    }
}
