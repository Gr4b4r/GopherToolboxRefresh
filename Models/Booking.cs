using System.ComponentModel.DataAnnotations;
using GopherToolboxRefresh.Models;
public class Booking
{
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public User User { get; set; } = new User();

    [Required]
    public Quest Quest { get; set; } = new Quest();
}
