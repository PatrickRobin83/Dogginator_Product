using Caliburn.Micro;
using de.rietrob.dogginator_product.dogginator.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace de.rietrob.dogginator_product.dogginator
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {

            SelectAssemblies();
            DisplayRootViewFor<ShellViewModel>();
        }
        protected override IEnumerable<Assembly> SelectAssemblies()
        {
            var assemblies = base.SelectAssemblies().ToList();
            assemblies.Add(typeof(LoginLibrary.ViewModels.LoginViewModel).Assembly);
            assemblies.Add(typeof(AppointmentLibrary.ViewModels.ManageAppointmentsViewModel).Assembly);
            assemblies.Add(typeof(ConsistedBookLibrary.ViewModels.ConsistedBookViewModel).Assembly);
            assemblies.Add(typeof(CustomerLibrary.ViewModels.ManageCustomerViewModel).Assembly);
            assemblies.Add(typeof(DogLibrary.ViewModels.ManageDogsViewModel).Assembly);
            assemblies.Add(typeof(InvoiceLibrary.ViewModels.ManageInvoicesViewModel).Assembly);
            assemblies.Add(typeof(OptionLibrary.ViewModels.OptionViewModel).Assembly);
            assemblies.Add(typeof(OverviewLibrary.ViewModels.OverViewViewModel).Assembly);
            assemblies.Add(typeof(ProductLibrary.ViewModels.ManageProductsViewModel).Assembly);
            assemblies.Add(typeof(UserLibrary.ViewModels.UserDetailsViewModel).Assembly);

            return assemblies;
        }
    }
}
