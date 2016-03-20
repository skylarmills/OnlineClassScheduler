using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WSAD_App1.Models.Data;

namespace WSAD_App1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AuthenticateRequest()
        {
            if (Context.User == null) { return; } //no user set
            //Get Current User Username
            string username = Context.User.Identity.Name;



            //Setup DbContext
            string[] roles = null;
            using (WSADDbContext context = new WSADDbContext())
            {

                //Add roles to IPrincipal Object
                User userDTO = context.Users.FirstOrDefault(row => row.Username == username);
                if(userDTO != null)
                {
                    roles = context.UserRoles.Where(row => row.UserId == userDTO.Id)
                        .Select(row => row.Role.Name)
                        .ToArray();
                }
            }

            //Build IPrincipal Object
            IIdentity userIdentity = new GenericIdentity(username);
            IPrincipal newUserObj = new System.Security.Principal.GenericPrincipal(userIdentity,roles);

            //Update Context.User with Iprincipal
            Context.User = newUserObj;
        }
    }
}
