using AccountSystem.Models;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Model;
using Model.ViewModels;
using Repository;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AccountClientService : IAccountClientService
    {
        private readonly ApplicationDbContext _dbContext;
        public AccountClientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Add(Account entity)
        {
            try
            {
                _dbContext.Accounts.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var model = _dbContext.Accounts.Single(x => x.Id == id);
                _dbContext.Accounts.Remove(model);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool FindById(int id)
        {
            try
            {
                var model = _dbContext.Accounts.Find(id);
                if (model != null)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public Account Get(int id)
        {
            var result = new Account();
            try
            {
                result = _dbContext.Accounts.Single(x => x.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public AccountPageViewModel GetAllByPage(int page = 1 )
        {
            var result = new AccountPageViewModel();
            try
            {
                int pageToQuantity = 10;
                result.Accounts = _dbContext.Accounts
                    .OrderBy(x => x.Name)
                    .Skip((page - 1) * pageToQuantity)
                    .Take(pageToQuantity)
                    .Include(x => x.Client)
                    .ToList();
                var totalOfAccount = _dbContext.Accounts.Count();
                result.ActuallyPage = page;
                result.TotalOfRegister = totalOfAccount;
                result.RegisterByPage = pageToQuantity;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public DetailPageViewModel GetWithClientAndDebs(int id , int page = 1)
        {
            var result = new DetailPageViewModel();
            try
            {
                int pageToQuantity = 10;

                var debs = _dbContext.Debs
                    .Where(x => x.AccountId == id)
                    .OrderByDescending(x => x.DateTime)
                    .Skip((page - 1) * pageToQuantity)
                    .Take(pageToQuantity).ToList();

                result.Account = _dbContext.Accounts
                           .Include(x => x.Client)
                           .Single(x => x.Id == id);

                result.Account.Debs = debs;

                var totalOfDebs = _dbContext.Debs
                    .Where(x => x.AccountId == id).Count();
                result.ActuallyPage = page;
                result.TotalOfRegister = totalOfDebs;
                result.RegisterByPage = pageToQuantity;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public bool Pay(PayViewModel model)
        {
            try
            {
                var account = _dbContext.Accounts.Single(x => x.Id == model.AccountId);
                account.Total -= model.Amount;
                Update(account);

                var deb = _dbContext.Debs.Where(x => x.AccountId == model.AccountId).ToList();

                foreach (var item in deb)
                {
                    _dbContext.Debs.Remove(item);
                }
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }

        }

        public bool PayOff(int id)
        {
            try
            {
                var account = Get(id);
                account.Total = 0;
                Update(account);
                var debs = _dbContext.Debs.Where(x => x.AccountId == account.Id).ToList();
                foreach(var item in debs)
                {
                    _dbContext.Debs.Remove(item);
                }
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Report(int id , string pathReport , string PathPdf)
        {
            try
            {

                ReportDocument report = new ReportDocument();
                report.Load(pathReport);

                ParameterFieldDefinitions cr;
                ParameterFieldDefinition cr2;
                ParameterValues cr3 = new ParameterValues();
                ParameterDiscreteValue cr4 = new ParameterDiscreteValue();
                cr4.Value = id;
                cr = report.DataDefinition.ParameterFields;
                cr2 = cr["AccountId"];
                cr3.Add(cr4);
                cr2.ApplyCurrentValues(cr3);
                ExportOptions export;
                DiskFileDestinationOptions diskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions excel = new PdfRtfWordFormatOptions();
                diskFileDestinationOptions.DiskFileName = PathPdf;
                export = report.ExportOptions;
                export.ExportDestinationType = ExportDestinationType.DiskFile;
                export.ExportFormatType = ExportFormatType.PortableDocFormat;
                export.ExportDestinationOptions = diskFileDestinationOptions;
                export.ExportFormatOptions = excel;
                report.Export();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public AccountPageViewModel Search(string parameter, int page)
        {
            var result = new AccountPageViewModel();
            try
            {
                int pageToQuantity = 10;
                result.Accounts = _dbContext.Accounts
                    .OrderBy(x => x.Name)
                    .Where(x => x.Name.Contains(parameter) || x.Client.Name.Contains(parameter))
                    .Skip((page - 1) * pageToQuantity)
                    .Take(pageToQuantity)
                    .Include(x => x.Client)
                    .ToList();

                var totalOfAccount = _dbContext.Accounts
                    .Where(x => x.Name.Contains(parameter) || x.Client.Name.Contains(parameter))
                    .Count();
                result.ActuallyPage = page;
                result.TotalOfRegister = totalOfAccount;
                result.RegisterByPage = pageToQuantity;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public bool Update(Account entity)
        {
            try
            {
                var result = _dbContext.Accounts.Single(x => x.Id == entity.Id);
                result.Name = entity.Name;
                result.Total = entity.Total;
                _dbContext.Entry(result).State = EntityState.Modified;
                _dbContext.SaveChanges();

                return true;
            }
            catch (Exception )
            {
                return false;
                throw;
            }
        }

        public bool VerifyRequestAndClient(int request, int client)
        {
            try
            {
                if (_dbContext.Accounts.FirstOrDefault(x => x.RequestId == request && x.ClientId == client) != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        IEnumerable<Account> IRepository<Account>.GetAll(int page)
        {
            throw new NotImplementedException();
        }
    }
}
