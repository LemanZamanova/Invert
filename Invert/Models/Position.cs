using System.ComponentModel.DataAnnotations;

namespace Invert.Models
{
    public class Position : BaseEntity
    {
        [MaxLength(20)]
        [MinLength(3)]
        public string? Name { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
