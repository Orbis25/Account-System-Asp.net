using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IReportService
    {
        IEnumerable<Debs> GetAllDebByYear(DateTime model);
        IEnumerable<DateTime> Years();
    }
}
