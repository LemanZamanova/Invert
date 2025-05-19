using Invert.Models;

namespace Invert.ViewModels
{
    public class UpdateEmployeeVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Decription { get; set; }
        public string XUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }
       
        //relations
        public int PositionId { get; set; }

        //ozellikleri
        public IFormFile Photo { get; set; }
        public string Image { get; set; }
        public List<Position> Position { get; set; }
    }
}
