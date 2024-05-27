using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRoleManager
    {
        public void CreateRole(Role newRole);
        public Role GetRoleByID(int roleID);
        public List<Role> GetAllRoles();
        public void UpdateRole(Role role);
        public void DeleteRoleByID(int roleID);
    }
}
