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
    public class RequestService : IRequestService
    {
        private readonly ApplicationDbContext _dbContext;
        public RequestService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(Request model)
        {
            try
            {
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
            throw new NotImplementedException();
        }
    }
}
