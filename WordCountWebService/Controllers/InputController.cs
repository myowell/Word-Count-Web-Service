/* Main controller for the file compression analyzer web service.
 * 
 * This controller will accept an HTTP POST request with a body containing a raw string, parses the results, and return a JSON string containing the top 50 most frequntly encountered words, word count, white space frequency, and punctuation frequency.
 * Attempting to send the service anything buy a POST request will result in an HTTP Bad Request exception and error message being returned.
 * A null or empty plain text request will return a JSON string containing an error notifying the user that the request was invalid.
 * 
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
using System.Web;
using System.Web.Http;
using WordCountWebService.Models;

namespace WordCountWebService.Controllers
{
    public class InputController : ApiController
    {
        // GET: api/Input
        public HttpResponseMessage Get()
        {
            var response = ("GET requests not supported.");
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
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
        public HttpResponseMessage Put()
        {
            var response = ("PUT requests not supported.");
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }

        // DELETE: api/Input/5
        public HttpResponseMessage Delete(int id)
        {
            var response = ("DELETE requests not supported.");
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
        }
    }
}
