using System.Collections.Generic;
using System.Linq;
using AddressBook;
using Foundation;
using phonenumberstest.iOS.Services;
using phonenumberstest.Models;
using phonenumberstest.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ContactsService))]
namespace phonenumberstest.iOS.Services
{
    public class ContactsService : NSObject, IContactsService
    {
        public IReadOnlyList<ContactModel> GetContacts()
        {
            var error = new NSError();
            var book = ABAddressBook.Create(out error);
            var people = book.GetPeople();

            return people
                .Select(item => new ContactModel
                {
                    Id = item.Id.ToString(),
                    Name = item.FirstName,
                    Surname = item.LastName,
                    Number = item.GetPhones().FirstOrDefault().Value
                })
                .ToList()
                .AsReadOnly();
        }
    }
}
