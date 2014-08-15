using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;
using WeeWorld.ADS.Models.Validation;
using Newtonsoft.Json;

namespace WeeWorld.ADS.Controllers.API.Attributes
{
    public class ValidationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>Handle exceptions and format appropriate responses for specific error types</summary>
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is ValidationException)
            {
                var ex = context.Exception as ValidationException;
                var responseBody = JsonConvert.SerializeObject(ex.Errors);

                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                context.Response.ReasonPhrase = ex.Message;
                context.Response.Content = new StringContent(responseBody);
            }
            else if (context.Exception is UnauthorizedAccessException)
            { 
                // log exception, hide error details
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                context.Response.ReasonPhrase = "Access code is unauthorized";
            }
            
            // allow framework to return any other exception types as 500s
        }

    }
}
