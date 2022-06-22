namespace Altex_Task.Models
{
    public class BookingGuests
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? HostId { get; set; }
        public string? GuestId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Approve { get; set; } = false;
    }
}
