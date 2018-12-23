using AccountSystem.Models;
using Model;
using Model.ViewModels;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    public class DebService : IDebService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountClientService _Account;
        public DebService(ApplicationDbContext dbContext ,
            IAccountClientService Account)
        {
            _dbContext = dbContext;
            _Account = Account;
        }

        public bool Add(Debs entity)
        {
            try
            {
                _dbContext.Debs.Add(entity);
                var account = _dbContext.Accounts
                              .Single(x => x.Id == entity.AccountId);
                account.Total += entity.Money;
                _Account.Update(account);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = _dbContext.Debs.Single(x => x.Id == id);
                var account = _dbContext.Accounts
                             .Single(x => x.Id == model.AccountId);
                account.Total -= model.Money;
                _dbContext.Debs.Remove(model);
                _Account.Update(account);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Debs Get(int id)
        {
            var result = new Debs();
            try
            {
                result = _dbContext.Debs
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public IEnumerable<Debs> GetAll(int page)
        {
            throw new NotImplementedException();
        }

        public bool Update(Debs entity)
        {
            try
            {
                var model = _dbContext.Debs.Single(x => x.Id == entity.Id);
                model.Money = entity.Money;
                model.Description = entity.Description;
                _dbContext.Entry(model).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
    }
}
