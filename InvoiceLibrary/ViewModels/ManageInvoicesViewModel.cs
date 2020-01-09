/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ManageInvoicesViewModel.cs
 *   Date			:   17.07.2019 23:59:25
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;

namespace de.rietrob.dogginator_product.InvoiceLibrary.ViewModels
{
    public class ManageInvoicesViewModel : Conductor<object>.Collection.AllActive
    {
        #region Fields

        private string _invoiceSearchText;
        private bool _showAlsoInactive;
        private BindableCollection<InvoiceModel> _availableInvoices;
        private InvoiceModel _selectedInvoice;
        private string _billingNumber;
        private CustomerModel _customer;
        private double _billTotal;
        private bool _isBilled;


        #endregion

        #region Properties
        public string InvoiceSearchText
        {
            get => _invoiceSearchText;
            set => _invoiceSearchText = value;
        }

        public bool ShowAlsoInactive
        {
            get => _showAlsoInactive;
            set => _showAlsoInactive = value;
        }

        public BindableCollection<InvoiceModel> AvailableInvoices
        {
            get => _availableInvoices;
            set => _availableInvoices = value;
        }

        public InvoiceModel SelectedInvoice
        {
            get => _selectedInvoice;
            set => _selectedInvoice = value;
        }

        public string BillingNumber
        {
            get => _billingNumber;
            set => _billingNumber = value;
        }

        public double BillTotal
        {
            get => _billTotal;
            set => _billTotal = value;
        }

        public bool IsBilled
        {
            get => _isBilled;
            set => _isBilled = value;
        }

        public CustomerModel Customer
        {
            get => _customer;
            set => _customer = value;
        }

        #endregion

        #region Contstructor

        #endregion

        #region Methods

        public void AddInvoice()
        {
            //Change View To CreateNewInvoiceView
            
        }

        public void EditInvoice()
        {
            //Change View to InvoiceDetailsView
        }

        public void DeleteInvoice()
        {
            //Set Selected Invoice to IsInvoiceActive = false;
        }
        #endregion


    }
}
