using AccountSystem.Models;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDebService _deb;
        public PaymentService(ApplicationDbContext dbContext , IDebService deb)
        {
            _dbContext = dbContext;
            _deb = deb;
        }

        public  async Task<bool> Add(Payment entity)
        {
            try
            {
                var deb = await _dbContext.Debs.SingleAsync(x => x.Id == entity.DebId);
                if (deb.Money >= entity.Quantity)
                {
                    deb.Money -= entity.Quantity;
                    _deb.Update(deb);
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
                var model = await _dbContext.Payments.Include(x => x.Deb).SingleAsync(x => x.Id == id);
                if (model != null)
                {
                    Debs deb = _deb.Get(model.Deb.Id);
                    if (deb.Money >= model.Quantity)
                    {
                        deb.Money += model.Quantity;
                        model.Deleted = Model.Enums.Deleted.yes;
                        _deb.Update(deb);
                    }
                    else
                    {
                        model.Deleted = Model.Enums.Deleted.yes;
                        _deb.Update(deb);
                    }
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

        public async Task<IEnumerable<Payment>> GetAll(int debId)
        {
            try
            {
                return await _dbContext.Payments.Where(x => x.DebId == debId).ToListAsync();
            } 
            catch (Exception)
            {
                return null;    
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

        public async Task<bool> PayAll(int debId)
        {
            try
            {
                     _dbContext.Payments.Where(x => x.DebId == debId).ToList()
                    .ForEach(x => x.Deleted = Model.Enums.Deleted.yes);
                var model = _dbContext.Debs.Find(debId);
                model.Deleted = Model.Enums.Deleted.payment;
                _deb.Update(model);
               await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(Payment entity)
        {
            try
            {
                var model = await _dbContext.Payments.Include(x => x.Deb).SingleAsync(x => x.Id == entity.Id);
                Debs deb = _deb.Get(model.Deb.Id);
                deb.Money -= (entity.Quantity - model.Quantity);
                if (_deb.Update(deb))
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
