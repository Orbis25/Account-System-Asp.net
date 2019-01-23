using AccountSystem.Models;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                result = (_dbContext.Clients.ToList().Count()-1);
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

        public int GetAllMyAccount(string id)
        {
            try
            {
                var model = _dbContext.Clients.Single(x => x.ApplicationUserId == id);
                return _dbContext.Accounts.Count(x => x.ClientId == model.Id);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetAllMyDebs(string id)
        {
            try
            {
                Client client = _dbContext.Clients.Single(x => x.ApplicationUserId == id);
                Account account = _dbContext.Accounts.Single(x => x.ClientId == client.Id);
                return _dbContext.Debs.Count(x => x.AccountId == account.Id && x.Money != 0);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int GetAllMyRequest(string id)
        {
            try
            {
                return _dbContext.Requests.Count(x => x.ApplicationUserId == id);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
