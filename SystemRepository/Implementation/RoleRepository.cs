using BusinessObject.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
