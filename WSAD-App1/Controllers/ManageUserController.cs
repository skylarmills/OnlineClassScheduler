using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.ManageUser;

namespace WSAD_App1.Controllers
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        public ActionResult Index()
        {
            //Setup a DBcontext
            List<ManageUserViewModel> collectionOfUserVM = new List<ManageUserViewModel>();
            using (WSADDbContext context = new WSADDbContext())
            {
                //Get all users
                var dbUsers = context.Users;

                //Move users to ViewModel Object
                foreach (var userDTO in dbUsers)
                {
                    collectionOfUserVM.Add(
                        new ManageUserViewModel(userDTO)
                        );
                }
            }
            //Send ViewModel Collete
            return View(collectionOfUserVM);
        }

        [HttpPost]
        public ActionResult Delete(List<ManageUserViewModel> collectionOfUsersVM)
        {
            //Filter collectionOfUsers to find only Selected Items
            var vmItemsToDelete = collectionOfUsersVM.Where(x => x.IsSelected == true);

            //Do delete

            using (WSADDbContext context = new WSADDbContext())
            {
                //Loop through ViewModel Items to delete
                foreach (var vmItem in vmItemsToDelete)
                {
                    var dtoToDelete = context.Users.FirstOrDefault(row => row.Id == vmItem.Id);
                    context.Users.Remove(dtoToDelete);
                }

                context.SaveChanges();
            }
                return RedirectToAction("Index");
            
        }
    }
}