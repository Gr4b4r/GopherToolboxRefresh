using Microsoft.AspNetCore.Identity;
using GopherToolboxRefresh.Models;
using System.Collections.Generic;

public class User : IdentityUser
{
    
    public ICollection<Userdata> Userdatas { get; set; } = new List<Userdata>();
}

