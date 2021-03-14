using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.Base
{
    public abstract class BaseUserManagmentEntity
    {
        public BaseUserManagmentEntity(){}
        protected BaseUserManagmentEntity(string Name)
        {
            this.Name = Name;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
