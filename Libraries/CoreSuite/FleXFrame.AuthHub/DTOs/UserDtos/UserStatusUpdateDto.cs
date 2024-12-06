using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleXFrame.AuthHub.DTOs.UserDtos
{
    public class UserStatusUpdateDto
    {
        public enum UserStatuses { Active, Inactive, Suspended, Deleted }
        

        public required string UserID { get; set; }
        public bool IsActive { get; set; }
        public required UserStatuses UserStatus { get; set; }


        public required DateTime LastModified { get; set; }
        public required string ModifiedBy { get; set; }
    }
}
