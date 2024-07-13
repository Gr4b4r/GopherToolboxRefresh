using Microsoft.AspNetCore.Identity;
using GopherToolboxRefresh.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


public class User : IdentityUser
{
    
    public ICollection<Userdata> Userdatas { get; set; } = new List<Userdata>();
	
	[StringLength(100)]
	public string Name { get; set; } = string.Empty;
	[StringLength(100)]
	public string Surname { get; set; } = string.Empty;
	[StringLength(50)]
	public string Nickname { get; set; } = string.Empty;
	[DataType(DataType.Date)]
	public DateTime Birthdate { get; set; }
	public string Phone { get; set; } = string.Empty;
}

