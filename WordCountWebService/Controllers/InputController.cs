using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WordCountWebService.Controllers
{
    public class InputController : ApiController
    {
        // GET: api/Input
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Input/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Input
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Input/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Input/5
        public void Delete(int id)
        {
        }
    }
}
