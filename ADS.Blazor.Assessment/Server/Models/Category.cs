using System.ComponentModel.DataAnnotations;

namespace ADS.Blazor.Assessment.Server.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
