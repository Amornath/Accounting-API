using Microsoft.AspNetCore.Identity;

namespace ScopoERP.ViewModels
{
    public class TokenViewModel
    {
        public string Access_Token { get; set; }
        public List<Result> Results { get; set; }
    }
    public class Result
    {
        public string ActionName { get; set; }
        public IdentityResult ActionResult { get; set; }
    }
}
