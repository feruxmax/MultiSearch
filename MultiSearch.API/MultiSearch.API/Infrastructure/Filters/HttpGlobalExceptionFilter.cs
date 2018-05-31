using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Threading;
using System.Threading.Tasks;


namespace MultiSearch.API.Infrastructure.Filters
{
    using ViewModels;

    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                HttpStatusCode.OK, new ApiBaseResponse
                {
                    Error = new Error()
                });
        }

        public override async Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(
                HttpStatusCode.OK, new ApiBaseResponse
                {
                    Error = new Error()
                });
        }
    }
}