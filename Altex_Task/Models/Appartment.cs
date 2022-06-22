namespace Altex_Task.Models
{
    public record Appartment
    {
        public string? Id{ get; set; }
        public string? City{ get; set; }
        public string? Address { get; set; }
        public int NumOfBeds { get; set; }
        public string? PhotoLocation { get; set; }
        public string? DistanceFromCenter{ get; set; }      
        public string? Description { get; set; }
    }
}
