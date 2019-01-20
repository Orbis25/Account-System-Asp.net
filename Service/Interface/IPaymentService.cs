using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IPaymentService
    {
        Task<IEnumerable<Payment>> GetAll(int debId);
        Task<bool>  Add(Payment entity);
        Task<bool> Delete(int id);
        Task<bool> Update(Payment entity);
        Task<Payment> GetById(int id);
    }
}
