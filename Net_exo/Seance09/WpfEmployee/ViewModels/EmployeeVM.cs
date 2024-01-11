using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmployeeVM
    {
        private NorthwindContext _dbContext = new NorthwindContext();
        private List<EmployeeModel> _EmployeesList;
        private List<string> _ListTitle;

        public List<EmployeeModel> EmployeesList
        {
            get { return _EmployeesList = _EmployeesList ?? LoadEmployees(); }
        }

        public List<string> ListTitle
        {
            get { return _ListTitle = _ListTitle ?? LoadTitles(); }
        }

        private List<EmployeeModel> LoadEmployees()
        {
            List<EmployeeModel> localCollection = new List<EmployeeModel>();
            foreach (var item in _dbContext.Employees)
            {
                localCollection.Add(new EmployeeModel(item));

            }

            return localCollection;
        }

        private List<string> LoadTitles()
        {
            return _dbContext.Employees.Select(e => e.TitleOfCourtesy).Distinct().ToList();
        }
    }
}
