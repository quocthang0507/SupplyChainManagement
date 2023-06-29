using Microsoft.AspNetCore.Identity;
using System.Net;

namespace SupplyChainManagement.Models.Response
{
    public class ApiResponse
    {
        public bool IsSuccess => Errors.Count == 0 && StatusCode == StatusCodes.Status200OK;

        public HttpStatusCode HttpStatusCode { get; init; }

        public int StatusCode { get; init; }

        public IList<string> Errors { get; init; }

        protected ApiResponse()
        {
            HttpStatusCode = HttpStatusCode.OK;
            StatusCode = (int)HttpStatusCode.OK;
            Errors = new List<string>();
        }

        public static ApiResponse<T> Success<T>(
            T result) => new ApiResponse<T>
            {
                Result = result,
                HttpStatusCode = HttpStatusCode.OK,
                StatusCode = StatusCodes.Status200OK
            };

        public static ApiResponse<T> Success<T>(
            T result,
            HttpStatusCode statusCode = HttpStatusCode.OK) => new ApiResponse<T>
            {
                Result = result,
                HttpStatusCode = statusCode,
                StatusCode = (int)statusCode,
            };

        public static ApiResponse<T> Success<T>(
            T result,
            int statusCode = StatusCodes.Status200OK) => new ApiResponse<T>
            {
                Result = result,
                HttpStatusCode = (HttpStatusCode)statusCode,
                StatusCode = statusCode,
            };

        public static ApiResponse<T> Fail<T>(
            HttpStatusCode statusCode,
            T result,
            params string[] errorMessages) => new ApiResponse<T>()
            {
                Result = result,
                HttpStatusCode = statusCode,
                StatusCode = (int)statusCode,
                Errors = new List<string>(errorMessages)
            };

        public static ApiResponse<T> Fail<T>(
            int statusCode,
            T result,
            params string[] errorMessages) => new ApiResponse<T>()
            {
                Result = result,
                HttpStatusCode = (HttpStatusCode)statusCode,
                StatusCode = statusCode,
                Errors = new List<string>(errorMessages)
            };

        public static ApiResponse Fail(
            HttpStatusCode statusCode,
            params string[] errorMessages)
        {
            if (errorMessages is null or { Length: 0 })
            {
                throw new ArgumentNullException(nameof(errorMessages));
            }

            return new ApiResponse()
            {
                HttpStatusCode = statusCode,
                StatusCode = (int)statusCode,
                Errors = new List<string>(errorMessages)
            };
        }

        public static ApiResponse Fail(
            int statusCode,
            params string[] errorMessages)
        {
            if (errorMessages is null or { Length: 0 })
            {
                throw new ArgumentNullException(nameof(errorMessages));
            }

            return new ApiResponse()
            {
                HttpStatusCode = (HttpStatusCode)statusCode,
                StatusCode = statusCode,
                Errors = new List<string>(errorMessages)
            };
        }

        public static ApiResponse Fail(
            HttpStatusCode statusCode,
            params IdentityError[] errorMessages) => Fail(statusCode,
                errorMessages.Aggregate("", (x, y) => x + y.Description + "\n\r"));

        public static ApiResponse Fail(
            int statusCode,
            params IdentityError[] errorMessages) => Fail(statusCode,
                errorMessages.Aggregate("", (x, y) => x + y.Description + "\n\r"));

        public static ApiResponse Fail(HttpStatusCode statusCode) => Fail(statusCode);

        public static ApiResponse Fail(int statusCode) => Fail(statusCode);
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Result { get; set; }
    }
}
