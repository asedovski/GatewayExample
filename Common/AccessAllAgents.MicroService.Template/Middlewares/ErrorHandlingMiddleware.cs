using AccessAllAgents.MicroService.Common.Exceptions;
using AccessAllAgents.MicroService.Template.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Global

namespace AccessAllAgents.MicroService.Template.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
            _jsonSerializerSettings = new JsonSerializerSettings {ContractResolver = new CamelCasePropertyNamesContractResolver()};
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var errorResponse = new GenericResponseViewModel
            {
                IsSuccessful = false
            };

            if (ex is AggregateException aggregateException)
            {
                ex = aggregateException.InnerException;
            }

            if (ex is AuthenticationException)
            {
                errorResponse.ErrorCode = (int) HttpStatusCode.Unauthorized;
                errorResponse.FailureReason = "You are not authorized to perform this action.";
            }
            else if (ex is ControllerException ce)
            {
                errorResponse.ErrorCode = ce.ErrorCode;
                errorResponse.FailureReason = ce.FailureReason;
            }
            else if (ex is ServiceException se)
            {
                errorResponse.ErrorCode = se.ErrorCode;
                errorResponse.FailureReason = se.Message;
            }
            else
            {
                errorResponse.ErrorCode = (int) HttpStatusCode.InternalServerError;
                errorResponse.FailureReason = "Oops. Something's gone wrong. Please bear with us until we fix it";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.OK;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse, _jsonSerializerSettings));
        }
    }
}