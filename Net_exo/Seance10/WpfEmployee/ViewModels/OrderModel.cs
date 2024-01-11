using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfEmployee.Models;

namespace WpfEmployee.ViewModels
{
    class OrderModel
    {
        private readonly Order _order;
        private decimal total = 0;

        public Order Order
        {
            get { return _order; }
        }

        public OrderModel(Order current, decimal total)
        {
            this._order = current;
            this.total = total;
        }

        public int Id
        {
            get { return _order.OrderId; }
        }

        public String OrderDate
        {
            get
            {
                if (_order.OrderDate.HasValue)
                {
                    return _order.OrderDate.Value.ToShortDateString();
                }

                return "";
            }
        }

        public String OrderTotal
        {
            get
            {
                return total.ToString();

            }
        }
    }
}
