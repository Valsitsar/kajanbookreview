using BusinessLogicLayer.Entities;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRoleManager
    {
        public Task CreateRoleAsync(Role newRole);
        public Task<Role> GetRoleByIDAsync(int roleID);
        public Task<List<Role>> GetAllRolesAsync();
        public Task UpdateRoleAsync(Role role);
        public Task DeleteRoleByIDAsync(int roleID);
    }
}
