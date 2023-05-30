using Microsoft.Data.SqlClient;
using System.Net;
using System.Text;
using System.Text.Json;

namespace ScopoERP.Helpers;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var response = context.Response;
        response.ContentType = "application/json";
        StringBuilder dberror = new StringBuilder();
        try
        {
            await _next(context);
        }
        catch (Exception error)
        {
            switch (error)
            {
                case SqlException:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case NullReferenceException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case ArithmeticException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case WebException:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case AccessViolationException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case JsonException:
                    response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    break;
                case RankException:
                    response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    break;
                case TimeoutException:
                    response.StatusCode = (int)HttpStatusCode.RequestTimeout;
                    break;
                case UriFormatException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case DirectoryNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case InvalidCastException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case BadImageFormatException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case OutOfMemoryException:
                    response.StatusCode = (int)HttpStatusCode.InsufficientStorage;
                    break;
                case TypeAccessException:
                    response.StatusCode = (int)HttpStatusCode.MethodNotAllowed;
                    break;
                case InvalidDataException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case MethodAccessException:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    break;
                case PathTooLongException:
                    response.StatusCode = (int)HttpStatusCode.RequestUriTooLong;
                    break;
                case InvalidOperationException:
                    response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
                    break;
                case RouteCreationException:
                    response.StatusCode = (int)HttpStatusCode.MisdirectedRequest;
                    break;
                case FileNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case IndexOutOfRangeException:
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case BadHttpRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case AggregateException:
                    response.StatusCode = (int)HttpStatusCode.FailedDependency;
                    break;
                case NotSupportedException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case SystemException:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(new { message = error.Message, innerexception = error.InnerException?.Message });
            await response.WriteAsync(result);
        }
    }
}
