namespace Altex_Task.Models
{
    public class City
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public string? city { get; set; }
    }
}
