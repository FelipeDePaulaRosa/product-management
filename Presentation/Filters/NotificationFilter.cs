using System.Net;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Presentation.ApiResponses;
using Microsoft.AspNetCore.Http;
using CrossCutting.Notifications;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters
{
    public class NotificationFilter : ActionFilterAttribute
    {
        private readonly INotificationContext _notificationContext;
        
        public NotificationFilter(INotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
        
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (_notificationContext.HasNotifications)
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.HttpContext.Response.ContentType = "application/json";

                var apiResponse = ApiProblemDetails.CreateApiProblemDetails(_notificationContext.Notifications);

                var responseContent = JsonConvert.SerializeObject(apiResponse);

                await context.HttpContext.Response.WriteAsync(responseContent);

                return;
            }

            await next();
        }
    }
}