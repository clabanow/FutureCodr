namespace FutureCodr.UI.Controllers.Apis
{
    using FutureCodr.Data.Interfaces;
    using FutureCodr.Models;
    using System;
    using System.Collections.Generic;
    using System.Web.Http;

    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepo;

        public UsersController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void Delete(int id)
        {
            _userRepo.DeleteUser(id);
        }

        public IEnumerable<User> Get()
        {
            return _userRepo.GetAllUsers();
        }
    }
}
