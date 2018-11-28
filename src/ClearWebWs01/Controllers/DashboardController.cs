using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClearWebWs01.Data;
using ClearWebWs01.Models.Devices;
using ClearWebWs01.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ClearWebWs01.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        public DashboardController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        // GET: Dashboard
        public ActionResult Index()
        {
            AppDataViewModel adv = new AppDataViewModel();

            // Get the list of devices and subscriptions attached to this user
            var user = dbContext.Users.Where(u => u.NormalizedUserName == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user != null)
            {
                var devicesForUser = dbContext.UsersToDevices.Where(u => u.UserId == user.Id);
                if (devicesForUser.Count() > 0)
                {
                    var deviceIds = devicesForUser.Where(u => u.Relationship == Models.Relationships.UserToDeviceRelationship.Owner).Select(d => d.DeviceId);
                    adv.OwnedDevices = dbContext.Device.Where(d => deviceIds.Contains(d.Id));
                }
            }
            else
            {
                // TODO: error out - alert user they aren't in the db (for some reason!?)
                return StatusCode(404, "Could not find current user.");
            }

            return View(adv);
        }

        [HttpPut]
        [Route("dashboard/binddevicetothisuser/{activationId}")]
        public async Task<IActionResult> BindDeviceToThisUser(int? activationId)
        {
            // Get the current user, their ID, then find the device with activationId, then attach device to this profile
            // TODO: there needs to be more validation on this device (is it already activated?  is it already attached to another profile?
            //       is it deprecated?  etc...)
            var user = dbContext.Users.Where(u => u.NormalizedUserName == HttpContext.User.Identity.Name).FirstOrDefault();
            if (user == null)
            {
                return Json(new
                {
                    success = false,
                    message = "The current logged in user cannot be found in the database.  Please sign out and back in."
                });
            }

            // Does the specified device exist and is it Pending Activation?
            var device = dbContext.Device.Where(d => d.ActivationCode == activationId).FirstOrDefault();
            if (device == null)
            {
                return Json(new
                {
                    success = false,
                    message = $"The activation code ({activationId}) could not be found in the database.   Check to make sure you entered the correct 4 digit number."
                });
            }

            // Check existing associations
            var userToDeviceMappings = dbContext.UsersToDevices.Where(utd => utd.DeviceId == device.Id);
            if (userToDeviceMappings.Count() > 0)
            {
                if (userToDeviceMappings.Where(u => u.UserId == user.Id).FirstOrDefault() != null)
                {
                    return Json(new
                    {
                        success = false,
                        message = $"This device is already in your list.  Did you mean to enter a different activation code?"
                    });
                }
                return Json(new
                {
                    success = false,
                    message = $"This device is already associated with one or more users.  Please ensure the device has been factory reset before proceeding.  This can be done by holding the Home and Vol Up buttons for 5 seconds."
                });
            }

            // Check device status
            if (device.Status != DeviceStatus.ProvisionedPendingUser)
            {
                return Json(new
                {
                    success = false,
                    message = $"The device status is {device.Status} but we're expecting it to be 'Provisioned - Pending User Activation'."
                });
            }

            // Time to associate the device to the user (as owner)
            dbContext.UsersToDevices.Add(new Models.Relationships.UserToDevice
            {
                DeviceId = device.Id,
                Relationship = Models.Relationships.UserToDeviceRelationship.Owner,
                UserId = user.Id
            });
            dbContext.SaveChanges();

            // TODO: change to async and check return value


            // Pass back the device we're attaching so web page can update with device info
            var res = Json(new
            {
                success = true,
                device = JsonConvert.SerializeObject(device)
            });
            return res;
        }

        // GET: Dashboard/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Dashboard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dashboard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dashboard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dashboard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dashboard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}