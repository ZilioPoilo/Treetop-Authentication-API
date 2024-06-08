using System.ComponentModel.DataAnnotations;

namespace Authentication_API.Dtos
{
    public class GuestDto
    {
        [Required]
        public string JwtToken { get; set; } = null!;

        [Required]
        public string Id { get; set; } = null!;
    }
}
