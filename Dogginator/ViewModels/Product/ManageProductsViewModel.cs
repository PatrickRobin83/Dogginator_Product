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
    public class ManageProductsViewModel : Conductor<object>.Collection.OneActive
    {
        #region Fields
        private string _searchText = "";
        private BindableCollection<ProductModel> _availableProducts = new BindableCollection<ProductModel>();
        private ProductModel _selectedProduct;
        private bool _productOverviewIsVisible = true;
        private bool _addProductIsVisible = false;
        private bool _productDetailsIsVisible = false;
        private Screen _activeProductDetailsView;
        private Screen _activeAddProductView;
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

        public bool ProductOverviewIsVisible
        {
            get { return _productOverviewIsVisible; }
            set
            {
                _productOverviewIsVisible = value;
                NotifyOfPropertyChange(() => ProductOverviewIsVisible);
            }
        }

        public bool AddProductIsVisible
        {
            get { return _addProductIsVisible; }
            set
            {
                _addProductIsVisible = value;
                NotifyOfPropertyChange(() => AddProductIsVisible);
            }
        }

        public bool ProductDetailsIsVisible
        {
            get { return _productDetailsIsVisible; }
            set
            {
                _productDetailsIsVisible = value;
                NotifyOfPropertyChange(() => ProductOverviewIsVisible);
            }
        }

        public Screen ActiveProductDetailsView
        {
            get { return _activeProductDetailsView; }
            set
            {
                _activeProductDetailsView = value;
                NotifyOfPropertyChange(() => ActiveProductDetailsView);
            }
        }
        public Screen ActiveAddProductView
        {
            get { return _activeAddProductView; }
            set
            {
                _activeAddProductView = value;
                NotifyOfPropertyChange(() => ActiveAddProductView);
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
            ActiveProductDetailsView = new ProductDetailsViewModel();
            Items.Add(ActiveProductDetailsView);
            ProductOverviewIsVisible = false;
            AddProductIsVisible = false;
            ProductDetailsIsVisible = true;

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
