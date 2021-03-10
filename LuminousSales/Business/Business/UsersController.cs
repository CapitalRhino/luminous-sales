using Models;

namespace LuminousSales.Business
{
    public class UsersController
    {
        private LuminousContext userContext;

        public UsersController()
        {
            this.userContext = new LuminousContext();
        }
    }
}
