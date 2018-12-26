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
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateTime _dateTime;
        public RequestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dateTime = DateTime.Now;
        }
        public async Task<bool> Add(Request model)
        {
            try
            {
                model.CreatedAt = _dateTime;
                model.State = false;
                _dbContext.Requests.Add(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = _dbContext.Requests.Find(id);
                _dbContext.Requests.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Request> GetAll(int page = 1)
        {
            var model = new List<Request>();
            try
            {
                model = _dbContext.Requests.Include(x => x.ApplicationUser).ToList();
            }
            catch (Exception)
            {
                return null;
            }
            return model;
        }

        public IEnumerable<Request> GetAllByIdUser(string id)
        {
            var model = new List<Request>();
            try
            {
                model = _dbContext.Requests.Where(x => x.ApplicationUserId == id).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return model;
        }

        public bool Update(Request model)
        {
            try
            {
                var request = _dbContext.Requests.Find(model.Id);
                request.State = model.State;
                _dbContext.Entry(request).State = EntityState.Modified;
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
