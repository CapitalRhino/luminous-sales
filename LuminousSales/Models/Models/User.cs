using Models.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class User : IBaseProperties
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Passcode { get; set; }
        public Role Role { get; set; }
        public ICollection<Deal> ItemsSoldByUser{ get; set; }
        public ICollection<Product> ItemsStockedByUser { get; set; }
    }
}
