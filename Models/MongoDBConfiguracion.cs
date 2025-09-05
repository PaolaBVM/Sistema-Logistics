using System.ComponentModel.DataAnnotations;

namespace SistemaLogistics.Models
{
    public class MongoDBSettings
    {
        [Required(ErrorMessage = "ConnectionString is required")]
        public string ConnectionString { get; set; } = string.Empty;

        [Required(ErrorMessage = "DatabaseName is required")]
        public string DatabaseName { get; set; } = string.Empty;
    }
}