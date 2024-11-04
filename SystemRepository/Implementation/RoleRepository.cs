using BusinessObject.Entity;
using SystemDAO;
using SystemRepository.Interface;

namespace SystemRepository.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        public List<Role> GetAllRoles()
        {
            return RoleDAO.getInstance().GetAllRoles();
        }
    }
}