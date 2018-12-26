using AccountSystem.Models;
using Model;
using Model.ViewModels;
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

        public ApplicationUser LastUserId()
        {
            var user = new ApplicationUser();
            try
            {
                user = _dbContext.Users.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            }
            catch (Exception)
            {
                return user;
            }
            return user;
        }

        public bool UpdateUserName(UpdateUserNameViewModelAppUs model)
        { ApplicationUser user;
            try
            {
                user = _dbContext.Users.FirstOrDefault(x => x.Id == model.Id);
                user.UserName = model.UserName;
                _dbContext.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
