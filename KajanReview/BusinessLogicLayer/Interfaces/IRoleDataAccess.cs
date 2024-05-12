using BusinessLogicLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IRoleDataAccess
    {
        public void CreateRole(Role newRole);
        public Role GetRoleByID(int roleID);
        public List<Role> GetAllRoles();
        public void UpdateRole(Role role);
        public void DeleteRoleByID(int roleID);
    }
}
