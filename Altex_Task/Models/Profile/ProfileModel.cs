namespace Altex_Task.Models.Profile
{
    public class ProfileModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Appartment? appartment { get; set; }
        public IEnumerable<GuestsModel>? Guests { get; set; }
        public IEnumerable<BookingModel>? Booking { get; set; }
    }
}
