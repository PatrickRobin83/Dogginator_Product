/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   CreateNewProductViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using Caliburn.Micro;
using System;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;
using de.rietrob.dogginator_product.DogginatorLibrary;

namespace de.rietrob.dogginator_product.ProductLibrary.ViewModels
{
    public class CreateNewProductViewModel : Screen
    {
        #region Fields
        private bool _isActiveItem = true;
        private int _itemNumber;
        private string _shortdescription = "";
        private string _longdescription = "";
        private string _price = "";

        #endregion

        #region Properties

        /// <summary>
        /// Value of the ItemNumber TextBox
        /// </summary>
        public int ItemNumber
        {
            get { return _itemNumber; }
            set
            {
                _itemNumber = value;
                NotifyOfPropertyChange(() => ItemNumber);
                NotifyOfPropertyChange(() => CanCreateItem);
            }
        }

        /// <summary>
        /// Value of the Shortdescription TextBox
        /// </summary>
        public string ShortDescription
        {
            get { return _shortdescription; }
            set
            {
                _shortdescription = value;
                NotifyOfPropertyChange(() => ShortDescription);
                NotifyOfPropertyChange(() => CanCreateItem);
            }
        }

        /// <summary>
        /// Value of the Longdescription TextBox
        /// </summary>
        public string LongDescription
        {
            get { return _longdescription; }
            set
            {
                _longdescription = value;
                NotifyOfPropertyChange(() => LongDescription);
                NotifyOfPropertyChange(() => CanCreateItem);
            }
        }

        /// <summary>
        /// Value of the Price Textbox
        /// </summary>
        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                NotifyOfPropertyChange(() => CanCreateItem);
            }
        }
        /// <summary>
        /// Value of the IsActive Checkbox. If checked the Item is active and the value is true.
        /// </summary>
        public bool IsActiveItem
        {
            get { return _isActiveItem; }
            set
            {
                _isActiveItem = value;
                NotifyOfPropertyChange(() => IsActiveItem);
            }
        }
        #endregion

        #region Constructor
        public CreateNewProductViewModel()
        {
            EventAggregationProvider.DogginatorAggregator.Subscribe(this);

        }
        #endregion

        #region Methods

        /// <summary>
        /// If true the CreateItem Button is activated
        /// </summary>
        public bool CanCreateItem
        {
            get
            {
                bool canSave = true;
                if (ShortDescription == null || LongDescription == null || Price == null)
                {

                }
                else
                {
                    if (ShortDescription.Length > 0 && LongDescription.Length > 0 && Price.Length > 0)
                    {
                        canSave = true;
                    }
                    else
                    {
                        canSave = false;
                    }
                }

                
                return canSave;
            }

        }

        /// <summary>
        /// Creates a Product Model
        /// </summary>
        public void CreateItem()
        {
            ProductModel product = new ProductModel();
            product.ItemNumber = ItemNumber;
            product.Shortdescription = ShortDescription;
            product.Longdescription = LongDescription;
            product.Price = Price + "€";
            product.Active = IsActiveItem;
            GlobalConfig.Connection.AddProductToDataStore(product);
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(product);
        }

        /// <summary>
        /// Cancels the Item creation and returns to the ProductOverView
        /// </summary>
        public void CancelCreation()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new ProductModel());
            this.TryClose();
        }

       
        #endregion
    }
}
