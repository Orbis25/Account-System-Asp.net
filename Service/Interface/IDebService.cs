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
    public interface IDebService : IRepository<Debs>
    {
        DetailPageViewModel Filter(FilterDebsViewModel model,int page);
    }
}
