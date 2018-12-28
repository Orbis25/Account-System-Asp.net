using Model;
using Model.ViewModels;
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
        IEnumerable<Request> GetAllByIdUser(string id);
        bool Update(Request model);
        PaginationViewModel<Request> GetAllWithPagination(int page = 1);
        PaginationViewModel<Request> Search(string parameter, int page = 1);

    }
}
