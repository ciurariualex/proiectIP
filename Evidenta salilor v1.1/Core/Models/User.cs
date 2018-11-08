using Core.Domain.Business.Models;
using Core.Domain.Definition;
using Core.Domain.Models.Implementation;
using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Models.User
{
	public class User : IdentityUser, IDeletable
	{
		private BaseModel baseModel;

		public User()
		{
		}

		public User(BaseModel baseModel)
		{
			this.baseModel = baseModel;
		}

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public bool IsDeleted { get; set; }
	}
}
