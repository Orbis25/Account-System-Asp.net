using AccountSystem.Models;
using Model;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            
        }
        public IEnumerable<Debs> GetAllDebByYear(DateTime model)
        {
            try
            {
                return _dbContext.Debs.Where(x => x.DateTime.Year == model.Year).ToList();
            }
            catch (Exception)
            {
                return new List<Debs>();
            }
        }

        public IEnumerable<DateTime> Years()
        {
            throw new NotImplementedException();
        }
    }
}
