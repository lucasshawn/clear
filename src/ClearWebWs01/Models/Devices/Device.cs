using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClearWebWs01.Models.Devices
{
    public class Device
    {
        [Display(Name = "Db Id")]
        public int Id { get; set; }
        [Display(Name = "Device Id")]
        public string Guid { get; set; }
        [Display(Name = "Device Name")]
        public string Name { get; set; }
        public DeviceStatus Status { get; set; }
        [Display(Name="Room")]
        public string OnPremLocation { get; set; }
        [Display(Name="World Location")]
        public string GeoLocation { get; set; }
        [Display(Name="Group Id")]
        public string GroupId { get; set; }
        [Display(Name ="Activation Code")]
        public int ActivationCode { get; set; }
    }

    public enum DeviceStatus
    {
        [Display(Name ="Unknown State")]
        Unknown,                            // 0
        [Display(Name = "Pending First Use")]
        PendingProvision,                   // 1
        [Display(Name = "Pending User Activation")]
        ProvisionedPendingUser,             // 2
        [Display(Name = "Configured")]
        UserConfiguredPendingAcknowledge,   // 3
        [Display(Name = "Pending Subscription")]
        ActivePendingSubscription,          // 4
        [Display(Name = "Active")]
        Active,                             // 5
        [Display(Name = "Deprecated")]
        Deprecated                          // 6
    }
}
