using Data.Base;

namespace Models.Models
{
    public class Permission : BaseUserManagmentEntity
    {
        public Permission() : base(){}
        public Permission(string Name) : base(Name){}
    }
}