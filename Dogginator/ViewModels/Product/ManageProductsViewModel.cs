using Caliburn.Micro;
using DogginatorLibrary;
using DogginatorLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class ManageProductsViewModel : Conductor<object>
    {
        #region Fields
        private string _searchText = "";
        private BindableCollection<ProductModel> _availableProducts= new BindableCollection<ProductModel>();
        private ProductModel _selectedProduct;
        #endregion

        #region Properties
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                NotifyOfPropertyChange(() => SearchText);
            }
        }
        public BindableCollection<ProductModel> AvailableProducts
        {
            get { return _availableProducts; }
            set
            {
                _availableProducts = value;
                NotifyOfPropertyChange(() => AvailableProducts);
            }
        }
        public ProductModel SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
                NotifyOfPropertyChange(() => CanDeleteProduct);
                NotifyOfPropertyChange(() => CanEditProduct);
            }
        }
        public bool CanEditProduct
        {
            get
            {
                bool output = false;
                if (SelectedProduct != null)
                {
                    output = true;
                }
                return output;
            }
        }
        public bool CanDeleteProduct
        {
            get
            {
                bool output = false;
                if (SelectedProduct != null)
                {
                    output = true;
                }
                return output;
            }
        }
        #endregion

        #region Contstructor
        public ManageProductsViewModel()
        {
            AvailableProducts = new BindableCollection<ProductModel>(GlobalConfig.Connection.GetAllProducts());
        }
        #endregion

        #region Methods
       

        public void EditProduct()
        {
            Console.WriteLine("Edit Product pressed");
        }

        public void AddProduct()
        {
            Console.WriteLine("Add Product pressed");
        }

       
        public void DeleteProduct()
        {
            Console.WriteLine("Delete Product pressed");
        }

        #endregion
    }
}
