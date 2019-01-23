using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IHomeService
    {
        int GetAllMenbers();
        decimal GetAllDebs();
        int GetAllAccounts();

        int GetAllMyAccount(string id);
        int GetAllMyDebs(string id);
        int GetAllMyRequest(string id);
    }
}
