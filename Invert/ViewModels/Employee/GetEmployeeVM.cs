namespace Invert.ViewModels
{
    public class GetEmployeeVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public int Id { get; set; }
        public string Image { get; set; }
        //relations
        public int PositionId { get; set; }
        //ozellikleri
        public string PositionName { get; set; }
    }
}
