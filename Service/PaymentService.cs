using AccountSystem.Models;
using Model;
using Service.Interface;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAccountClientService _account;
        public PaymentService(ApplicationDbContext dbContext , IAccountClientService account)
        {
            _dbContext = dbContext;
            _account = account;
        }
        public  async  Task<bool> Add(Payment entity)
        {
            try
            {
                var account = await _dbContext.Accounts.SingleAsync(x => x.Id == entity.AccountId);
                if (account.Total >= entity.Quantity )
                {
                    account.Total -= entity.Quantity;
                    _account.Update(account);
                    _dbContext.Payments.Add(entity);
                     await _dbContext.SaveChangesAsync();
                    return true;
                }
                
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = await _dbContext.Payments.Include(x => x.Account).SingleAsync(x => x.Id == id);
                if (model != null)
                {
                   Account account = _account.Get(model.Account.Id);
                    if (account.Total < model.Quantity) return false;
                    account.Total += model.Quantity;
                    _account.Update(account);
                    _dbContext.Payments.Remove(model);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Payment> GetById(int id)
        {
            try
            {
                return await _dbContext.Payments.SingleAsync(x => x.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Update(Payment entity)
        {
            try
            {
                var model = await _dbContext.Payments.Include(x => x.Account).SingleAsync(x => x.Id == entity.Id);
                Account account = _account.Get(model.Account.Id);
                account.Total -= (entity.Quantity - model.Quantity);
                if (_account.Update(account))
                {
                    model.Quantity = entity.Quantity;
                    _dbContext.Entry(model).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
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
