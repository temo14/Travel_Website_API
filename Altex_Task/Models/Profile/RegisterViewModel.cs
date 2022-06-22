namespace Altex_Task.Models.Profile
{
    public record RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? UserEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? UserPassword { get; set; }
        [Required]
        [Display(Name = "User surname")]
        public string? Lastname { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string? Firstname { get; set; }
        [Required]
        [Display(Name = "User Login")]
        public string? Username { get; set; }
        public string? PhotoPath { get; set; }
    }
}
