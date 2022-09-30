using MinimalFrontend.Models;

namespace MinimalFrontend.Controller;

public interface IUserRepositoryController
{
    Task<ServiceUser[]> GetAllUsers();
    Task<ServiceUser[]> GetAllUsersByAge(int maxAge);
    Task<ServiceUser> GetUserById(int iD);
    Task Create(string name, string mail, int age);
    Task Delete(int iD);
    Task Update(int iD, string name, string mail, int age);
}