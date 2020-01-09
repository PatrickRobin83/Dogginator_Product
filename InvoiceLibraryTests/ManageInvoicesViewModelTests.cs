/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   ManageInvoicesViewModelTests.cs
 *   Date			:   2020-01-09 16:09:13
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System.Runtime.CompilerServices;
using de.rietrob.dogginator_product.InvoiceLibrary.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InvoiceLibraryTests
{
 [TestClass]
    public class ManageInvoicesViewModelTests
    {
        private ManageInvoicesViewModel _testTarget;
        [TestInitialize]
        public void TestInitialize()
        {
            _testTarget = new ManageInvoicesViewModel();
        }
        [TestMethod]
        public void IsAddVoiceOnlyCalledOnce()
        {
            var counter = 0;
            _testTarget.AddInvoice();
            counter++;

            Assert.AreEqual(1,counter);
        }
        [TestMethod]
        public void IsEditVoiceOnlyCalledOnce()
        {
            var counter = 0;
            _testTarget.EditInvoice();
            counter++;

            Assert.AreEqual(1, counter);
        }
        [TestMethod]
        public void IsDeleteVoiceOnlyCalledOnce()
        {
            var counter = 0;
            _testTarget.DeleteInvoice();
            counter++;

            Assert.AreEqual(1, counter);
        }
    }
}