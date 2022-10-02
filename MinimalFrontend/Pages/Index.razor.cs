using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using MinimalFrontend.Controller;
using MinimalFrontend.Enums;
using MinimalFrontend.Models;

namespace MinimalFrontend.Pages;

public partial class Index
{
    [Inject] private IUserRepositoryController UserRepositoryController { get; set; }
    private string BadgeColor => _currentResponseModel.ResponseStatus == ResponseStatus.OK ? "#00cc6a" : "#ff4343";
    private string StatusMessage => _currentResponseModel.ResponseMessage;
    const int ShowMessageForMilliseconds = 2500;
    private readonly ServiceUser _formModel = new();
    private readonly List<ColumnDefinition<ServiceUser>> _columnsGrid = new();
    private readonly List<ServiceUser> _users = new();
    private readonly List<int> _userIds = new();
    private string _activeTabId ="TabOne";
    private bool _showStatusMessage;
    private UserGetRequestResponseModel _currentResponseModel = new();

    protected override async Task OnInitializedAsync()
    {
        _columnsGrid.Add(new ColumnDefinition<ServiceUser>("Id", x => x.Id));
        _columnsGrid.Add(new ColumnDefinition<ServiceUser>("Mail", x => x.Mail));
        _columnsGrid.Add(new ColumnDefinition<ServiceUser>("Username", x => x.UserName));
        _columnsGrid.Add(new ColumnDefinition<ServiceUser>("Age", x => x.Age));
        _currentResponseModel = await UserRepositoryController.GetAllUsers();
        UpdateUsersList(_currentResponseModel.ServiceUsers);
        foreach (var user in _users)
        {
            _userIds.Add(user.Id);
        }
    }

    private void UpdateUsersList(List<ServiceUser> updatedList)
    {
        _users.Clear();
        foreach (var user in updatedList)
        {
            _users.Add(user);
        }
    }

    private async Task FetchAll()
    {
        _currentResponseModel = await UserRepositoryController.GetAllUsers();
        UpdateUsersList(_currentResponseModel.ServiceUsers);
        UpdateFormViewModel();
        await ResetStatusMessage();

    }

    private async Task GetByAge()
    {
        _currentResponseModel = await UserRepositoryController.GetAllUsersByAge(_formModel.Age);
        UpdateUsersList(_currentResponseModel.ServiceUsers);
        UpdateFormViewModel();
        await ResetStatusMessage();

    }

    private async Task GetById()
    {
        _currentResponseModel = await UserRepositoryController.GetUserById(_formModel.Id);
        UpdateUsersList(_currentResponseModel.ServiceUsers);
        UpdateFormViewModel();
        await ResetStatusMessage();

    }

    private async Task Create()
    {
        _currentResponseModel = await UserRepositoryController.Create(_formModel.UserName, _formModel.Mail, _formModel.Age);
        UpdateFormViewModel();
        await ResetStatusMessage();

    }

    private async Task Update()
    {
        _currentResponseModel = await UserRepositoryController.Update(_formModel.Id, _formModel.UserName, _formModel.Mail, _formModel.Age);
        UpdateFormViewModel();
        await ResetStatusMessage();

    }

    private async Task Delete()
    {
       _currentResponseModel = await UserRepositoryController.Delete(_formModel.Id);
        UpdateFormViewModel();
        await ResetStatusMessage();
    }

    private void UpdateFormViewModel()
    {
        if (_currentResponseModel.ResponseStatus == ResponseStatus.OK)
        {
            _formModel.Age = 0;
            _formModel.Id = _users.Count > 0 ? _userIds[0] : 1;
            _formModel.Mail = "";
            _formModel.UserName = "";
        }
    }

    private async Task ResetStatusMessage()
    {
        _showStatusMessage = true;
        StateHasChanged();
        await Task.Delay(ShowMessageForMilliseconds);
        _showStatusMessage = false;
        StateHasChanged();
    }
}