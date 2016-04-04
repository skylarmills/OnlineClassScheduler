using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WSAD_App1.Models.Data;
using WSAD_App1.Models.ViewModels.SessionSearch;

namespace WSAD_App1.Controllers
{
    public class SessionSearchController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<SessionSearchViewModel> Get(string query)
        {
            using(WSADDbContext context = new WSADDbContext())
            {
                var matches = context.Sessions
                    .Where(row => row.Course.StartsWith(query));


                List<SessionSearchViewModel> ssVM = new List<SessionSearchViewModel>();

                foreach(var sessionDTO in matches)
                {
                    ssVM.Add(new SessionSearchViewModel(sessionDTO));
                }

                return ssVM;
            }
        }

        /*// GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }*/
    }
}