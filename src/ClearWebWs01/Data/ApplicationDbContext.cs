using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ClearWebWs01.Models;
using ClearWebWs01.Models.Devices;
using ClearWebWs01.Models.Subscriptions;
using ClearWebWs01.Models.Notifications;

namespace ClearWebWs01.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ClearWebWs01.Models.Devices.Device> Device { get; set; }
        public DbSet<ClearWebWs01.Models.Subscriptions.Subscription> Subscription { get; set; }
        public DbSet<ClearWebWs01.Models.Notifications.Notification> Notification { get; set; }
        
    }
}
