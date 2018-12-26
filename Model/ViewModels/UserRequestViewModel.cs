using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    public class UserRequestViewModel
    {
        public IEnumerable<Request> Requests { get; set; }
        public Client Client { get; set; }
    }
}
