using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using phonenumberstest.DataServices;
using phonenumberstest.Models;
using phonenumberstest.Services;
using Xamarin.Forms;

namespace phonenumberstest.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IContactsService contactsService;
        private readonly IContactsPermissionsService contactsPermissionsService;
        private readonly ISQLRepository iSQLRepository;
        private IReadOnlyList<ContactModel> contacts;
        private string synchronizationText;
        private bool synchronizing;

        public MainPageViewModel()
        {
            SynchronizationText = "Not Synchronized";
            Synchronizing = true;
            contactsService = DependencyService.Get<IContactsService>();
            contactsPermissionsService = DependencyService.Get<IContactsPermissionsService>();
            iSQLRepository = new SQLRepository(Constants.DatabasePath);

            GetRequest = new Command(async () => await ExecuteGetContacts());
        }

        public ICommand GetRequest { get; set; }

        public IReadOnlyList<ContactModel> Contacts
        {
            get
            {
                return contacts;
            }
            set
            {
                contacts = value;
                OnPropertyChanged(nameof(Contacts));
            }
        }

        public string SynchronizationText
        {
            get
            {
                return synchronizationText;
            }
            set
            {
                synchronizationText = value;
                OnPropertyChanged(nameof(SynchronizationText));
            }
        }

        public bool Synchronizing
        {
            get
            {
                return synchronizing;
            }
            set
            {
                synchronizing = value;
                OnPropertyChanged(nameof(Synchronizing));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Initialize()
        {
            await iSQLRepository.CreateTableAsync<ContactModel>();
            var contactsFromDb = await iSQLRepository.GetAll<ContactModel>();
            if(contactsFromDb != null)
            {
                Contacts = contactsFromDb;
            }
        }

        private async Task ExecuteGetContacts()
        {
            Synchronizing = false;
            SynchronizationText = "Synchronization In Progress";
            var permission = await contactsPermissionsService.CheckPermissions();
            if (!permission)
            {
                SynchronizationText = "Synchronization Failed";
                Synchronizing = true;
                return;
            }

            var contacts = contactsService.GetContacts();
            await iSQLRepository.SaveAll<ContactModel>(contacts);
            Contacts = contacts;
            SynchronizationText = "Synchronized";
            Synchronizing = true;
        }
    }
}
