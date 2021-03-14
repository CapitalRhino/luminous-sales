using Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models
{
    public class Permission : BaseUserManagmentEntity
    {
        public Permission() : base(){}
        public Permission(string Name) : base(Name){}
    }
}