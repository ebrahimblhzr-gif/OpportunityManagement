using System.ComponentModel.DataAnnotations;

namespace OpportunityAPI.DTOs
{
    public class AddOpportunityDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Country { get; set; } = string.Empty;

        [Required]
        public DateTime Deadline { get; set; }

        public bool IsFullyFunded { get; set; }
    }
}
