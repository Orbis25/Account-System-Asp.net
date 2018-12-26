using AccountSystem.Models;
using Model;
using Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountService
    {
        ApplicationUser GetUser(string id);
       bool UpdateUserName(UpdateUserNameViewModelAppUs model);
    }
}
