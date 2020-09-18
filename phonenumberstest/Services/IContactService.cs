using System.Collections.Generic;
using phonenumberstest.Models;

namespace phonenumberstest.Services
{
    public interface IContactsService
    {
        IReadOnlyList<ContactModel> GetContacts();
    }
}
