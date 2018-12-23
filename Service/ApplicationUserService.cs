using AccountSystem.Models;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ApplicationUserService : IAccountService
    {
        private readonly ApplicationDbContext _dbContext;
        public ApplicationUserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ApplicationUser GetUser(string id)
        {
            ApplicationUser user;
            try
            {
                user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            }
            catch (Exception)
            {
                user = null;
            }
            return user;
        }
    }
}
