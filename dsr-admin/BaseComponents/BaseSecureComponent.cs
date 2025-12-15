using Microsoft.AspNetCore.Components;
using dsr_admin.Models;

namespace dsr_admin.BaseComponents;

public class BaseSecureComponent : ComponentBase
{
    [Inject] protected NavigationManager Nav { get; set; } = default!;
    [Inject] protected UserSession UserSession { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        // Authorization
        if (string.IsNullOrEmpty(UserSession.UserName))
        {
            Nav.NavigateTo("/");
            return;
        }

        // Permission
        var pageName = this.GetType().Name; // Dashboard, Profile, etc.

        Console.WriteLine($"Current Page: {pageName}");

        if (UserSession.UserRoleId == 0)
        {
            Nav.NavigateTo("/unauthorized");
        }

        await base.OnInitializedAsync();
    }
}
