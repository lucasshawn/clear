using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClearWebWs01.Models.Notifications
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime Expiry { get; set; }
        [Display(Name ="Applies to Device Group Id")]
        public int AppliesToDeviceGroupId { get; set; }
        [Display(Name ="Applies to Device Id")]
        public int AppliesToDevice { get; set; }
        [Display(Name ="AppId (reserved)")]
        public string AppId { get; set; }
    }
}
