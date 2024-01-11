using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using examJanvier23.Models;
using WpfApplication1.ViewModels;
using Castle.Components.DictionaryAdapter;

namespace examJanvier23.ViewModels
{
    internal class NorthWindVM
    {
        // state
        NorthwindContext _dbContext = new NorthwindContext();

        ObservableCollection<ProductModel> _listProducts;
        ObservableCollection<CountrySalesModel> _listCountrySales;
        ProductModel _selectedProduct;

        DelegateCommand _giveUpProduct;

        // state getter / setter
        public ObservableCollection<ProductModel> ListProducts
        {
            get { return _listProducts = loadProducts(); }
        }

        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set { _selectedProduct = value; }
        }

        public ObservableCollection<CountrySalesModel> ListCountrySales
        {
            get { return _listCountrySales = LoadCountrySales(); }
        }

        // state load methods
        private ObservableCollection<ProductModel> loadProducts()
        {
            ObservableCollection<ProductModel> productModels = new ObservableCollection<ProductModel>();
            List<Product> products = _dbContext.Products.Select(p => p).Where(p => p.Discontinued == false).ToList();

            foreach (Product product in products)
            {
                productModels.Add(new ProductModel(product));
            }

            return productModels;
        }

        private ObservableCollection<CountrySalesModel> LoadCountrySales()
        {
            ObservableCollection<CountrySalesModel> countrySalesModels = new ObservableCollection<CountrySalesModel>();
            List<string> countryNames = _dbContext.Suppliers.Select(s => s.Country).Distinct().ToList();
            List<CountrySalesModel> temp = new List<CountrySalesModel>();

            foreach (string countryName in countryNames)
            {
                int sales = _dbContext.Products.Select(p => p).Where(p => p.Supplier == null ? false : p.Supplier.Country == countryName).Count();
                temp.Add(new CountrySalesModel(countryName, sales));
            }

            temp = temp.OrderByDescending(a => a.ProductsSalesCount).ToList();

            foreach (CountrySalesModel item in temp)
            {
                countrySalesModels.Add(item); 
            }

            return countrySalesModels;
        }

        // commands

        public DelegateCommand GiveUpProduct
        {
            get { return _giveUpProduct = _giveUpProduct ?? new DelegateCommand(InitGiveUpProduct); }
        }

        // commands init methods

        private void InitGiveUpProduct()
        {
            SelectedProduct.Discontinued = true;
            _listProducts.Remove(SelectedProduct);
        }
    }
}
