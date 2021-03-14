using Models;
using Models.Models;
using System.Linq;

namespace LuminousSales.Business
{
    public class UsersController
    {

        private LuminousContext contex;

        public UsersController()
        {
            this.contex = new LuminousContext();
        }
    }
}
