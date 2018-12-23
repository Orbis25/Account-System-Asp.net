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
   public interface IAccountClientService : IRepository<Account>
    {
        AccountPageViewModel GetAllByPage(int page = 1);
        bool FindById(int id);
        AccountPageViewModel Search(string parameter, int page);
        DetailPageViewModel GetWithClientAndDebs(int id , int page = 1);
        bool Pay(PayViewModel model);
        bool PayOff(int id);
        bool Report(int id , string pathReport , string PathPdf);
         
    }
}
