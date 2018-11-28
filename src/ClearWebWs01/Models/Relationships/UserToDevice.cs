using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClearWebWs01.Models.Relationships
{
    public class UserToDevice
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int DeviceId { get; set; }
        public UserToDeviceRelationship Relationship { get; set; }
    }

    public enum UserToDeviceRelationship
    {
        Owner,
        Delegate,
        Concierge
    }
}
