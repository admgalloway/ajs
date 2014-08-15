using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WeeWorld.ADS.Controllers.API.Attributes;
using WeeWorld.ADS.Data.Models;

namespace WeeWorld.ADS.Controllers.API
{
    [RequiresHttps]
    public abstract class BaseController<T> : ApiController where T : IModel
    {
        /// <summary>Create a response with a body, 201 status code, and a location header</summary>
        protected HttpResponseMessage ResourceWith201(IModel responseBody)
        {
            return CreateResponse(HttpStatusCode.Created, responseBody);
        }

        /// <summary>Create a response with a body, 200 status code, and a location header</summary>
        protected HttpResponseMessage ResourceWith200(IModel responseBody)
        {
            return CreateResponse(HttpStatusCode.OK, responseBody);
        }

        /// <summary>Create a HttpResponse with a body, status code and location</summary>
        /// <param name="statusCode">The StatusCode to return</param>
        /// <param name="responseBody">The response body to return</param>
        protected HttpResponseMessage CreateResponse(HttpStatusCode statusCode, IModel responseBody)
        {
            HttpResponseMessage response = Request.CreateResponse(statusCode, responseBody);
            response.Headers.Location = new Uri(string.Format("https://{0}/api/{1}s/{2}", Request.RequestUri.Authority, typeof(T).Name, responseBody.Id));
            return response;
        }
    }
}