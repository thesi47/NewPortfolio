using Microsoft.AspNetCore.Identity;

namespace NewPortfolio.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
