using Microsoft.AspNetCore.Components;
using dsr_admin.Models;
using System.Net.Http.Json;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using dsr_admin.Clients;

namespace dsr_admin.BaseComponents;

public class BaseSecureComponent : ComponentBase
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected UserSession UserSession { get; set; } = default!;
    [Inject] protected AccountClient AccountClient { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        // Authorization
        if (string.IsNullOrEmpty(UserSession.UserName))
        {
            Nav.NavigateTo("/");
            return;
        }

        // Permission
        bool isAuthorize = false;
        var uri = Nav.ToBaseRelativePath(Nav.Uri);
        var pageName = uri.Split('?')[0].Trim('/').ToLower(); 

        Console.WriteLine($"Current Page: {pageName}");

        var permissions = await AccountClient.GetPermissions(UserSession.UserRoleId);

        isAuthorize = permissions.Any(x => x.PageName.ToLower() == pageName);        

        if (UserSession.UserRoleId == 0 || !isAuthorize)
        {
            Nav.NavigateTo("/unauthorized");
        }

        await base.OnInitializedAsync();
    }
}
