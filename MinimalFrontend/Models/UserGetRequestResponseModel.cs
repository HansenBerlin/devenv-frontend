using MinimalFrontend.Enums;

namespace MinimalFrontend.Models;

public class UserGetRequestResponseModel
{
    public List<ServiceUser> ServiceUsers { get; } = new();
    public string ResponseMessage { get; } = "";
    public ResponseStatus ResponseStatus { get; }

    public UserGetRequestResponseModel(List<ServiceUser> users, string responseMessage, ResponseStatus responseStatus)
    {
        ServiceUsers = users;
        ResponseMessage = responseMessage;
        ResponseStatus = responseStatus;
    }
    
    public UserGetRequestResponseModel(){}
}