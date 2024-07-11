using Microsoft.AspNetCore.Identity;
using GopherToolboxRefresh.Models;
using System.Collections.Generic;

public class User : IdentityUser
{
    
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}

