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
        public IEnumerable<SessionSearchViewModel> Get(string term)
        {


            using (WSADDbContext context = new WSADDbContext())
            {
                IQueryable<Session> matches;
                List<SessionSearchViewModel> ssVM = new List<SessionSearchViewModel>();

                if (string.IsNullOrWhiteSpace(term))
                {
                    matches = context.Sessions.AsQueryable();
                }
                else {
                    matches = context.Sessions
                        .Where(row => row.Course.StartsWith(term));


                    
                }

                    foreach (var sessionDTO in matches)
                    {
                        ssVM.Add(new SessionSearchViewModel(sessionDTO));
                    }

                    return ssVM;
                }
            
        }

        /*public IEnumerable<string> Get()
        {
            return new string[] { "Value1", "Value2" };
        }

        // GET api/<controller>/5
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