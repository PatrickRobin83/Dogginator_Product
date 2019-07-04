using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogginatorLibrary.Models;
using DogginatorLibrary;

namespace de.rietrob.dogginator_product.dogginator.ViewModels
{
    public class CreateNewProductViewModel : Screen
    {
        #region Fields
        private bool _isActive = true;
        private int _itemNumber;
        private string _shortdescription;
        private string _longdescription;
        private string _price = "";
        private decimal _priceInDecimal;
        private DateTime _createDate;
        private DateTime _editDate;

        #endregion

        #region Properties
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
        public string ShortDescription
        {
            get { return _shortdescription; }
            set
            {
                _shortdescription = value;
                NotifyOfPropertyChange(() => ShortDescription);
            }
        }
        public string LongDescription
        {
            get { return _longdescription; }
            set
            {
                _longdescription = value;
                NotifyOfPropertyChange(() => LongDescription);
            }
        }

        public string Price
        {
            get { return _price; }
            set
            {
                _price = value;
                NotifyOfPropertyChange(() => Price);
                Decimal.TryParse(_price, out _priceInDecimal);
            }
        }

        public decimal PriceInDecimal
        {
            get { return _priceInDecimal; }
            set
            {
                _priceInDecimal = value;
                NotifyOfPropertyChange(() => PriceInDecimal);
            }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                NotifyOfPropertyChange(() => IsActive);
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
                    if (ShortDescription.Length > 0 && !ShortDescription.Equals(_shortdescription) ||
                    LongDescription.Length > 0 && !LongDescription.Equals(_longdescription) || !_price.Equals(Price) || _isActive != IsActive)
                    {
                        canSave = true;
                    }
                }

                
                return canSave;
            }

        }

        public void CreateItem()
        {
            ProductModel product = new ProductModel();
            product.ItemNumber = ItemNumber;
            product.Shortdescription = ShortDescription;
            product.Longdescription = LongDescription;
            product.Price = PriceInDecimal;
            GlobalConfig.Connection.AddProductToDatabase(product);
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(product);
        }

        public void CancelCreation()
        {
            EventAggregationProvider.DogginatorAggregator.PublishOnUIThread(new ProductModel());
            this.TryClose();
        }

       
        #endregion
    }
}
