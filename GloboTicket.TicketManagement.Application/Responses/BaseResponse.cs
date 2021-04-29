using System.Collections.Generic;

namespace GloboTicket.TicketManagement.Application.Responses
{
    //TODO: Use base response for all of my query and command responses, to unify my responses to the client.
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }

        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message = null)
        {
            Success = true;
        }

        public BaseResponse(string message, bool success)
        {
            Success = success;
            Message = message;
        }
    }
}