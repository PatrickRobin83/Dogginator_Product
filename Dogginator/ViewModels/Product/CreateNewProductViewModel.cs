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
        private string _shortdescription = "";
        private string _longdescription = "";
        private string _price = "";
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
                NotifyOfPropertyChange(() => CanCreateItem);
            }
        }
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

        public void CreateItem()
        {
            ProductModel product = new ProductModel();
            product.ItemNumber = ItemNumber;
            product.Shortdescription = ShortDescription;
            product.Longdescription = LongDescription;
            product.Price = Price + "€";
            product.Active = IsActive;
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
