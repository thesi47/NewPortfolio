using System.Security.Cryptography.X509Certificates;

namespace NewPortfolio.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime PassingYear { get; set; }
        public string InstituteName { get; set; }

    }
}
