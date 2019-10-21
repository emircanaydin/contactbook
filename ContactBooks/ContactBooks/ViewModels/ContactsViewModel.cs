using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ContactBooks
{
    public class ContactsViewModel : ObservableObject
    {
        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get { return _selectedContact; }
            set { OnPropertyChanged(ref _selectedContact, value); }
        }

        private bool _isEditMode;
        public bool IsEditMode
        {
            get { return _isEditMode; }
            set
            {
                OnPropertyChanged(ref _isEditMode, value);
                OnPropertyChanged("IsDisplayMode");
            }
        }

        public bool IsDisplayMode
        {
            get { return !_isEditMode; }
        }

        private IEnumerable<Contact> _contacts; 

        public ObservableCollection<Contact> Contacts { get; private set; }

        public ICommand EditCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand BrowseImageCommand { get; private set; }

        private IContactDataService _dataService;
        private IDialogService _dialogService;

        public ContactsViewModel(IContactDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;
            _contacts = dataService.GetContacts();

            EditCommand = new RelayCommand(Edit, CanEdit);
            SaveCommand = new RelayCommand(Save, IsEdit);
            UpdateCommand = new RelayCommand(Update);
            BrowseImageCommand = new RelayCommand(BrowseImage, CanEdit);
        }

        private void BrowseImage()
        {
            var filePath = _dialogService.OpenFile("ImageFiles|*.bmp;*.jpg;*.jpeg;*.png|All files");
            SelectedContact.ImagePath = filePath;
        }

        private void Save()
        {
            _dataService.Save(_contacts);
            IsEditMode = false;
            OnPropertyChanged("SelectedContact");
        }

        private void Update()
        {
            _dataService.Save(_contacts);
            IsEditMode = false;
            OnPropertyChanged("SelectedContact");
        }

        private bool IsEdit()
        {
            return IsEditMode;
        }

        private bool CanEdit()
        {
            if (SelectedContact == null)
                return false;

            return !IsEditMode;
        }

        private void Edit()
        {
            IsEditMode = true;
        }

        public void LoadContacts(IEnumerable<Contact> contacts)
        {
            Contacts = new ObservableCollection<Contact>(contacts);
            OnPropertyChanged("Contacts");
        }
    }
}
