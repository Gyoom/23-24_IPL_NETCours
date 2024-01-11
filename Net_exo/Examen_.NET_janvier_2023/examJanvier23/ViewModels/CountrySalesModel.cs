using examJanvier23.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examJanvier23.ViewModels
{
    internal class CountrySalesModel
    {

        private readonly string _countryName;
        private readonly int _productsSaleCount;

        public CountrySalesModel(string countryName, int productsSalesCount)
        {
            _countryName = countryName;
            _productsSaleCount = productsSalesCount;
        }


        public string CountryName
        {
            get { return _countryName; }
        }

        public int ProductsSalesCount
        {
            get { return _productsSaleCount; }
        }
    }
}
