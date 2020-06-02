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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using Caliburn.Micro;
using de.rietrob.dogginator_product.DogginatorLibrary;
using de.rietrob.dogginator_product.DogginatorLibrary.Models;

namespace de.rietrob.dogginator_product.InvoiceLibrary.ViewModels
{
    public class ManageInvoicesViewModel : Conductor<object>.Collection.AllActive
    {
        #region Fields

        private string _invoiceSearchText;
        private bool _showAlsoInactive;
        private BindableCollection<InvoiceModel> _availableInvoices = new BindableCollection<InvoiceModel>();
        private InvoiceModel _selectedInvoice;
        private string _billingNumber;
        private CustomerModel _customer;
        private double _billTotal;
        private bool _isBilled;


        #endregion

        #region Properties

        /// <summary>
        /// Value from TextBox SearchText
        /// </summary>
        public string InvoiceSearchText
        {
            get { return _invoiceSearchText;  }

            set
            {
                _invoiceSearchText = value;
                NotifyOfPropertyChange(() => InvoiceSearchText);
                AvailableInvoices = getInvoices();
            }
        }

        /// <summary>
        /// Value of CheckBox Show also Inactive
        /// </summary>
        public bool ShowAlsoInactive
        {
            get => _showAlsoInactive;
            set => _showAlsoInactive = value;
        }

        /// <summary>
        /// List of all Invoices shown in the DataGridView
        /// </summary>
        public BindableCollection<InvoiceModel> AvailableInvoices
        {
            get { return _availableInvoices; }

            set
            {
                _availableInvoices = value;
                NotifyOfPropertyChange(() => AvailableInvoices);
            }
        }

        /// <summary>
        /// Selected Invoice on the DataGridView
        /// </summary>
        public InvoiceModel SelectedInvoice
        {
            get => _selectedInvoice;
            set => _selectedInvoice = value;
        }

        /// <summary>
        /// From each Item in the DataGrid the string Billingnumber
        /// </summary>
        public string BillingNumber
        {
            get => _billingNumber;
            set => _billingNumber = value;
        }

        /// <summary>
        /// From each Item in the DataGrid the double Bill Total
        /// </summary>
        public double BillTotal
        {
            get => _billTotal;
            set => _billTotal = value;
        }

        /// <summary>
        /// For each Item in the DataGFrid the Bool Is Billed
        /// </summary>
        public bool IsBilled
        {
            get => _isBilled;
            set => _isBilled = value;
        }

        // TODO: Write a comment for the Customer Model Property
        public CustomerModel Customer
        {
            get => _customer;
            set => _customer = value;
        }

        #endregion

        #region Contstructor

        public ManageInvoicesViewModel()
        {
            AvailableInvoices = new BindableCollection<InvoiceModel>(GlobalConfig.Connection.Get_InvoicesActiveAndInactive());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create new Invoice. Opens the CreateNewInvoiceView
        /// </summary>
        public void AddInvoice()
        {
            // TODO: Change View To CreateNewInvoiceView
            
        }

        /// <summary>
        /// Opens the InvoiceDetailsView to edit the Invoice
        /// </summary>
        public void EditInvoice()
        {
            // TODO: Change View to InvoiceDetailsView
        }

        /// <summary>
        /// Deletes the selected Invoices from the Data store and from the DataGrid
        /// </summary>
        public void DeleteInvoice()
        {
            // TODO: Set Selected Invoice to IsInvoiceActive = false;
        }
        // ToDO: Write a Comment for this Method
        public BindableCollection<InvoiceModel> getInvoices()
        {
            AvailableInvoices = new BindableCollection<InvoiceModel>(GlobalConfig.Connection.Get_InvoicesActiveAndInactive());
            return AvailableInvoices;
        }

        #endregion


    }
}
