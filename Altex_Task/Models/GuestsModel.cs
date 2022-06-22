global using System.Text.Json.Serialization;
using Altex_Task.Models.Profile;

namespace Altex_Task.Models
{
    public record GuestsModel
    {
        public string? FirstName { get; set; }
        public string? Lastname { get; set; }
        //[JsonIgnore]
        //public UserModel? User { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
}
