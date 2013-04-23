using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Util;
using Dexter.Data;
using Dexter.Model;
using Dexter.View;
using System.Windows.Input;
using System.ComponentModel;

namespace Dexter.ViewModel
{
    public class AddressBookViewModel : INotifyPropertyChanged
    {
        private ContactRepo _db;

        public AddressBookViewModel(ContactRepo db)
        {
            _db = db;
        }

        public List<Contact> AllContacts 
        {
            get
            {
                return _db.All().ToList();
            }
        }
        public Contact EditContact { get; set; }

        private RelayCommand _add;
        public ICommand Add
        {
            get
            {
                return _add ?? (_add = new RelayCommand(p => addContact()));
            }
        }
        private void addContact() 
        {
            var contact = new Contact();
            var dialog = new ContactView { DataContext = contact };
            dialog.ShowDialog();
            _db.Create(contact);
            OnPropertyChanged("AllContacts");
        }

        private RelayCommand _update;
        public ICommand Update
        {
            get
            {
                return _update ?? (_update = new RelayCommand(p => updateContact(),
                    p => EditContact != null));
            }
        }
        private void updateContact()
        {
            var dialog = new ContactView { DataContext = EditContact };
            dialog.ShowDialog();
            _db.Update(EditContact);
            OnPropertyChanged("AllContacts");
        }

        private RelayCommand _delete;
        public ICommand Delete
        {
            get
            {
                return _delete ?? (_delete = new RelayCommand(p => deleteContact(),
                    p => EditContact != null));
            }
        }
        private void deleteContact()
        {
            _db.Destory(EditContact);
            OnPropertyChanged("AllContacts");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
