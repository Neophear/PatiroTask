using PatiroTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace PatiroTask.Data
{
    public interface IRoleAccessData
    {
        public IEnumerable<RoleAccess> GetRoleAccesses();
        public IEnumerable<RoleAccess> GetRoleAccesses(string role);
        public IEnumerable<RoleAccess> GetRoleAccessesForProperty(string prop);
    }

    public class RoleAccessData : IRoleAccessData
    {
        private readonly ICollection<RoleAccess> roleAccesses;

        public RoleAccessData()
        {
            roleAccesses = new List<RoleAccess>
            {
                new RoleAccess { Role = "Employee", Property = "IsActive" },
                new RoleAccess { Role = "Partner", Property = "Name" },
                new RoleAccess { Role = "Partner", Property = "Description" },
                new RoleAccess { Role = "Partner", Property = "IsActive" },
                new RoleAccess { Role = "Partner", Property = "City" },
                new RoleAccess { Role = "Partner", Property = "ZipCode" },
                new RoleAccess { Role = "Partner", Property = "Members" },
                new RoleAccess { Role = "Admin", Property = "Name" },
                new RoleAccess { Role = "Admin", Property = "Description" },
                new RoleAccess { Role = "Admin", Property = "IsActive" },
                new RoleAccess { Role = "Admin", Property = "City" },
                new RoleAccess { Role = "Admin", Property = "ZipCode" },
                new RoleAccess { Role = "Admin", Property = "Members" },
                new RoleAccess { Role = "Admin", Property = "CreatedAt" },
                new RoleAccess { Role = "Admin", Property = "CreatedBy" }
            };
        }

        public IEnumerable<RoleAccess> GetRoleAccesses() => roleAccesses;
        public IEnumerable<RoleAccess> GetRoleAccesses(string role) => roleAccesses.Where(ra => ra.RoleLowered == role.ToLower());
        public IEnumerable<RoleAccess> GetRoleAccessesForProperty(string prop) => roleAccesses.Where(ra => ra.PropertyLowered == prop.ToLower());
    }
}
