﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.Schedule;

namespace WSAD_App1.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        // GET: CourseCart
        public ActionResult Index()
        {
            List<EnrolledSessionViewModel> enrolledSessions = new List<EnrolledSessionViewModel>();

            using (WSADDbContext context = new WSADDbContext())
            {
                //Get User info
                string username = User.Identity.Name;

                //Get user id from DB
                var userId = context.Users
                    .Where(x => x.Username == username)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                //Get Enrolled sessions
                enrolledSessions = context.UserSessions.Where(x => x.User_Id == userId)
                    .ToArray()
                    .Select(x => new EnrolledSessionViewModel(x))
                    .ToList();


                //Generated Schedule view model
            }
                return View(enrolledSessions);
        }

        [HttpPost]
        public ActionResult Delete(List<EnrolledSessionViewModel> collectionOfSessionsVM)
        {
            //Get sessions to delete

            var vmItemsToDelete = collectionOfSessionsVM.Where(x => x.IsSelected == true);

            //Do delete

            using (WSADDbContext context = new WSADDbContext())
            {
                //Loop through ViewModel Items to delete
                foreach (var vmItem in vmItemsToDelete)
                {
                    var dtoToDelete = context.UserSessions.FirstOrDefault(row => row.Session_Id == vmItem.Session_Id);
                    context.UserSessions.Remove(dtoToDelete);
                }

                context.SaveChanges();
            }

            TempData["success"] = "Successfully unenrolled from course(s)";
            return RedirectToAction("Index");
        }
    }
}