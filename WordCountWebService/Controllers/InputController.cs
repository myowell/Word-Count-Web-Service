/* Main controller for the file compression analyzer web service.
 * 
 * This controller will accept an HTTP POST request with a body containing a raw string, parses the results, and return a JSON string containing the top 50 most frequntly encountered words, word count, white space frequency, and punctuation frequency.
 * Author: Michael Yowell - michael.yowell@gmail.com
 */

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WordCountWebService.Models;

namespace WordCountWebService.Controllers
{
    public class InputController : ApiController
    {
        // GET: api/Input
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Input
        public JObject Post([FromBody]String request)
        {
            if (request.Length == 0 || request == null){
                dynamic errorResponse = new JObject();
                errorResponse.Add("Error", "Please send a non-empty non-null string.");
                return errorResponse;

            }

            FileRequestModel response = new FileRequestModel(request);

            return response.getFileCountJSONResponse();
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
