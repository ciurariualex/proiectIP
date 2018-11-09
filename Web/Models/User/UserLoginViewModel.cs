namespace Web.Models.User
{
	public class UserLoginViewModel
    {
		public string Email { get; set; }

		public string  Password { get; set; }

		public bool IsPersistent { get; set; }

		public string ReturnUrl { get; set; }
	}
}
