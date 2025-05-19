

namespace Invert.Models
{
    public class Employee:BaseEntity
    {
        public string Name  { get; set; }
        public string Surname { get; set; }
        public string Decription { get; set; }
        public string XUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string Image { get; set; }
        //relations
        public int PositionId { get; set; }

        public Position Position { get; set; }
    }
}
