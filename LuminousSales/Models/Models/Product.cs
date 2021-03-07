using Models.Models.Interfaces;

namespace Models.Models
{
    public class Product : IBaseProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double AvailableInStock { get; set; }
    }
}