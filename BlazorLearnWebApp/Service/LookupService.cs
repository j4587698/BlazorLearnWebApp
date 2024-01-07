using BlazorLearnWebApp.Entity;
using BootstrapBlazor.Components;

namespace BlazorLearnWebApp.Service;

public class LookupService : ILookupService
{
    public IEnumerable<SelectedItem>? GetItemsByKey(string? key)
    {
        if (key == "role")
        {
            return RoleEntity.Select.ToList(x => new SelectedItem(x.Id.ToString(), x.RoleName!));
        }
        return null;
    }
}