using System.Net.Mime;
using System.Text;
using System.Text.Json;
using MinimalFrontend.Models;

namespace MinimalFrontend.Controller;



public class UserRepositoryController : IUserRepositoryController
{
    private readonly HttpClient _httpClient;
    private const string UsersEndpoint = "http://localhost:5000/users";

    public UserRepositoryController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceUser[]> GetAllUsers()
    {
        return await _httpClient.GetFromJsonAsync<ServiceUser[]>(UsersEndpoint);
        //return response ?? Array.Empty<ServiceUser>();

    }
    
    public async Task<ServiceUser[]> GetAllUsersByAge(int maxAge)
    {
        return await _httpClient.GetFromJsonAsync<ServiceUser[]>($"{UsersEndpoint}/age/{maxAge}");
        //return response ?? Array.Empty<ServiceUser>();
    }
    
    public async Task<ServiceUser> GetUserById(int iD)
    {
        return await _httpClient.GetFromJsonAsync<ServiceUser>($"{UsersEndpoint}/{iD}");
        //return response ?? new ServiceUser();
    }
    
    public async Task Create(string name, string mail, int age)
    {
        var json = ConvertToJson(0, name, mail, age);
        using var httpResponseMessage = await _httpClient.PostAsync(UsersEndpoint, json);
        httpResponseMessage.EnsureSuccessStatusCode();
    }

    private StringContent ConvertToJson(int iD, string name, string mail, int age)
    {
        var user = new ServiceUser
        {
            Id = iD,
            Age = age,
            UserName = name,
            Mail = mail
        };
        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
        return todoItemJson;
    }

    public async Task Delete(int iD)
    {
        await _httpClient.DeleteAsync(UsersEndpoint);
    }

    public async Task Update(int iD, string name, string mail, int age)
    {
        var json = ConvertToJson(iD, name, mail, age);
        using var httpResponseMessage = await _httpClient.PutAsync(UsersEndpoint, json);
        httpResponseMessage.EnsureSuccessStatusCode();
    }
}