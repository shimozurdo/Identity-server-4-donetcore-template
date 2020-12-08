
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace webapi.Models
{
	public class ApplicationRole: IdentityRole
	{
		public string ApplicationClient { get; set; }
	}
}
