using System.ComponentModel.DataAnnotations;

namespace OpportunityAPI.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
