using Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Role : BaseUserManagmentEntity
    {
        public Role() : base(){}
        public Role(string Name)  : base(Name){}
    }
}