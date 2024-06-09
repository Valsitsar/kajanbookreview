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

        public async Task CreateRoleAsync(Role newRole)
        {
            await _roleDataAccess.CreateRoleAsync(newRole);
        }

        public async Task<Role> GetRoleByIDAsync(int roleID)
        {
            return await _roleDataAccess.GetRoleByIDAsync(roleID);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await _roleDataAccess.GetAllRolesAsync();
        }

        public async Task UpdateRoleAsync(Role role)
        {
            await _roleDataAccess.UpdateRoleAsync(role);
        }

        public async Task DeleteRoleByIDAsync(int roleID)
        {
            await _roleDataAccess.DeleteRoleByIDAsync(roleID);
        }
    }
}