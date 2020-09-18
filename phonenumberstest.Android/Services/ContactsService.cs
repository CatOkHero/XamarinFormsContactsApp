using System.Collections.Generic;
using Android.Content;
using Android.Database;
using Android.Provider;
using phonenumberstest.Droid.Services;
using phonenumberstest.Models;
using phonenumberstest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactsService))]
namespace phonenumberstest.Droid.Services
{
    public class ContactsService : IContactsService
    {
        public IReadOnlyList<ContactModel> GetContacts()
        {
            var uri = ContactsContract.CommonDataKinds.Phone.ContentUri;
            string[] projection = {
                ContactsContract.Contacts.InterfaceConsts.DisplayNameAlternative,
                ContactsContract.Contacts.InterfaceConsts.DisplayNamePrimary,
                ContactsContract.Contacts.InterfaceConsts.Id,
                ContactsContract.CommonDataKinds.Phone.Number
            };

            var loader = new CursorLoader(Xamarin.Essentials.Platform.CurrentActivity, uri, projection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            var contactList = new List<ContactModel>();
            if (cursor.MoveToFirst())
            {
                do
                {
                    var number = cursor.GetString(cursor.GetColumnIndex(projection[3]));

                    contactList.Add(new ContactModel
                    {
                        Id = cursor.GetString(cursor.GetColumnIndex(projection[2])),
                        Number = number,
                        Name = cursor.GetString(cursor.GetColumnIndex(projection[1]))
                    });
                } while (cursor.MoveToNext());
            }

            return contactList.AsReadOnly();
        }
    }
}
