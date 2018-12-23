using Model.OthersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class AccountPageViewModel : PageModel
    {
        public IEnumerable<Account> Accounts { get; set; }
    }
}
