using Model.OthersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModels
{
    //modelo para paginacion
    public class PaginationViewModel<TEntity> : PageModel where TEntity : class 
    {
        public List<TEntity> Entities { get; set; }
    }
}
