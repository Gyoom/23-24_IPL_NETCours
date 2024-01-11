using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class EmployeeVM : INotifyPropertyChanged
    {
        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
            }
        }

        // state

        private NorthwindContext _dbContext = new NorthwindContext();

        private ObservableCollection<EmployeeModel> _EmployeesList;
        private ObservableCollection<OrderModel> _OrderList;
        private List<string> _ListTitle;

        private EmployeeModel _selectedEmployee;

        private DelegateCommand _addEmployee;
        private DelegateCommand _removeEmployee;
        private DelegateCommand _saveEmployee;

        // state getter / setter

        public ObservableCollection<EmployeeModel> EmployeesList
        {
            get { return _EmployeesList = _EmployeesList ?? LoadEmployees(); }
        }

        public ObservableCollection<OrderModel> OrdersList
        {
            get
            {
                if (SelectedEmployee != null)
                {
                    _OrderList = LoadOrders();
                }

                return _OrderList;

            }
        }

        public List<string> ListTitle
        {
            get { return _ListTitle = _ListTitle ?? LoadTitles(); }
        }

        public EmployeeModel SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("OrdersList"); }
        }

        // commands statement

        public DelegateCommand AddEmployee
        {
            get { return _addEmployee = _addEmployee ?? new DelegateCommand(InitAddEmployee); }
        }

        public DelegateCommand RemoveEmployee
        {
            get { return _removeEmployee = _removeEmployee ?? new DelegateCommand(InitRemoveEmployee); }
        }

        public DelegateCommand SaveEmployee
        {
            get { return _saveEmployee = _saveEmployee ?? new DelegateCommand(InitSaveEmployee); }
        }

        // load state methods

        private ObservableCollection<EmployeeModel> LoadEmployees()
        {
            ObservableCollection<EmployeeModel> localCollection = new ObservableCollection<EmployeeModel>();
            foreach (var item in _dbContext.Employees)
            {
                localCollection.Add(new EmployeeModel(item));
            }

            return localCollection;
        }

        private ObservableCollection<OrderModel> LoadOrders()
        {

            ObservableCollection<OrderModel> localCollection = new ObservableCollection<OrderModel>();

            if (SelectedEmployee == null)
                return localCollection;
            else
            {

                List<Order> orders = _dbContext.Orders.Select(o => o).Where(o => o.EmployeeId == SelectedEmployee.Id).OrderByDescending(o => o.OrderDate).Take(3).ToList();
                foreach (Order order in orders)
                {
                    decimal total = _dbContext.OrderDetails.Where(od => od.OrderId == order.OrderId).Sum(od => od.UnitPrice);
                    localCollection.Add(new OrderModel(order, total));
                }
            }

            return localCollection;
        }

        private List<string> LoadTitles()
        {
            return _dbContext.Employees.Select(e => e.TitleOfCourtesy).Distinct().ToList();
        }

        // commands init Methods

        private void InitAddEmployee()
        {
            Employee employeeGlobal = new Employee();
            EmployeeModel employeeModel = new EmployeeModel(employeeGlobal);
            EmployeesList.Add(employeeModel);
            SelectedEmployee = employeeModel;
        }

        private void InitRemoveEmployee()
        {
            _EmployeesList.Remove(SelectedEmployee);
        }

        private void InitSaveEmployee()
        {
            Employee verif = _dbContext.Employees.Where(e => e.EmployeeId == SelectedEmployee.MonEmployee.EmployeeId).SingleOrDefault();
            if (verif == null)
            {
                _dbContext.Employees.Add(SelectedEmployee.MonEmployee);
            }

            _dbContext.SaveChanges();
            MessageBox.Show("Enregistrement en base de données fait");
        }
    }
}
