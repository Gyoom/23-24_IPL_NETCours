using examJanvier23.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examJanvier23.ViewModels
{
    internal class ProductModel : INotifyPropertyChanged
    {
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

        private readonly Product _product;

        public ProductModel(Product product)
        {
            _product = product;
        }

        public Product Product { get { return _product; } }

        public int Id
        {
            get { return _product.ProductId; }
        }

        public string Name
        {
            get { return _product.ProductName; }
        }

        public string CategoryName
        {
            get
            {
                if (_product.Category != null)
                {
                    return _product.Category.CategoryName;
                }
                else
                {
                    return "no categorie";
                }
            }
        }

        public string ContactName
        {
            get
            {
                if (_product.Supplier != null && _product.Supplier.ContactName != null)
                {
                    return _product.Supplier.ContactName;
                }
                else
                {
                    return "pas de fournisseru";
                }
            }
        }

        public bool Discontinued
        {
            set 
            {
                _product.Discontinued = value;
            }
        }
    }
}
