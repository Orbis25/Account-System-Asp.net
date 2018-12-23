using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IRequestService 
    {
        Task<bool> Add(Request model);
        bool Delete(int id);
        IEnumerable<Request> GetAll(int page = 1);
    }
}
