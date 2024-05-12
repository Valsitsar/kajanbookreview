using BusinessLogicLayer.Entities;
using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.EntityManagers
{
    public class RoleManager : IRoleManager
    {
        private IRoleDataAccess _roleDataAccess;

        public RoleManager(IRoleDataAccess roleDataAccess)
        {
            _roleDataAccess = roleDataAccess;
        }

        public void CreateRole(Role newRole)
        {
            try { _roleDataAccess.CreateRole(newRole); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public Role GetRoleByID(int roleID)
        {
            try { return _roleDataAccess.GetRoleByID(roleID); }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public List<Role> GetAllRoles()
        {
            try { return _roleDataAccess.GetAllRoles(); } 
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void UpdateRole(Role role)
        {
            try { _roleDataAccess.UpdateRole(role); } 
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void DeleteRoleByID(int roleID)
        {
            try { _roleDataAccess.DeleteRoleByID(roleID); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
}