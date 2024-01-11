using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfEmployee.Models;
using System.ComponentModel;
using System.Windows.Controls;
using System.Diagnostics;

namespace WpfEmployee.ViewModels
{
    class EmployeeModel : INotifyPropertyChanged
    {

        private readonly Employee _monEmployee;

        // Property changed standard handling
        public event PropertyChangedEventHandler PropertyChanged;
        // La view s'enregistera automatiquement sur cet event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Employee MonEmployee
        {
            get { return _monEmployee; }
        }

        public EmployeeModel(Employee current)
        {
            this._monEmployee = current;
        }

        public int Id
        {
            get { return _monEmployee.EmployeeId; }
        }

        public String DisplayBirthDate
        {
            get
            {
                if (_monEmployee.BirthDate.HasValue)
                {
                    return _monEmployee.BirthDate.Value.ToShortDateString();
                }

                return "";
            }
        }

        public string TitleOfCourtesy
        {
            get { return _monEmployee.TitleOfCourtesy; }
            set
            {
                _monEmployee.TitleOfCourtesy = value;
            }
        }

        public String FullName
        {
            get
            {
                return String.Format("{0} {1}", _monEmployee.FirstName, _monEmployee.LastName).Trim();
            }
        }

        public String LastName
        {
            get { return _monEmployee.LastName; }
            set
            {
                _monEmployee.LastName = value;
                OnPropertyChanged("FullName");

            }
        }
        public String FirstName
        {
            get { return _monEmployee.FirstName; }
            set
            {
                _monEmployee.FirstName = value;

            }
        }
        public DateTime? BirthDate
        {
            get { return _monEmployee.BirthDate; }
            set
            {
                _monEmployee.BirthDate = value;

            }
        }
        public DateTime? HireDate
        {
            get { return _monEmployee.HireDate; }
            set
            {
                _monEmployee.HireDate = value;


            }
        }
    }
}
