﻿using System.Net;
using Domain.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Presentation.ApiResponses;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var code = exception switch
            {
                ApiApplicationException => HttpStatusCode.BadRequest,
                ApiNotFoundException => HttpStatusCode.NotFound,
                SqlException => HttpStatusCode.BadGateway,
                _ => HttpStatusCode.InternalServerError
            };

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)code;
            
            var response = ApiProblemDetails.CreateApiProblemDetails(exception, code);
            context.Result = new JsonResult(response);
            
            base.OnException(context);
        }
    }
}