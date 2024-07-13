// ViewModels/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace GopherToolboxRefresh.ViewModels
{
    public class RegisterViewModel
    {
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[StringLength(100)]
		public string Surname { get; set; } = string.Empty;

		[StringLength(50)]
		public string? Nickname { get; set; } = string.Empty;

        [Required]
		[DataType(DataType.Date)]
		public DateTime Birthdate { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;

		public string Phone { get; set; } = string.Empty;

		[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;

		public string Rola { get; set; } = string.Empty;
	}
}
