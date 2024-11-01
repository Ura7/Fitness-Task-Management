using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using WebApplication3.Models;

namespace WebApplication3.Areas.Identity.Data;

public class AppUser : IdentityUser
{
	public string Firstname { get; set; }
	public string Lastname { get; set; }

	public string Number { get; set; }
	public string Gender { get; set; }

	public DateTime Birthday { get; set; }

	public byte[] ProfilePhoto { get; set; }

	public string Rol {  get; set; }

	
}

