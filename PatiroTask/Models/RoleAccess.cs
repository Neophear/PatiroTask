using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatiroTask.Models
{
    public class RoleAccess
    {
        private string role;
        private string roleLowered;
        private string property;
        private string propertyLowered;

        public string Role
        {
            get => role;
            set
            {
                roleLowered = value.ToLower();
                role = value;
            }
        }

        public string RoleLowered { get => roleLowered; }

        public string Property
        {
            get => property;
            set
            {
                propertyLowered = value.ToLower();
                property = value;
            }
        }

        public string PropertyLowered { get => propertyLowered; }
    }
}
