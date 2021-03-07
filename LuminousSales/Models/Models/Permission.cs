using Models.Models.Interfaces;

namespace Models.Models
{
    public class Permission : IBaseProperties
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}