using System;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace TaxValidationApi.Models
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";
        public HttpResponseHeaders Headers { get; }

        public HttpStatusCodeException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message, HttpResponseHeaders headers) : this(statusCode,
            message)
        {
            this.Headers = headers;
        }

        public HttpStatusCodeException(int statusCode, Exception inner, HttpResponseHeaders headers) : this(statusCode,
            inner.ToString(), headers)
        {
        }

        public HttpStatusCodeException(int statusCode, JObject errorObject, HttpResponseHeaders headers) : this(
            statusCode, errorObject.ToString(), headers)
        {
            this.ContentType = @"application/json";
        }
    }
}