using AccountSystem.Models;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class HomeService : IHomeService
    {
        private readonly ApplicationDbContext _dbContext;
        public HomeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int GetAllAccounts()
        {
            int result = 0;
            try
            {
                result = _dbContext.Users.ToList().Count();
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        public decimal GetAllDebs()
        {
            decimal result = 0;
            try
            {
                var debs = _dbContext.Debs.ToList();
                foreach (var item in debs)
                {
                    result += item.Money;
                }
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }

        public int GetAllMenbers()
        {
            int result = 0;
            try
            {
                result = _dbContext.Accounts.ToList().Count();
            }
            catch (Exception)
            {
                result = 0;
            }
            return result;
        }
    }
}
