using System;
using System.Net;
using System.Linq;
using Domain.Exceptions;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using CrossCutting.Notifications;

namespace Presentation.ApiResponses
{
    public class ApiProblemDetails
    {
        public string Type { get; private set; }
        public string Title { get; private set; }
        public int Status { get; private set; }
        public List<ApiError> Errors { get; private set; }

        private ApiProblemDetails(
            string type,
            string title,
            Exception error,
            HttpStatusCode httpStatusCode)
        {
            Type = type;
            Title = title;
            Errors = CreateError(error);
            Status = (int)httpStatusCode;
        }

        private ApiProblemDetails(
            string type,
            string title,
            IEnumerable<Notification> notifications)
        {
            Type = type;
            Title = title;
            Status = (int)HttpStatusCode.BadRequest;
            Errors = CreateNotifications(notifications);
        }

        public static ApiProblemDetails CreateApiProblemDetails(Exception error, HttpStatusCode code)
        {
            return error switch
            {
                ApiApplicationException => CreateAsApplicationError(error),
                ApiNotFoundException => CreateAsNotFoundError(error),
                SqlException => CreateAsBadGatewayError(error),
                _ => CreateAsInternalServerError(error)
            };
        }
        
        public static ApiProblemDetails CreateApiProblemDetails(IEnumerable<Notification> notifications)
        {
            return new ApiProblemDetails(
                "Application validation",
                "One or more validation errors occurred.",
                notifications);
        }
        
        private static ApiProblemDetails CreateAsInternalServerError(Exception error)
        {
            return new ApiProblemDetails(
                "Internal application error",
                "One or more errors occurred.",
                error,
                HttpStatusCode.InternalServerError);
        }
        
        private static ApiProblemDetails CreateAsApplicationError(Exception error)
        {
            return new ApiProblemDetails(
                "Application validation",
                "One or more validation errors occurred.",
                error,
                HttpStatusCode.BadRequest);
        }

        private static ApiProblemDetails CreateAsNotFoundError(Exception error)
        {
            return new ApiProblemDetails(
                "Not found error",
                "One or more objects were not found",
                error,
                HttpStatusCode.NotFound);
        }

        private static ApiProblemDetails CreateAsBadGatewayError(Exception error)
        {
            return new ApiProblemDetails(
                "Bad gateway error",
                "One or more errors occurred in the database.",
                error,
                HttpStatusCode.BadGateway);
        }

        private static List<ApiError> CreateError(Exception error)
        {
            return new List<ApiError>{ new (error) };
        }

        private static List<ApiError> CreateNotifications(IEnumerable<Notification> notifications)
        {
            return notifications.Select(notification => new ApiError(notification)).ToList();
        }
    }
}