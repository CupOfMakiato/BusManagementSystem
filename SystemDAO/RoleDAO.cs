using BusinessObject.Entity;

namespace SystemDAO
{
    public class RoleDAO
    {
        private static RoleDAO instance = null;

        public RoleDAO()
        {
        }

        public static RoleDAO getInstance()
        {
            if (instance == null)
            {
                instance = new RoleDAO();
            }
            return instance;
        }

        public List<Role> GetAllRoles()
        {
            var list = new List<Role>();
            try
            {
                using var context = new BusManagementSystemContext();
                list = context.Roles.ToList();
            }
            catch (Exception ex)
            {
                return list;
            }
            return list;
        }
    }
}