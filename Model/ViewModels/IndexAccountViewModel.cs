using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class IndexAccountViewModel
    {
        public List<Client> Clients { get; set; }
        public List<Request> Requests { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
