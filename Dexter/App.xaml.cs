using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Dexter.Model;
using Dexter.Data;
using Dexter.View;
using Dexter.ViewModel;

namespace Dexter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var db = new ContactRepo();
            var vm = new AddressBookViewModel(db);
            var view = new AddressBook { DataContext = vm };
            view.Show();
        }
    }
}
