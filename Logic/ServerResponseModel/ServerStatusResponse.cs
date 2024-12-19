using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace resource_mangment.Logic.ServerResponseModel
{
    public enum Status
    {
        Proccessing,
        Finished,
    }

    public class ServerStatusResponse
    {
        /// <summary>
        /// TRUE if the request attempt is successful, FALSE otherwise.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Title Optiona for error message
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Title Optiona for error message
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Login attempt result message
        /// </summary>
        public string Message { get; set; } = null!;

        /// <summary>
        /// Login attempt result message
        /// </summary>
        public string CurrentStatus { get; set; } = Status.Proccessing.ToString();

        /// <summary>
        /// The JWT token if the login attempt is successful, or NULL if not
        /// </summary>
        public string? Token { get; set; }
    }
}
