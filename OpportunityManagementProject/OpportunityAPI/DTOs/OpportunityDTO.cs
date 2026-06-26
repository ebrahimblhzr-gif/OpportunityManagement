namespace OpportunityAPI.DTOs
{
    public class OpportunityDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public DateTime Deadline { get; set; }

        public bool IsFullyFunded { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
