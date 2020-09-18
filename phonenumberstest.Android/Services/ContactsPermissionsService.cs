using System.Threading.Tasks;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using phonenumberstest.Droid.Services;
using phonenumberstest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactsPermissionsService))]
namespace phonenumberstest.Droid.Services
{
    public class ContactsPermissionsService : IContactsPermissionsService
    {
        private static TaskCompletionSource<bool> task;

        public Task<bool> CheckPermissions()
        {
            task = new TaskCompletionSource<bool>();
            var currentPermission = ContextCompat.CheckSelfPermission(Xamarin.Essentials.Platform.AppContext, Manifest.Permission.ReadContacts);
            if (currentPermission == Permission.Denied)
            {
                ActivityCompat.RequestPermissions(
                        Xamarin.Essentials.Platform.CurrentActivity,
                        new string[] { Manifest.Permission.ReadContacts },
                        1);

                return task.Task;
            }
            else
            {
                return Task.FromResult(true);
            }
        }

        public static void OnRequestCompleted(bool result)
        {
            task.TrySetResult(result);
        }
    }
}
