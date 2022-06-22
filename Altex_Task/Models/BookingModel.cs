namespace Altex_Task.Models
{
    public record BookingModel
    {
        public string? City { get; set; }
        //[JsonIgnore]
        public Appartment? Appartment { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
}
