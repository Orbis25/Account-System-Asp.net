using Model.OthersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    
    public class IndexPageViewModel : PageModel
    {
        public List<Client> Clients { get; set; }
    }
}
