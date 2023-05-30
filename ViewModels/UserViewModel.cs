namespace ScopoERP.ViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       // public byte[] UserImage { get; set; }
        public IList<string> Roles { get; set; }
        public string StringRoles { get; set; }

    }

    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RoleViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
