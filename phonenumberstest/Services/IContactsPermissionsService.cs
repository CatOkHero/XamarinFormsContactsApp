using System.Threading.Tasks;

namespace phonenumberstest.Services
{
    public interface IContactsPermissionsService
    {
        Task<bool> CheckPermissions();
    }
}
