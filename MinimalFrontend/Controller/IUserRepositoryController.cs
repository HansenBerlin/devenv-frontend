using MinimalFrontend.Models;

namespace MinimalFrontend.Controller;

public interface IUserRepositoryController
{
    Task<UserGetRequestResponseModel> GetAllUsers();
    Task<UserGetRequestResponseModel> GetAllUsersByAge(int minAge);
    Task<UserGetRequestResponseModel> GetUserById(int iD);
    Task<UserGetRequestResponseModel> Create(string name, string mail, int age);
    Task<UserGetRequestResponseModel> Delete(int iD);
    Task<UserGetRequestResponseModel> Update(int iD, string name, string mail, int age);
}