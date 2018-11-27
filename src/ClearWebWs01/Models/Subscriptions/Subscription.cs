using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClearWebWs01.Models.Subscriptions
{
    public class Subscription
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Device Group Id")]
        public int DeviceGroupId { get; set; }
        public string Tier { get; set; }
        public DateTime Expiry { get; set; }
        [Display(Name ="Activation Date")]
        public DateTime ActivationDate { get; set; }
    }
}
