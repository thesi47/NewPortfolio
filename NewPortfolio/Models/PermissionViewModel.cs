namespace NewPortfolio.Models
{
    public class PermissionViewModel
    {
        public string UserId { get; set; }
        public List<string> SelectedPermissions { get; set; } = new();
    }
}
