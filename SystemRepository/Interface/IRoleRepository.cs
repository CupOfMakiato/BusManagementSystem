using BusinessObject.Entity;

namespace SystemRepository.Interface
{
    public interface IRoleRepository
    {
        List<Role> GetAllRoles();
    }
}