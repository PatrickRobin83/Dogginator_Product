/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ManageProductsViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;


namespace de.rietrob.dogginator_product.ProductLibrary.ViewModels
{
    public class ManageProductsViewModel : Conductor<object>.Collection.OneActive, IHandle<ProductModel>
    {
        #region Fields
        private string _productSearchText = "";
        private BindableCollection<ProductModel> _availableProducts = new BindableCollection<ProductModel>();
        private ProductModel _selectedProduct;
        private bool _productOverviewIsVisible = true;
        private bool _addProductIsVisible = false;
        private bool _productDetailsIsVisible = false;
        private bool _showAlsoInactive = false;
        private Screen _activeProductDetailsView;
        private Screen _activeAddProductView;
        
        #endregion

        #region Properties

        /// <summary>
        /// Value of the Search TextBox
        /// </summary>
        public string ProductSearchText
        {
            get { return _productSearchText; }
            set
            {
                _productSearchText = value;
                AvailableProducts = getProducts();
                NotifyOfPropertyChange(() => ProductSearchText);

            }
        }

        /// <summary>
        /// List of all Products in the Data store shown in a DataGridView
        /// </summary>
        public BindableCollection<ProductModel> AvailableProducts
        {
            get { return _availableProducts; }
            set
            {
                _availableProducts = value;
                NotifyOfPropertyChange(() => AvailableProducts);
            }
        }

        /// <summary>
        /// Selected Product in the DataGridView
        /// </summary>
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

        /// <summary>
        /// If true the Edit Product Button is activated.
        /// </summary>
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

        /// <summary>
        /// If true the Delete Product Button is activated
        /// </summary>
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

        /// <summary>
        /// If true the ManageProductView is visible
        /// </summary>
        public bool ProductOverviewIsVisible
        {
            get { return _productOverviewIsVisible; }
            set
            {
                _productOverviewIsVisible = value;
                NotifyOfPropertyChange(() => ProductOverviewIsVisible);
            }
        }

        /// <summary>
        /// If true the CreateProductView is visible
        /// </summary>
        public bool AddProductIsVisible
        {
            get { return _addProductIsVisible; }
            set
            {
                _addProductIsVisible = value;
                NotifyOfPropertyChange(() => AddProductIsVisible);
            }
        }

        /// <summary>
        /// If true the ProductDetailsView is visible
        /// </summary>
        public bool ProductDetailsIsVisible
        {
            get { return _productDetailsIsVisible; }
            set
            {
                _productDetailsIsVisible = value;
                NotifyOfPropertyChange(() => ProductDetailsIsVisible);
            }
        }

        /// <summary>
        /// Represents the ProductDetailsView
        /// </summary>
        public Screen ActiveProductDetailsView
        {
            get { return _activeProductDetailsView; }
            set
            {
                _activeProductDetailsView = value;
                NotifyOfPropertyChange(() => ActiveProductDetailsView);
            }
        }

        /// <summary>
        /// Represents the CreateProductView
        /// </summary>
        public Screen ActiveAddProductView
        {
            get { return _activeAddProductView; }
            set
            {
                _activeAddProductView = value;
                NotifyOfPropertyChange(() => ActiveAddProductView);
            }
        }

        /// <summary>
        /// Value of the Show also inactive Checkbox. If true also all inactive Products will be shown in the DataGridView
        /// </summary>
        public bool ShowAlsoInactive
        {
            get { return _showAlsoInactive; }

            set
            {
                _showAlsoInactive = value;
                NotifyOfPropertyChange(() => ShowAlsoInactive);
                AvailableProducts = getProducts();
                NotifyOfPropertyChange(() => AvailableProducts);

            }
        }

        #endregion

        #region Contstructor
        public ManageProductsViewModel()
        {
            AvailableProducts = new BindableCollection<ProductModel>(GlobalConfig.Connection.GetAllProducts());
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);
        }
        #endregion

        #region Methods

        /// <summary>
        /// Loads the ProductDetailsView and fill all controls with the values of the ProductModel
        /// </summary>
        public void EditProduct()
        {
            ActiveProductDetailsView = new ProductDetailsViewModel(SelectedProduct);
            Items.Add(ActiveProductDetailsView);
            ProductOverviewIsVisible = false;
            AddProductIsVisible = false;
            ProductDetailsIsVisible = true;

        }

        /// <summary>
        /// Loads the CreateProductView
        /// </summary>
        public void AddProduct()
        {
            ActiveAddProductView = new CreateNewProductViewModel();
            Items.Add(ActiveAddProductView);
            ProductOverviewIsVisible = false;
            AddProductIsVisible = true;
            ProductOverviewIsVisible = false;
        }

        /// <summary>
        /// Deletes the Product and refreshes the DataGridView
        /// </summary>
        public void DeleteProduct()
        {
            GlobalConfig.Connection.DeleteProductFromDataStore(SelectedProduct);
            AvailableProducts = new BindableCollection<ProductModel>(GlobalConfig.Connection.GetAllProducts());
        }

        /// <summary>
        /// Refreshes the UI after creating or editing a Product
        /// </summary>
        /// <param name="productModel"></param>
        public void Handle(ProductModel productModel)
        {
            if (productModel != null && productModel.ItemNumber > 0)
            {
                AvailableProducts = new BindableCollection<ProductModel>(GlobalConfig.Connection.GetAllProducts());
            }
            ProductOverviewIsVisible = true;
            ProductDetailsIsVisible = false;
            AddProductIsVisible = false;
            SelectedProduct = null;
            AvailableProducts = new BindableCollection<ProductModel>(GlobalConfig.Connection.GetAllProducts());
            NotifyOfPropertyChange(() => AvailableProducts);
            ShowAlsoInactive = false;
            NotifyOfPropertyChange(() => ShowAlsoInactive);
        }

        /// <summary>
        /// Trigger a data store query with the value of the SearchTextBox
        /// </summary>
        /// <returns>A List with Product that contains the given Search string</returns>
        private BindableCollection<ProductModel> getProducts()
        {
            AvailableProducts = new BindableCollection<ProductModel>(GlobalConfig.Connection.SearchResultProducts(ProductSearchText, ShowAlsoInactive));
            return AvailableProducts;
        }

        #endregion
    }
}
