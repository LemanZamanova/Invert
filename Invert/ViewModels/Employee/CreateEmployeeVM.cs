using System.ComponentModel.DataAnnotations;
using Invert.Models;

namespace Invert.ViewModels
{
    public class CreateEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Decription { get; set; }
        public string XUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }

        //relations
        [Required]
        public int? PositionId { get; set; }
        //Ozellikleri
        public IFormFile Photo { get; set; }
        public List<Position>? Position { get; set; }
    }
}
