using BusinessObject.Entity;
using SystemRepository.Interface;
using SystemService.Interface;

namespace SystemService.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }
    }
}