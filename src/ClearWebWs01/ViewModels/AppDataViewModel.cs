using ClearWebWs01.Models.Devices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClearWebWs01.ViewModels
{
    /// <summary>
    /// This view model encompasses all app data being managed by the app.
    /// </summary>
    public class AppDataViewModel
    {
        public DbSet<Device> DeviceList { get; set; }
        public DbSet<IdentityUser> Users { get; set; }
    }
}
