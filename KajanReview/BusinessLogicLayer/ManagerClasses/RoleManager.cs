using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.ManagerClasses
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleDataAccess _roleDataAccess;

        public RoleManager(IRoleDataAccess roleDataAccess)
        {
            _roleDataAccess = roleDataAccess ?? throw new ArgumentNullException(nameof(_roleDataAccess));
        }

        public void CreateRole(Role newRole)
        {
            _roleDataAccess.CreateRole(newRole);
        }

        public Role GetRoleByID(int roleID)
        {
            return _roleDataAccess.GetRoleByID(roleID);
        }

        public List<Role> GetAllRoles()
        {
            return _roleDataAccess.GetAllRoles();
        }

        public void UpdateRole(Role role)
        {
            _roleDataAccess.UpdateRole(role);
        }

        public void DeleteRoleByID(int roleID)
        {
            _roleDataAccess.DeleteRoleByID(roleID);
        }
    }
}