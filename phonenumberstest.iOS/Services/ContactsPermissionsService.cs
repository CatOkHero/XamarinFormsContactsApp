using System.Threading.Tasks;
using AddressBook;
using Foundation;
using phonenumberstest.iOS.Services;
using phonenumberstest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactsPermissionsService))]
namespace phonenumberstest.iOS.Services
{
    public class ContactsPermissionsService : IContactsPermissionsService
    {
        public Task<bool> CheckPermissions()
        {
            var error = new NSError();
            var abAddressBook = ABAddressBook.Create(out error);
            var taskCompletitionSource = new TaskCompletionSource<bool>();
            abAddressBook.RequestAccess((result, errors) =>
            {
                taskCompletitionSource.SetResult(result);
            });

            return taskCompletitionSource.Task;
        }
    }
}
