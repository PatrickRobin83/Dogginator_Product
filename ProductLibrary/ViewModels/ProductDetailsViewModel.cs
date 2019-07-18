/**
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ProductsDetailsViewModel.cs
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
using System;

namespace de.rietrob.dogginator_product.ProductLibrary.ViewModels
{
    public class ProductDetailsViewModel : Screen
    {
        #region Fields
        ProductModel _productToEdit;
        int _itemNumber;
        bool _notActive;
        string _shortdescription = "";
        string _longdescription = "";
        string _price = "";

        #endregion

        #region Properties
        public ProductModel ProductToEdit
        {
            get { return _productToEdit; }
            set
            {
                _productToEdit = value;
                NotifyOfPropertyChange(()=> ProductToEdit);
            }
        }
        public int ItemNumber {
            get { return _itemNumber; }
            set
            {
                _itemNumber = value;
                NotifyOfPropertyChange(() => ItemNumber);
            }
        }
        public bool NotActive
        {
            get { return _notActive; }
            set
            {
                _notActive = value;
                NotifyOfPropertyChange(() => NotActive);
                NotifyOfPropertyChange(() => CanEditProduct);
            }
        }

        public string ShortDescription
        {
            get { return _shortdescription; }
            set
            {
                _shortdescription = value;
                NotifyOfPropertyChange(() => ShortDescription);
                NotifyOfPropertyChange(() => CanEditProduct);
            }
        }
        public string LongDescription
        {
            get { return _longdescription; }
            set
            {
                _longdescription = value;
                NotifyOfPropertyChange(() => LongDescription);
                NotifyOfPropertyChange(() => CanEditProduct);
            }
        }
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                NotifyOfPropertyChange(() => CanEditProduct);

            }
        }

        #endregion

        #region Constructor
        public ProductDetailsViewModel(ProductModel product)
        {
            ProductToEdit = product;
            ItemNumber = product.ItemNumber;
            ShortDescription = product.Shortdescription;
            LongDescription = product.Longdescription;
            String[] priceConvert = product.Price.Split('€');
            Price = priceConvert[0];
            if (product.Active == true)
            {
                NotActive = false;
            }
            else
            {
                NotActive = true;
            }
            
        }
        #endregion

        #region Methods

        public bool CanEditProduct
        {

            get
            {
                bool output = false;
                if (NotActive)
                {
                    if (ProductToEdit.Active == true)
                    {
                        output = true;
                    }
                }
                else
                {
                    if (ProductToEdit.Active == false)
                    {
                        output = true;
                    }
                }

                if(!ProductToEdit.Shortdescription.Equals(ShortDescription))
                {
                    output = true;
                }

                if (!ProductToEdit.Longdescription.Equals(LongDescription))
                {
                    output = true;
                }
                

                if (!ProductToEdit.Price.Equals(Price + "€"))
                {
                    output = true;
                }
                

                return output;
            }
        }

        public void EditProduct()
        {
            ProductToEdit.Shortdescription = ShortDescription;
            ProductToEdit.Longdescription = LongDescription;
            ProductToEdit.Price = Price + "€";

            if (NotActive)
            {
                ProductToEdit.Active = false;
            }
            else
            {
                ProductToEdit.Active = true;
            }
            GlobalConfig.Connection.UpdateProduct(ProductToEdit);
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(ProductToEdit);
            this.TryClose();
        }

        public void Cancel()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new ProductModel());
            this.TryClose();
        }

        #endregion
    }
}
