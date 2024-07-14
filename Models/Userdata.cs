using System.ComponentModel.DataAnnotations;
using GopherToolboxRefresh.Models;
public class Userdata
{
    [Key]
    public int UserDataId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [Required]
    public User User { get; set; }

    [Required]
    public Quest Quest { get; set; }
}
