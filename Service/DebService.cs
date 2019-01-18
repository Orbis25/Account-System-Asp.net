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
            }
        }

        public Debs Get(int id)
        {
            try
            {
                return _dbContext.Debs
                    .Single(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Debs> GetAll(int page)
        {
            throw new NotImplementedException();
        }

        public DetailPageViewModel Filter(FilterDebsViewModel model, int page)
       {
            DetailPageViewModel result = new DetailPageViewModel();
            try
            {
                DateTime from = DateTime.Parse(model.From);
                DateTime to = Convert.ToDateTime(model.To);
                int pageToQuantity = 10;
                result.Account = _dbContext.Accounts
                          .Include(x => x.Client)
                          .Single(x => x.Id == model.IdAccount);
                result.Account.Debs = _dbContext.Debs
                  .Where(x => x.AccountId == model.IdAccount && (x.DateTime >= from) && (x.DateTime <= to))
                  .OrderByDescending(x => x.DateTime)
                  .Skip((page - 1) * pageToQuantity)
                  .Take(pageToQuantity).ToList();

                result.Account.Payments = _dbContext.Payments.Where(x => x.AccountId == model.IdAccount);
               
                result.TotalOfRegister = _dbContext.Debs.Count(x => x.AccountId == model.IdAccount && (x.DateTime >= from) && (x.DateTime <= to));
                result.ActuallyPage = page;
                result.RegisterByPage = pageToQuantity;
            }
            catch (Exception e)
            {
                result = null;
            }
            return result;
        }

        public bool Update(Debs entity)
        {
            try
            {
                var model = _dbContext.Debs.Single(x => x.Id == entity.Id);
                var account = _Account.Get(entity.AccountId);
                account.Total += entity.Money - model.Money;
                if (_Account.Update(account))
                {
                    model.Money = entity.Money;
                    model.Description = entity.Description;
                    _dbContext.Entry(model).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return true;
                }
                    return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
